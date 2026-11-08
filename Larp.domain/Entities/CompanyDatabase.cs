using System;
using System.Collections.Generic;
using System.Text;

namespace Larp.domain.Entities
{
    public class CompanyDatabase
    {

        public int CompanyDatabaseId { get; set; }
        public int CompanyId { get; set; }
        public string ServerName { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string CredentialKey { get; set; } = string.Empty;
        public bool isActive { get; set; } = true;
        public Company? Company { get; set; }


    }
}
 