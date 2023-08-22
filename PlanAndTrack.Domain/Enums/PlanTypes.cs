using System;
using System.ComponentModel.DataAnnotations;

namespace PlanAndTrack.Domain.Enums
{
    public enum PlanTypes
    {
        [Display(Name = "Önem Derecesi Öncelikli")]
        IMPORTANCE,

        [Display(Name = "Talep Sayısı Öncelikli")]
        NUMBER,
    }
}

