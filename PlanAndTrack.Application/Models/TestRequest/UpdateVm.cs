using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using PlanAndTrack.Domain.Enums;

namespace PlanAndTrack.Application.Models.TestRequest
{
    public class UpdateVm
    {
        public int Id { get; set; }

        [Display(Name = "Talep Durumu")]
        public TestRequestStatus Status { get; set; }

        [Required]
        [Display(Name = "Talep Kısa İsim")]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "İşin Tanımı")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Önem Derecesi")]
        public ImportanceLevels ImportanceLevel { get; set; }

        [Required]
        [Display(Name = "Test Tipi")]
        public TestTypes TestType { get; set; }

        [Display(Name = "Ekli Doküman")]
        public bool HasFile { get; set; }

        [Display(Name = "Test Dokümanı")]
        public IFormFile? FormFile { get; set; }

        [Display(Name ="Öncesinde Tamamlanması Gereken Test Talebi")]
        public int? PreRequestId { get; set; }

        public List<TestRequestVm>? PreRequests { get; set; }
    }
}

