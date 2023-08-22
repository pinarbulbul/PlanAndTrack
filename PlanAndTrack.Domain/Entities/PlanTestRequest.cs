using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace PlanAndTrack.Domain.Entities
{
    public class PlanTestRequest:BaseEntity
    {
        [Required]
        public int TestRequestId { get; set; }

        [ForeignKey("TestRequestId")]
        public TestRequest? TestRequest { get; set; }

        [Required]
        public int PlannedPeriodId { get; set; }

        [ForeignKey("PlannedPeriodId")]
        public PlanPeriod? PlanPeriod { get; set; }

        public int TimeRequired { get; set; }

        public int LeftTimeRequired { get; set; }

        public string? Assignee { get; set; }
    }
}

