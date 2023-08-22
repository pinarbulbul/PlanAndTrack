using System;
using System.ComponentModel.DataAnnotations;
using PlanAndTrack.Domain.Enums;

namespace PlanAndTrack.Application.Models.TestRequest
{
    public class TestRequestVm
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

        [Display(Name = "Talep Durumu")]
        public TestRequestStatus Status { get; set; }

        [Display(Name = "Talep Sahibi")]
        public string? CreatedBy { get; set; }

        [Display(Name = "Talep Oluşturma Tarihi")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Öngörülen İş Yükü")]
        public int? TimeRequired { get; set; }

        [Display(Name = "Ekli Doküman")]
        public bool HasFile { get; set; }

        public string TestIdentifier
        {
            get
            {
                return Id +"-"+ Name;
            }
        }

        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanApprove { get; set; }

    }
}

