using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PlanAndTrack.Domain.Entities;
using PlanAndTrack.Domain.Enums;

namespace PlanAndTrack.Application.Models.Plan
{
    public class ResourcesVm
    {
        public int PlannedPeriodId { get; set; }

        [Required]
        [Display(Name = "Test Tipi")]
        public TestTypes TestType { get; set; }

        [Display(Name = "Kullanılabilir")]
        public int Available { get; set; }

        [Display(Name = "Kullanılması Planlanan")]
        public int Promised { get; set; }

        [Display(Name = "Kullanılmış")]
        public int Used { get; set; }

    }

}

