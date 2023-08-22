using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using PlanAndTrack.Application.Models;
using PlanAndTrack.Application.Models.TestRequest;
using PlanAndTrack.Application.Repositories;
using PlanAndTrack.Application.Services.Interfaces;
using PlanAndTrack.Application.Validations;
using PlanAndTrack.Domain.Entities;
using PlanAndTrack.Domain.Enums;

namespace PlanAndTrack.Application.Services
{
    public class TestRequestService:ITestRequestService
    {
        private readonly ITestRequestRepository _repository;
        private readonly IHttpContextAccessor? _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IValidations _validations;

        public TestRequestService(ITestRequestRepository repository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IValidations validations)
        {
            _repository = repository;
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

        public void ControlEditRequest(int id, TestRequest? request, string? userName)
        {
            _validations.IsRequestExist(id, request);
            _validations.IsRequestEditAuthorized(id, request.CreatedBy, userName, request.Status);
        }


        public void ControlApproveRequest(int id, TestRequest? request, string? userName, bool isUserManager)
        {
            _validations.IsRequestExist(id, request);
            _validations.IsRequestApprovedAuthorized(id, request.CreatedBy, request.Status, userName, isUserManager);
        }

        public async Task<int> CreateAsync(CreateVm createVm)
        {
            var request = _mapper.Map<TestRequest>(createVm);
            request.CreationDate = DateTime.Now;
            request.CreatedBy = GetUserName();
            request.Status = TestRequestStatus.NEW;
            
            if (createVm.FormFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await createVm.FormFile.CopyToAsync(memoryStream);
                    request.File = memoryStream.ToArray();
                }
            }

            await _repository.AddAsync(request);

            return request.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var request = await GetRequestAsync(id);
            ControlEditRequest(id, request, GetUserName());
            if (request.Status == TestRequestStatus.NEW)
                await _repository.DeleteAsync(request);
            else {
                request.Status = TestRequestStatus.CANCELLED;
                await _repository.UpdateAsync(request);
            }
        }

        public async Task<List<TestRequestVm>> GetAllAsync()
        {
            var requests = await _repository.GetAllAsync();
            List<TestRequestVm> list = new(requests.Count);
            foreach (var req in requests)
            {
                var reqVm=_mapper.Map<TestRequestVm>(req);
                reqVm.HasFile = HasFile(req); 
                reqVm.CanEdit = _validations.CanEdit(req.CreatedBy, req.Status, GetUserName());
                reqVm.CanDelete = _validations.CanDelete(req.CreatedBy, req.Status, GetUserName());
                reqVm.CanApprove = _validations.CanApprove(req.Status, IsUserManager());
                list.Add(reqVm);
            }
            return list.OrderByDescending(x=>x.Id).ToList();
        }

        public async Task<UpdateVm> GetUpdateVmAsync(int id)
        {
            var request = await _repository.GetAsync(id);
            ControlEditRequest(id, request, GetUserName());

            var reqVm= _mapper.Map<UpdateVm>(request);
            reqVm.HasFile = HasFile(request);

            var requests = _repository.GetUserTestRequests(GetUserName());
            reqVm.PreRequests = _mapper.Map<List<TestRequestVm>>(requests);

            return reqVm;
        }

        private async Task<TestRequest> GetRequestAsync(int id)
        {
            var request = await _repository.GetAsync(id);
            return request;
        }

        public async Task UpdateAsync(UpdateVm updateVm)
        {
            var request = await GetRequestAsync(updateVm.Id);
            ControlEditRequest(updateVm.Id, request, GetUserName());

            request.Name = updateVm.Name;
            request.Description = updateVm.Description;
            request.ImportanceLevel = updateVm.ImportanceLevel;
            request.TestType = updateVm.TestType;
            request.PreRequestId = updateVm.PreRequestId;
            if (request.Status != TestRequestStatus.NEW)
            {
                request.Status = TestRequestStatus.UPDATED;
                request.TimeRequired = null;
                request.LeftTimeRequired = null;
            }
            request.LastUpdateDate = DateTime.Now;

            if (updateVm.FormFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await updateVm.FormFile.CopyToAsync(memoryStream);
                    request.File = memoryStream.ToArray();
                }
            }

            await _repository.UpdateAsync(request);
        }

        public async Task<byte[]?> GetFileBytesAsync(int id)
        {
            var request = await _repository.GetAsync(id);
            return request?.File;
        }

        public CreateVm GetCreateVm()
        {
            var requests =  _repository.GetUserTestRequests(GetUserName());
            return new CreateVm()
            {
                PreRequests= _mapper.Map<List<TestRequestVm>>(requests)
            };
        }

        private bool HasFile(TestRequest? request) {
            return request?.File?.Length > 0;
        }

        public async Task<ApproveVm> GetApproveVmAsync(int id)
        {
             var request = await _repository.GetAsync(id);
            ControlApproveRequest(id, request, GetUserName(), IsUserManager());

            var reqVm = _mapper.Map<ApproveVm>(request);
            reqVm.HasFile = HasFile(request);

            if (request.PreRequestId != null) {
                var preReq = await _repository.GetAsync((int)request.PreRequestId);
                reqVm.PreRequestTestIdentifier=preReq.Id +"-"+preReq.Name + "(" + preReq.Status.GetDisplayName()+")";

                reqVm.PreRequestTestStatus = preReq.Status;
            }

            return reqVm;
        }

        public async Task ApproveAsync(ApproveVm approveVm)
        {
            var request = await GetRequestAsync(approveVm.Id);
            ControlApproveRequest(approveVm.Id, request, GetUserName(), IsUserManager());

            request.TimeRequired = approveVm.TimeRequired;
            request.LeftTimeRequired = approveVm.TimeRequired;
            request.Status = TestRequestStatus.APPROVED;
            request.LastUpdateDate = DateTime.Now;

            await _repository.UpdateAsync(request);
        }

        public async Task RejectAsync(int id)
        {
            var request = await GetRequestAsync(id);
            ControlApproveRequest(id, request, GetUserName(), IsUserManager());


            request.Status = TestRequestStatus.REJECTED;
            request.LastUpdateDate = DateTime.Now;

            await _repository.UpdateAsync(request);
        }

        public async Task<DetailVm> GetDetailVmAsync(int id)
        {
            var request = await _repository.GetAsync(id);

            var reqVm = _mapper.Map<DetailVm>(request);
            reqVm.HasFile = HasFile(request);

            if (request?.PreRequestId != null && request.PreRequestId > 0) {

                var preRequestTest = await _repository.GetAsync((int)request.PreRequestId);
                if(preRequestTest!=null)
                    reqVm.PreRequestTest = preRequestTest.Id + "-" + preRequestTest.Name;
            }

            return reqVm;
        }
    }
}

