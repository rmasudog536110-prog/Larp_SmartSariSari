namespace Larp.domain.DTO
{
    public class ApprovalItemDto
    {
        public string Type { get; set; } = string.Empty;

        public int ItemId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;

        public DateTime RequestedAt { get; set; }
    }
}