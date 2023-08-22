using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using System.Xml.Linq;
using PlanAndTrack.Domain.Enums;

namespace PlanAndTrack.Domain.Entities
{

    public class PlanPeriod:BaseEntity
    {
        [Required]
        [Display(Name = "Başlangıç Tarihi")]
        public DateOnly PeriodStart { get; set; }

        [Required]
        [Display(Name = "Bitiş Tarihi")]
        public DateOnly PlannedPeriodEnd { get; set; }

        [Display(Name = "Gerçekleşen Bitiş Tarihi")]
        public DateOnly? ActualPeriodEnd { get; set; }

        [Column("AppliedType")]
        public string AppliedTypeString
        {
            get { return AppliedType.ToString(); }
            private set { AppliedType = value.ParseEnum<PlanTypes>(); }
        }

        [NotMapped]
        [Required]
        [Display(Name = "Uygulanan Plan")]
        public PlanTypes AppliedType { get; set; }

        public List<PlanTestRequest>? PlanTestRequests { get; set; }
        public List<PlanPeriodResource>? PlanPeriodResource { get; set; }
        public PlanPerformance? PlanPerformance { get; set; }
    }
}

