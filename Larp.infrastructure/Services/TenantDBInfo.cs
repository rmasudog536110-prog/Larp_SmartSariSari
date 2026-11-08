using System;
using System.Collections.Generic;
using System.Text;

namespace Larp.infrastructure.Services
{
    public class TenantDBInfo
    {
        public string ServerName { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string CredentialKey { get; set; } = string.Empty;
    }
}
