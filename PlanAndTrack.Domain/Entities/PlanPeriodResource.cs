using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using PlanAndTrack.Domain.Enums;

namespace PlanAndTrack.Domain.Entities
{
    public class PlanPeriodResource:BaseEntity
    {
        public int PlannedPeriodId { get; set; }

        [ForeignKey("PlannedPeriodId")]
        public PlanPeriod? PlanPeriod { get; set; }

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

        [Required]
        [Display(Name = "Kullanılabilir")]
        public int Available { get; set; }

        [Display(Name = "Kullanılması Planlanan")]
        public int Promised { get; set; }

        [Display(Name = "Kullanılmış")]
        public int Used { get; set; }

    }
}

