namespace Larp.domain.DTO
{
    public class CreateCompanyRequest
    {
        public string CompanyCode { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string? SubscriptionPlan { get; set; }       // Basic / Premium / Enterprise
        public DateTime? SubscriptionExpiry { get; set; }

        public string ServerName { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string CredentialKey { get; set; } = string.Empty;
    }

    public record RestoreRequest(string FileName, string ConfirmCompanyName);
    public class ReactivateRequest
    {
        public string Plan { get; set; } = string.Empty;
        public DateTime Expiry { get; set; }
    }
}