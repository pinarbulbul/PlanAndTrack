using System;
using System.ComponentModel.DataAnnotations;

namespace PlanAndTrack.Domain.Enums
{
    public enum ImportanceLevels
    {
        [Display(Name="Önemsiz")]
        NotImportant = 1,

        [Display(Name = "Biraz Önemli")]
        Little = 2,

        [Display(Name = "Orta Önemli")]
        Moderate = 3,

        [Display(Name = "Fazla Önemli")]
        Very = 4,

        [Display(Name = "Çok Fazla Önemli")]
        TooMuch = 5
    }
}

