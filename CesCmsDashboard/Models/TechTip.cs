using System;
using System.ComponentModel.DataAnnotations;

namespace CesCmsDashboard.Models
{
    public class TechTip
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Slug { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public string? VideoUrl { get; set; }

        [MaxLength(100)]
        public string? Category { get; set; }

        public bool IsPublished { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
