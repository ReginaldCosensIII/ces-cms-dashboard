using System;
using System.ComponentModel.DataAnnotations;

namespace CesCmsDashboard.Models
{
    public class TechTip
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A title is required.")]
        [MaxLength(250, ErrorMessage = "Title cannot exceed 250 characters.")]
        public string Title { get; set; } = string.Empty;


        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; } = string.Empty;

        [RegularExpression(@"^(https?\:\/\/)?(www\.)?(youtube\.com|youtu\.be|vimeo\.com)\/.+$", ErrorMessage = "Must be a valid YouTube or Vimeo URL.")]
        public string? VideoUrl { get; set; }

        [MaxLength(100)]
        public string? Category { get; set; }

        public bool IsPublished { get; set; } = false;

        public int DisplayOrder { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
