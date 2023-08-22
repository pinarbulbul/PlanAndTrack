using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PlanAndTrack.Domain.Entities;
using PlanAndTrack.Domain.Enums;

namespace PlanAndTrack.Application.Models.Plan
{
    public class PlanHeaderVm

    {
        public int Id { get; set; }

        [Display(Name = "Başlangıç Tarihi")]
        public DateOnly PeriodStart { get; set; }

        [Required]
        [Display(Name = "Bitiş Tarihi")]
        public DateOnly PlannedPeriodEnd { get; set; }

        [Required]
        [Display(Name = "Gerçekleşen Bitiş Tarihi")]
        public DateOnly? ActualPeriodEnd { get; set; }

        [Display(Name = "Uygulanan Plan")]
        public PlanTypes AppliedType { get; set; }

        [Display(Name = "Durum")]
        public string Status
        {
            get
            {
                if (ActualPeriodEnd != null)
                    return "Tamamlandı";
                else if (PeriodStart.CompareTo(DateOnly.FromDateTime(DateTime.Now))<=0 && ActualPeriodEnd == null)
                    return "Devam Ediyor";
                else
                    return "";
            }
        }
    }

}

