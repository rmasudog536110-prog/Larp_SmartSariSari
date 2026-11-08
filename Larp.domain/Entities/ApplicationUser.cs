using Microsoft.AspNetCore.Identity;

namespace Larp.domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;

        public int? CompanyId { get; set; }

        public Company? Company { get; set; }

        public bool IsActive { get; set; } = true;
    }
}