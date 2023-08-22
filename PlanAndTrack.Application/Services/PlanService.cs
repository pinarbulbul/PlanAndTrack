
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Resources;
using System.Xml.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using PlanAndTrack.Application.Models.Plan;
using PlanAndTrack.Application.Models.TestRequest;
using PlanAndTrack.Application.Repositories;
using PlanAndTrack.Application.Services.Interfaces;
using PlanAndTrack.Application.Validations;
using PlanAndTrack.Domain.Entities;
using PlanAndTrack.Domain.Enums;
using static System.Net.Mime.MediaTypeNames;
using CreateVm = PlanAndTrack.Application.Models.Plan.CreateVm;
using DetailVm = PlanAndTrack.Application.Models.Plan.DetailVm;

namespace PlanAndTrack.Application.Services
{
    public class PlanService : IPlanService
    {
        private readonly IPlanPeriodRepository _planRepository;
        private readonly IPlanPerformanceRepository _performanceRepository;
        private readonly IPlanPeriodResourceRepository _resourcesRepository;
        private readonly IPlanTestRequestRepository _planTestRequestRepository;
        private readonly ITestRequestRepository _testRequestRepository;
        private readonly IHttpContextAccessor? _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IValidations _validations;

        public PlanService(IPlanPeriodRepository planRepository,
        IPlanPerformanceRepository performanceRepository,
        IPlanPeriodResourceRepository resourcesRepository,
        IPlanTestRequestRepository planTestRequestRepository,
        ITestRequestRepository testRequestRepository,
        IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IValidations validations)
        {
            _planRepository = planRepository;
            _performanceRepository = performanceRepository;
            _resourcesRepository = resourcesRepository;
            _planTestRequestRepository = planTestRequestRepository;
            _testRequestRepository = testRequestRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _validations = validations;
        }

        private string? GetUserName()
        {
            return _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        }

        private bool IsUserManager()
        {
            return (bool)_httpContextAccessor?.HttpContext?.User?.IsInRole(role: "Manager");
        }

        public void ControlCreatePlan(string? userName, bool isUserManager)
        {
            _validations.IsUserAuthorizedForCreatePlan(userName, isUserManager);
            _validations.IsPlanAbleToCreate(_planRepository.IsThereAnyContinuePlan());
        }

 
        public async Task<List<PlanHeaderVm>> GetAllAsync()
        {
            var plans = await _planRepository.GetAllAsync();
            List<PlanHeaderVm> list = _mapper.Map<List<PlanHeaderVm>>(plans);
            return list.OrderByDescending(x => x.Id).ToList();
        }

        public CreateVm GetCreateVmAsync()
        {
            return new CreateVm()
            {
                Resources = new List<ResourcesVm>() {
                    new ResourcesVm() { TestType = TestTypes.Functional  },
                    new ResourcesVm() { TestType = TestTypes.Performance  },
                    new ResourcesVm() { TestType = TestTypes.Security  }
                }
            };
        }

        public async Task<CreateVm> GetPlanTasksAsync(CreateVm createVm)
        {
            createVm.CanBeCreated = _validations.CanPlanBeCreated(_planRepository.IsThereAnyContinuePlan());
            var testRequestsNumber = await GetPlanTypeRequestsAsync(PlanTypes.NUMBER, createVm.Resources);
            var testRequestsImportance = await GetPlanTypeRequestsAsync(PlanTypes.IMPORTANCE, createVm.Resources);
            createVm.PlanTypes = new List<PlanTypesVm>()
            {
                new PlanTypesVm(){
                    AppliedType=PlanTypes.NUMBER,
                    TestRequests=testRequestsNumber,
                    Performance=CalculatePerformanceValues(testRequestsNumber,createVm.Resources)
                },
                new PlanTypesVm(){
                    AppliedType=PlanTypes.IMPORTANCE,
                    TestRequests=testRequestsImportance,
                    Performance=CalculatePerformanceValues(testRequestsImportance, createVm.Resources)
                }
            };
            return createVm;
        }

        private async Task<List<TestRequestsVm>> GetPlanTypeRequestsAsync(PlanTypes appliedPlan, List<ResourcesVm> resources)
        {
            var planRequests = new List<TestRequestsVm>();

            var unfinishedRequests = _testRequestRepository.GetUnfinishedTestRequests();

            //Önceki planlamalardan kalan bitirilmemiş işler bu periodda kesin planlamada olacak.
            foreach (var item in unfinishedRequests)
            {
                planRequests.Add(_mapper.Map<TestRequestsVm>(item));
            }

            foreach (var res in resources)
            {
                var resTests = unfinishedRequests.Where(x => x.TestType == res.TestType);
                res.Promised = (int)resTests.Sum(x => (int?)x.LeftTimeRequired);
            }


            var approvedRequests = _testRequestRepository.GetApprovedTestRequests();

            DataTable canPlannedRequests = new DataTable();
            canPlannedRequests.Columns.Add("TestId", typeof(Int32));
            canPlannedRequests.Columns.Add("PreRequestTestId", typeof(Int32));
            canPlannedRequests.Columns.Add("TestType", typeof(Int32));
            canPlannedRequests.Columns.Add("TimeRequired", typeof(Int32)); //Weight
            canPlannedRequests.Columns.Add("Value", typeof(Int32));//Profit
            canPlannedRequests.Columns.Add("Contribution", typeof(double));// Value/TimeRequired
            canPlannedRequests.Columns.Add("IsSelected", typeof(SByte));

            DataRow row;


            foreach (var req in approvedRequests)
            {
                if (req.PreRequestId != null)
                {
                    var isInApprovedList = approvedRequests.Where(x => x.Id == req.PreRequestId).ToList().Count;
                    if (isInApprovedList == 0)
                    {
                        var preReq = await _testRequestRepository.GetAsync((int)req.PreRequestId);
                        if (preReq.Status.Equals(TestRequestStatus.UPDATED)
                            || preReq.Status.Equals(TestRequestStatus.NEW)
                            || preReq.Status.Equals(TestRequestStatus.REJECTED))
                        {
                            continue;
                        }
                        else
                        {
                            req.PreRequestId = null;
                        }
                    }
                }

                row = CreateRow(row = canPlannedRequests.NewRow(), req, appliedPlan);
                canPlannedRequests.Rows.Add(row);
            }

            var result = ChooseRequests(canPlannedRequests, resources);

            var selectedRequest = result.AsEnumerable()
                .Where(myRow => myRow.Field<sbyte>("IsSelected") == 1);

            foreach (var item in selectedRequest)
            {
                var selected = approvedRequests.Where(x => x.Id == (int)item["TestId"]).FirstOrDefault();
                planRequests.Add(_mapper.Map<TestRequestsVm>(selected));
            }

            return planRequests;
        }

        private DataRow CreateRow(DataRow row, TestRequest req, PlanTypes appliedPlan)
        {
            int timeRequired = (int)req.TimeRequired;
            int value = appliedPlan == PlanTypes.IMPORTANCE ? (int)req.ImportanceLevel * 100 : 100;

            row["TestId"] = req.Id;
            row["PreRequestTestId"] = req.PreRequestId == null ? DBNull.Value : req.PreRequestId;
            row["TestType"] = (int)req.TestType;
            row["TimeRequired"] = timeRequired;
            row["Value"] = value;
            row["Contribution"] = Convert.ToDouble(value) / Convert.ToDouble(timeRequired);
            row["IsSelected"] = 0;
            return row;
        }

        private DataTable ChooseRequests(DataTable dt, List<ResourcesVm> resources)
        {
            try
            {
                //Select and Order Test Request with No PreRequest
                var rowsNoPreRequestTasks = dt.Select("PreRequestTestId is null");
                var dtNoPreRequestTasks = rowsNoPreRequestTasks.Any() ? rowsNoPreRequestTasks.CopyToDataTable() : dt.Clone();
                DataView dvNoPreRequestTasks = new DataView(dtNoPreRequestTasks);
                dvNoPreRequestTasks.Sort = "Contribution DESC";
                dtNoPreRequestTasks = dvNoPreRequestTasks.ToTable();

                //Select and Order Test Requests With PreRequest
                var rowsWithPreRequestTasks = dt.Select("PreRequestTestId is not null");
                var dtWithPreRequestTasks = rowsWithPreRequestTasks.Any() ? rowsWithPreRequestTasks.CopyToDataTable() : dt.Clone();
                DataView dvWithPreRequestTasks = new DataView(dtWithPreRequestTasks);
                dvWithPreRequestTasks.Sort = "Contribution";
                dtWithPreRequestTasks = dvWithPreRequestTasks.ToTable();

                var dtSuitableRequests = dtNoPreRequestTasks.Copy();

                for (int j = 0; j < dtWithPreRequestTasks.Rows.Count; j++)
                {
                    for (int i = dtSuitableRequests.Rows.Count - 1; i >= 0; i--)
                    {
                        if ((double)dtSuitableRequests.Rows[i]["Contribution"] > (double)dtWithPreRequestTasks.Rows[j]["Contribution"]
                            || (int)dtSuitableRequests.Rows[i]["TestId"] == (int)dtWithPreRequestTasks.Rows[j]["PreRequestTestId"]
                            )
                        {
                            var row = dtSuitableRequests.NewRow();
                            row.ItemArray = (object?[])dtWithPreRequestTasks.Rows[j].ItemArray.Clone();
                            dtSuitableRequests.Rows.InsertAt(row, i+1);
                            break;
                        }
                    }
                }

                foreach (DataRow row in dtSuitableRequests.Rows)
                {
                    if (row["PreRequestTestId"] != DBNull.Value) {
                        var preRequestTestRow= dtSuitableRequests.Select("TestId =" + row["PreRequestTestId"]).FirstOrDefault();
                        if ((sbyte)preRequestTestRow["IsSelected"] == 0)
                            continue;
                    }

                    var res = resources.Where(x => x.TestType == (TestTypes)row["TestType"]).FirstOrDefault();

                    if (res.Promised + (int)row["TimeRequired"] <= res.Available)
                    {
                        res.Promised += (int)row["TimeRequired"];
                        row["IsSelected"] = 1;
                    }
                }
                return dtSuitableRequests;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetApprovedTestRequestsInfo(out int totalNumber, out int totalImportanceLevel)
        {
            var approvedTests = _testRequestRepository.GetApprovedTestRequests();
            totalNumber = (int)approvedTests?.Count;
            totalImportanceLevel = (int)approvedTests.Sum(x => (int)x.ImportanceLevel);

            var unfinishedTests = _testRequestRepository.GetUnfinishedTestRequests();
            totalNumber += (int)unfinishedTests?.Count;
            totalImportanceLevel += (int)unfinishedTests?.Sum(x => (int)x.ImportanceLevel);
        }

        private PerformanceVm CalculatePerformanceValues(List<TestRequestsVm> tests, List<ResourcesVm> resources)
        {
            var totalNumber = 0;
            var totalImportanceLevel = 0;
            GetApprovedTestRequestsInfo(out totalNumber, out totalImportanceLevel);

            var resourceUsages = new List<ResourcesVm>() { };

            foreach (var res in resources)
            {
                resourceUsages.Add(
                    new ResourcesVm()
                    {
                        TestType = res.TestType,
                        Available = res.Available,
                        Promised = tests.Where(x => x.TestType == res.TestType).Sum(x => (int)x.LeftTimeRequired)
                    });
            }

            var performance = new PerformanceVm()
            {
                TotalNumber = totalNumber,
                PromisedNumber = tests.Count,
                TotalImportanceLevel = totalImportanceLevel,
                PromisedImportanceLevel = tests.Sum(x => (int)x.ImportanceLevel),
                PromisedTime = tests.Sum(x => (int)x.LeftTimeRequired),
                Resources = resourceUsages
            };

            return performance;
        }

        public async Task<int> CreateAsync(CreateVm createVm)
        {
            ControlCreatePlan(GetUserName(), IsUserManager());
            var planPeriod = new PlanPeriod()
            {
                PeriodStart = DateOnly.FromDateTime(DateTime.Today),
                PlannedPeriodEnd = DateOnly.FromDateTime(DateTime.Today.AddDays(14)),
                AppliedType = createVm.AppliedType,
                CreationDate = DateTime.Now,
                CreatedBy = GetUserName()
            };

            var appliedPlan = createVm?.PlanTypes?.Where(x => x.AppliedType == createVm.AppliedType).FirstOrDefault();

            planPeriod.PlanPeriodResource = new List<PlanPeriodResource>();

            foreach (var res in createVm?.Resources)
            {
                var planResource = _mapper.Map<PlanPeriodResource>(res);
                planResource.CreationDate = DateTime.Now;
                planResource.CreatedBy = GetUserName();
                planResource.Promised = (int)appliedPlan?.TestRequests.Where(x => x.TestType == res.TestType).Sum(x => (int)x.LeftTimeRequired);
                planPeriod?.PlanPeriodResource.Add(planResource);
            }

            planPeriod.PlanTestRequests = new List<PlanTestRequest>();

            foreach (var test in appliedPlan?.TestRequests)
            {
                var testPlan = new PlanTestRequest()
                {
                    TestRequestId = test.Id,
                    TimeRequired=(int)test.LeftTimeRequired,
                    LeftTimeRequired=(int)test.LeftTimeRequired,
                    CreationDate = DateTime.Now,
                    CreatedBy = GetUserName()
                };

                var testReq= await _testRequestRepository.GetAsync(test.Id);
                testReq.Status = TestRequestStatus.PLANNED;

                testPlan.TestRequest = testReq;

                planPeriod?.PlanTestRequests.Add(testPlan);
            }

            planPeriod.PlanPerformance = new PlanPerformance
            {
                TotalNumber = appliedPlan.Performance.TotalNumber,
                TotalImportanceLevel = appliedPlan.Performance.TotalImportanceLevel,
                PromisedNumber = appliedPlan.TestRequests.Count(),
                PromisedImportanceLevel = (int)appliedPlan?.TestRequests.Sum(x => (int)x.ImportanceLevel),
                PromisedTime = (int)appliedPlan?.TestRequests.Sum(x => (int)x.LeftTimeRequired),
                CreationDate = DateTime.Now,
                CreatedBy = GetUserName()
            };
            
            await _planRepository.AddAsync(planPeriod);
            return planPeriod.Id;
        }

        public async Task<CurrentPlanVm> GetCurrentPlan() {
            var plan = _planRepository.GetCurrentPlan();
            if (plan == null)
            {
                return new CurrentPlanVm()
                {
                    InfoMessage="Aktif planlama dönemi bulunamadı!"
                };
            }
            var planTestRequests = _planTestRequestRepository.GetPlanTestRequests(plan.Id);
            var testRequests = new List<TestRequestsVm>();

            foreach (var req in planTestRequests)
            {
                var testReq = await _testRequestRepository.GetAsync(req.TestRequestId);
                testRequests.Add(new TestRequestsVm()
                {
                    Id = req.TestRequestId,
                    PlanTestRequestId=req.Id,
                    Name = testReq.Name,
                    TestType = testReq.TestType,
                    ImportanceLevel = testReq.ImportanceLevel,
                    CreatedBy = testReq.CreatedBy,
                    CreationDate = testReq.CreationDate,
                    TimeRequired = req.TimeRequired,
                    Assignee = req.Assignee,
                    LeftTimeRequired = req.LeftTimeRequired
                });
            }

            return new CurrentPlanVm()
            {
                Id=plan.Id,
                PeriodStart = plan.PeriodStart,
                PlannedPeriodEnd = plan.PlannedPeriodEnd,
                TestRequests = testRequests,
                CanFinish=_validations.CanFinishPlan(IsUserManager(), plan.ActualPeriodEnd)
            };
        }

        public async Task<UpdateTestVm> GetUpdateTestVmAsync(int planRequestId)
        {
            var planRequest = await _planTestRequestRepository.GetAsync(planRequestId);
            var request = await _testRequestRepository.GetAsync(planRequest.TestRequestId);

            var reqVm = _mapper.Map<UpdateTestVm>(request);
            reqVm.HasFile = HasFile(request);
            reqVm.PlanTestRequestId = planRequest.Id;
            reqVm.TimeRequired = planRequest.TimeRequired;
            reqVm.LeftTimeRequired = planRequest.LeftTimeRequired;
            reqVm.Assignee = planRequest.Assignee;

            if (request.PreRequestId != null)
            {
                var preReq = await _testRequestRepository.GetAsync((int)request.PreRequestId);
                reqVm.PreRequestTestIdentifier = preReq.Id + "-" + preReq.Name + "(" + preReq.Status.GetDisplayName() + ")";
            }

            return reqVm;
        }

        private bool HasFile(TestRequest? request)
        {
            return request?.File?.Length > 0;
        }

        public async Task UpdateTestAsync(UpdateTestVm updateVm)
        {
            var planRequest = await _planTestRequestRepository.GetAsync(updateVm.PlanTestRequestId);
            
            planRequest.LeftTimeRequired = updateVm.LeftTimeRequired;
            planRequest.Assignee = GetUserName();
            planRequest.LastUpdateDate = DateTime.Now;
            planRequest.LastUpdatedBy = GetUserName();

            await _planTestRequestRepository.UpdateAsync(planRequest);
        }

        public async Task FinishAsync(int id)
        {
            var plan = await _planRepository.GetAsync(id);
            _validations.IsPlanExist(id, plan);
            _validations.IsUserAuthorizedForFinishPlan(GetUserName(),IsUserManager(), plan?.ActualPeriodEnd);

            plan.ActualPeriodEnd = DateOnly.FromDateTime(DateTime.Now);

            var planResource = (List<PlanPeriodResource>?)_resourcesRepository.GetPlanResources(id);

            var finishedImportanceLevel = 0;
            var finishedNumber = 0;
            var finishedTime = 0;

            var planTestRequests = _planTestRequestRepository.GetPlanTestRequests(id);

            foreach (var req in planTestRequests)
            {
                var testReq = await _testRequestRepository.GetAsync(req.TestRequestId);
                _validations.IsRequestExist(req.TestRequestId, testReq);
                testReq.LeftTimeRequired = req.LeftTimeRequired;
                if (req.LeftTimeRequired == 0)
                {
                    testReq.Status = TestRequestStatus.FINISHED;
                    finishedImportanceLevel += (int)testReq.ImportanceLevel;
                    finishedNumber++;
                }
                else
                    testReq.Status = TestRequestStatus.UNFINISHED;

                finishedTime += req.TimeRequired - req.LeftTimeRequired;
                var resource = planResource.Where(x => x.TestType == testReq.TestType).FirstOrDefault();
                resource.Used += req.TimeRequired - req.LeftTimeRequired;

                req.TestRequest = testReq;
            }
            //TODO:test
            plan.PlanPeriodResource = planResource;

            var performance = _performanceRepository.GetPlanPerformance(id);
            performance.FinishedImportanceLevel = finishedImportanceLevel;
            performance.FinishedNumber = finishedNumber;
            performance.FinishedTime = finishedTime;

            await _planRepository.UpdateAsync(plan);
        }

        public async Task<DetailVm> GetDetailVmAsync(int id)
        {
            var plan = await _planRepository.GetAsync(id);
            _validations.IsPlanExist(id, plan);

            var planTestRequests = _planTestRequestRepository.GetPlanTestRequests(id);
            var testRequests = new List<TestRequestsVm>();

            foreach (var req in planTestRequests)
            {
                var testReq = await _testRequestRepository.GetAsync(req.TestRequestId);
                testRequests.Add(new TestRequestsVm()
                {
                    Id = req.TestRequestId,
                    Name = testReq.Name,
                    TestType = testReq.TestType,
                    ImportanceLevel = testReq.ImportanceLevel,
                    CreatedBy = testReq.CreatedBy,
                    CreationDate = testReq.CreationDate,
                    TimeRequired = testReq.TimeRequired,
                    //LeftTimeRequired = testReq.LeftTimeRequired,
                    Assignee = req.Assignee,
                    LeftTimeRequired = testReq.LeftTimeRequired
                });
            }

            var resources = _resourcesRepository.GetPlanResources(id);

            var performance = _performanceRepository.GetPlanPerformance(id);

            //var performanceVm = _mapper.Map<PerformanceVm>(performance);
            //if (performanceVm.PromisedNumber>0)
              //  performanceVm.ActualNumberPercentage = performanceVm.FinishedNumber * 100 / performanceVm.PromisedNumber;

            return new DetailVm()
            {
                AppliedType = plan.AppliedType,
                PeriodStart = plan.PeriodStart,
                PlannedPeriodEnd = plan.PlannedPeriodEnd,
                ActualPeriodEnd = plan.ActualPeriodEnd,
                TestRequests = testRequests,
                Resources = _mapper.Map<List<ResourcesVm>>(resources),
                Performance = _mapper.Map<PerformanceVm>(performance)
            };
        }

    }
}