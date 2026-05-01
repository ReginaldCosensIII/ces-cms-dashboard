using System;
using System.ComponentModel.DataAnnotations;

namespace CesCmsDashboard.Models
{
    public class ActivityLog
    {
        public int Id { get; set; }

        [Required]
        public string ActionType { get; set; } = string.Empty;

        [Required]
        public string EntityType { get; set; } = string.Empty;

        [Required]
        public string EntityTitle { get; set; } = string.Empty;

        // Timestamp MUST be set using DateTime.UtcNow
        public DateTime Timestamp { get; set; }
    }
}
