using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PlanAndTrack.Domain.Entities;
using PlanAndTrack.Domain.Enums;

namespace PlanAndTrack.Application.Models.Plan
{
    public class CreateVm
    {
        public int Id { get; set; }

        //[Required]
        //[BindProperty, DataType(DataType.Date)]
        //[Display(Name = "Başlangıç Tarihi")]
        //public DateTime PeriodStart { get; set; }

        public bool CanBeCreated { get; set; }

        public List<ResourcesVm>? Resources { get; set; }

        public List<PlanTypesVm>? PlanTypes { get; set; }
        public PlanTypes AppliedType { get; set; }
    }

}

