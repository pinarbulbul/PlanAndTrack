using System;
using System.ComponentModel.DataAnnotations;

namespace PlanAndTrack.Domain.Enums
{
    public enum TestRequestStatus
    {
        [Display(Name="Yeni")]
        NEW,

        [Display(Name = "Güncellendi")]
        UPDATED,

        [Display(Name = "Onaylandı")]
        APPROVED,

        [Display(Name = "Reddedildi")]
        REJECTED,

        [Display(Name = "Planlandı")]
        PLANNED,

        [Display(Name = "Tamamlanmadı")]
        UNFINISHED,

        [Display(Name = "Tamamlandı")]
        FINISHED,

        [Display(Name = "İptal Edildi")]
        CANCELLED

    }
}

