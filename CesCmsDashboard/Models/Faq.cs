using System;
using System.ComponentModel.DataAnnotations;

namespace CesCmsDashboard.Models;

public class Faq
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "A question is required.")]
    [MaxLength(250, ErrorMessage = "Question cannot exceed 250 characters.")]
    public string Question { get; set; } = string.Empty;

    [Required(ErrorMessage = "An answer is required.")]
    public string Answer { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }
    public bool IsPublished { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
