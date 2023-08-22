using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using PlanAndTrack.Domain.Enums;

namespace PlanAndTrack.Domain.Entities
{
    public class PlanPerformance:BaseEntity
    {
        [Required]
        public int PlannedPeriodId { get; set; }

        [ForeignKey("PlannedPeriodId")]
        public PlanPeriod? PlanPeriod { get; set; }

        [Display(Name = "Toplam Talep Sayısı")]
        public int TotalNumber { get; set; }

        [Display(Name = "Planlanan Talep Sayısı")]
        public int PromisedNumber { get; set; }

        [Display(Name = "Tamamlanan Talep Sayısı")]
        public int FinishedNumber { get; set; }

        [Display(Name = "Toplam Önem Derecesi")]
        public int TotalImportanceLevel { get; set; }

        [Display(Name = "Planlanan Önem Derecesi")]
        public int PromisedImportanceLevel { get; set; }

        [Display(Name = "Tamamlanan Önem Derecesi")]
        public int FinishedImportanceLevel { get; set; }

        [Display(Name = "Planlanan Zaman")]
        public int PromisedTime { get; set; }
        
        [Display(Name = "Tamamlanan Zaman")]
        public int FinishedTime { get; set; }


    }
}

