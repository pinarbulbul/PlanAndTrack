using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace PlanAndTrack.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Talep Sahibi")]
        public string? CreatedBy { get; set; }

        [Required]
        [Display(Name = "Talep Oluşturma Tarihi")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Son Güncelleyen")]
        public string? LastUpdatedBy { get; set; }

        [Display(Name = "Son Güncelleme Tarihi")]
        public DateTime? LastUpdateDate { get; set; }
    }
}

