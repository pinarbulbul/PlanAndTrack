using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PlanAndTrack.Domain.Entities;
using PlanAndTrack.Domain.Enums;

namespace PlanAndTrack.Application.Models.Plan
{
    public class CurrentPlanVm
    {
        public int Id { get; set; }

        [Display(Name = "Başlangıç Tarihi")]
        public DateOnly PeriodStart { get; set; }

        [Display(Name = "Öngörülen Bitiş Tarihi")]
        public DateOnly PlannedPeriodEnd { get; set; }

        [Display(Name = "Planlanan Testler")]
        public List<TestRequestsVm>? TestRequests { get; set; }

        public bool CanFinish { get; set; }

        public string? InfoMessage { get; set; }


    }
}

