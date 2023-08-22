using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using PlanAndTrack.Domain.Enums;

namespace PlanAndTrack.Application.Models.Plan
{
    public class UpdateTestVm
    {
        public int Id { get; set; }

        public int PlanTestRequestId { get; set; }

        [Display(Name = "Talep Kısa İsim")]
        public string? Name { get; set; }

        [Display(Name = "İşin Tanımı")]
        public string? Description { get; set; }

        [Display(Name = "Önem Derecesi")]
        public ImportanceLevels ImportanceLevel { get; set; }

        [Display(Name = "Test Tipi")]
        public TestTypes TestType { get; set; }

        [Display(Name = "Ekli Doküman")]
        public bool HasFile { get; set; }

        [Display(Name ="Öncesinde Tamamlanması Gereken Test Talebi")]
        public int? PreRequestId { get; set; }

        [Display(Name = "Öncesinde Tamamlanması Gereken Test Talebi")]
        public string? PreRequestTestIdentifier { get; set; }

        [Display(Name = "Talep Sahibi")]
        public string? CreatedBy { get; set; }

        [Display(Name = "Öngörülen İş Yükü")]
        public int TimeRequired { get; set; }

        [Required]
        [Display(Name = "Kalan İş Yükü")]
        public int LeftTimeRequired { get; set; }

        [Display(Name = "İşlem Yapan")]
        public string? Assignee { get; set; }
    }
}

