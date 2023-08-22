using System;
using PlanAndTrack.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PlanAndTrack.Application.Models.Plan
{
    public class PlanTypesVm
        {
            [Display(Name = "Uygulanan Plan")]
            public PlanTypes AppliedType { get; set; }

            public List<TestRequestsVm>? TestRequests { get; set; }
            public PerformanceVm? Performance { get; set; }

        }
    }


