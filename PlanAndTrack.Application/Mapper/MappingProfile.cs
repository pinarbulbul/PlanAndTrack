using System;
using AutoMapper;
using PlanAndTrack.Application.Models.Plan;
using PlanAndTrack.Application.Models.TestRequest;
using PlanAndTrack.Domain.Entities;

namespace PlanAndTrack.Application.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //TestRequest
            CreateMap<TestRequestVm, TestRequest>().ReverseMap();
            CreateMap<Models.TestRequest.CreateVm, TestRequest>().ReverseMap();
            CreateMap<UpdateVm, TestRequest>().ReverseMap();
            CreateMap<Models.TestRequest.DetailVm, TestRequest>().ReverseMap();
            CreateMap<ApproveVm, TestRequest>().ReverseMap();

            //Plan
            CreateMap<PlanHeaderVm, PlanPeriod>().ReverseMap();
            CreateMap<ResourcesVm, PlanPeriodResource>().ReverseMap();
            CreateMap<PerformanceVm, PlanPerformance>().ReverseMap();
            CreateMap<TestRequestsVm, TestRequest>().ReverseMap();

            CreateMap<UpdateTestVm, TestRequest>().ReverseMap();
            
        }
    }
}

