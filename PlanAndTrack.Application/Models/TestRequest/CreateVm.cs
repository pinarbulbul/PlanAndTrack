using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using PlanAndTrack.Domain.Enums;

namespace PlanAndTrack.Application.Models.TestRequest
{
    public class CreateVm
    {
        [Required]
        [Display(Name = "Talep Kısa İsim")]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Yapılacak İşin Tanımı")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Önem Derecesi")]
        public ImportanceLevels ImportanceLevel { get; set; }

        [Required]
        [Display(Name = "Test Tipi")]
        public TestTypes TestType { get; set; }
        
        [Display(Name = "Test Dokümanı")]
        public IFormFile? FormFile { get; set; }

        [Display(Name = "Öncesinde Tamamlanması Gereken Test Talebi")]
        public int? PreRequestId { get; set; }

        public List<TestRequestVm>? PreRequests { get; set; }

    }
}

