using System;
using System.ComponentModel.DataAnnotations;
using PlanAndTrack.Domain.Enums;

namespace PlanAndTrack.Application.Models.Plan
{
    public class TestRequestsVm
    {
        [Display(Name = "Test Id")]
        public int Id { get; set; }

        public int PlanTestRequestId { get; set; }

        [Display(Name = "Talep Kısa İsim")] 
        public string? Name { get; set; }

        [Display(Name = "Önem Derecesi")]
        public ImportanceLevels ImportanceLevel { get; set; }

        [Display(Name = "Test Tipi")]
        public TestTypes TestType { get; set; }

        [Display(Name = "Talep Sahibi")]
        public string? CreatedBy { get; set; }

        [Display(Name = "Talep Oluşturma Tarihi")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Öngörülen İş Yükü")]
        public int? TimeRequired { get; set; }

        [Display(Name = "Kalan İş Yükü")]
        public int? LeftTimeRequired { get; set; }

        [Display(Name = "İşlem Yapan")]
        public string? Assignee { get; set; }

    }
}

