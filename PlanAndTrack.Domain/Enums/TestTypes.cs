using System;
using System.ComponentModel.DataAnnotations;

namespace PlanAndTrack.Domain.Enums
{
    public enum TestTypes
    {
        [Display(Name = "Seçilmemiş")]
        Undefined = 0,

        [Display(Name ="Manuel Fonksiyonel Test")]
        Functional = 1,

        [Display(Name = "Performans Testi")]
        Performance = 2,

        [Display(Name = "Güvenlik Testi")]
        Security = 3,
    }
}

