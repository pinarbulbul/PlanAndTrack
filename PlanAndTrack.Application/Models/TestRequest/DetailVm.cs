using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using PlanAndTrack.Domain.Enums;

namespace PlanAndTrack.Application.Models.TestRequest
{
    public class DetailVm
    {
        public int Id { get; set; }

        [Display(Name = "Talep Durumu")]
        public TestRequestStatus Status { get; set; }

        [Display(Name = "Talep Kısa İsim")]
        public string? Name { get; set; }

        [Display(Name = "İşin Tanımı")]
        public string? Description { get; set; }

        [Display(Name = "Önem Derecesi")]
        public ImportanceLevels ImportanceLevel { get; set; }

        [Display(Name = "Test Tipi")]
        public TestTypes TestType { get; set; }

        [Display(Name = "Öngörülen İş Yükü")]
        public int? TimeRequired { get; set; }

        [Display(Name = "Kalan İş Yükü")]
        public int? LeftTimeRequired { get; set; }

        [Display(Name = "Ekli Doküman")]
        public bool HasFile { get; set; }

        [Display(Name = "Test Dokümanı")]
        public IFormFile? FormFile { get; set; }

        [Display(Name = "Öncesinde Tamamlanması Gereken Test Talebi")]
        public string? PreRequestTest{ get; set; }
    }
}

