using System;

namespace Larp.domain.DTO
{
    public class TermsAndConditionsDto
    {
        public int TermsAndConditionsId { get; set; }

        public string Version { get; set; } = "1.0";

        public DateTime EffectiveDate { get; set; }

        public string Content { get; set; } = "";

        public bool IsActive { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public class SaveTermsAndConditionsRequest
    {
        public string Version { get; set; } = "1.0";

        public DateTime EffectiveDate { get; set; }

        public string Content { get; set; } = "";

        public bool IsActive { get; set; } = true;
    }
}