using System;
using System.ComponentModel.DataAnnotations;
using PlanAndTrack.Domain.Entities;
using PlanAndTrack.Domain.Enums;

namespace PlanAndTrack.Application.Models.Plan
{
    public class DetailVm
    {
        public int Id { get; set; }

        [Display(Name = "Başlangıç Tarihi")]
        public DateOnly PeriodStart { get; set; }

        [Display(Name = "Öngörülen Bitiş Tarihi")]
        public DateOnly PlannedPeriodEnd { get; set; }

        [Display(Name = "Gerçekleşen Bitiş Tarihi")]
        public DateOnly? ActualPeriodEnd { get; set; }

        [Display(Name = "Uygulanan Plan")]
        public PlanTypes AppliedType { get; set; }

        public string Status
        {
            get
            {
                if (ActualPeriodEnd != null)
                    return "Tamamlandı";
                else if (PeriodStart.CompareTo(DateOnly.FromDateTime(DateTime.Now)) <= 0 && ActualPeriodEnd == null)
                    return "Devam Ediyor";
                else
                    return "";
            }
        }


        [Display(Name = "Planlanan Testler")]
        public List<TestRequestsVm>? TestRequests { get; set; }


        [Display(Name = "Kaynaklar")]
        public List<ResourcesVm>? Resources { get; set; }


        [Display(Name = "Performans Ölçütleri")]
        public PerformanceVm? Performance { get; set; }

    }

}

