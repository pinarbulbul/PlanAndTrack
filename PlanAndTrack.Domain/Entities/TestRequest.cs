using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using PlanAndTrack.Domain.Enums;

namespace PlanAndTrack.Domain.Entities
{
    public class TestRequest : BaseEntity
    {
        [Required]
        [Display(Name = "Talep Kısa İsim")]
        public string? Name { get; set; }

        [Required]
        [Display(Name="İşin Tanımı")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Önem Derecesi")]
        public ImportanceLevels ImportanceLevel { get; set; }

        [Column("TestType")]
        public string TestTypeString
        {
            get { return TestType.ToString(); }
            private set { TestType = value.ParseEnum<TestTypes>(); }
        }

        [NotMapped]
        [Required]
        [Display(Name = "Test Tipi")]
        public TestTypes TestType { get; set; }

        [Column("Status")]
        public string StatusString
        {
            get { return Status.ToString(); }
            private set { Status = value.ParseEnum<TestRequestStatus>(); }
        }

        [NotMapped]
        [Required]
        [Display(Name = "Durum")]
        public TestRequestStatus Status { get; set; }

        public byte[]? File { get; set; }

        public int? TimeRequired { get; set; }

        public int? LeftTimeRequired { get; set; }

        public int? PreRequestId { get; set; }

        [ForeignKey("PreRequestId")]
        public TestRequest? PreRequestTestRequest { get; set; }

        public List<PlanTestRequest>? TestRequestPlans { get; set; }
    }
}

