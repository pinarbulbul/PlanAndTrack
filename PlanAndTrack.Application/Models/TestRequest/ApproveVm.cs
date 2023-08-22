using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using PlanAndTrack.Domain.Enums;

namespace PlanAndTrack.Application.Models.TestRequest
{
    public class ApproveVm
    {
        public int Id { get; set; }

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

        [Display(Name = "Öncesinde Tamamlanması Gereken Test Talebi Durumu")]
        public TestRequestStatus? PreRequestTestStatus { get; set; }

        [Required]
        [Display(Name = "İşin Tamamlanması İçin Öngörülen İş Gücü (Saat)")]
        public int? TimeRequired { get; set; }

    }
}

