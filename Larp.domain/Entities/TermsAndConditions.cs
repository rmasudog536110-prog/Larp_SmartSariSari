using System;

namespace Larp.domain.Entities
{
    public class TermsAndConditions
    {
        public int TermsAndConditionsId { get; set; }

        public string Version { get; set; } = "1.0";

        public DateTime EffectiveDate { get; set; } = DateTime.Today;

        public string Content { get; set; } = "";

        public bool IsActive { get; set; } = true;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}