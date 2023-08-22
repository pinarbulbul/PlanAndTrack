using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PlanAndTrack.Domain.Entities;

namespace PlanAndTrack.Application.Models.Plan
{
    public class PerformanceVm
    {
        public int PlannedPeriodId { get; set; }

        [Display(Name = "Toplam Talep Sayısı")]
        public int TotalNumber { get; set; }

        [Display(Name = "Planlanan Talep Sayısı")]
        public int PromisedNumber { get; set; }

        [Display(Name = "Talep Sayısı Bazında Karşılanan Talep Yüzdesi")]
        public int NumberPercentage
        {
            get
            {
                if (TotalNumber > 0)
                    return PromisedNumber * 100 / TotalNumber;
                else
                    return 0;
            }
        }

        [Display(Name = "Tamamlanan Talep Sayısı")]
        public int FinishedNumber { get; set; }

        [Display(Name = "Talep Sayısı Bazında Gerçekleşen Talep Yüzdesi")]
        public int ActualNumberPercentage
        {
            get
            {
                if (PromisedNumber > 0)
                    return FinishedNumber * 100 / PromisedNumber;
                else
                    return 0;
            }
        }

        [Display(Name = "Toplam Önem Derecesi")]
        public int TotalImportanceLevel { get; set; }

        [Display(Name = "Planlanan Önem Derecesi")]
        public int PromisedImportanceLevel { get; set; }

        [Display(Name = "Taleplerin Önem Miktarı Bazında Karşılanan Önem Yüzdesi")]
        public int ImportanceLevelPercentage
        {
            get
            {
                if (TotalImportanceLevel > 0)
                    return PromisedImportanceLevel * 100 / TotalImportanceLevel;
                else
                    return 0;
            }
        }

        [Display(Name = "Tamamlanan Önem Derecesi")]
        public int FinishedImportanceLevel { get; set; }

        [Display(Name = "Taleplerin Önem Miktarı Bazında Gerçekleşen Önem Yüzdesi")]
        public int ActualImportanceLevelPercentage
        {
            get
            {
                if (PromisedImportanceLevel > 0)
                    return FinishedImportanceLevel * 100 / PromisedImportanceLevel;
                else
                    return 0;
            }
        }

        [Display(Name = "Planlanan Toplam Zaman")]
        public int PromisedTime { get; set; }

        [Display(Name = "Tamamlanan Zaman")]
        public int FinishedTime { get; set; }

        public List<ResourcesVm>? Resources { get; set; }
    }

}

