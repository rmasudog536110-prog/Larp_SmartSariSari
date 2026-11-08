using System;
using System.Collections.Generic;
using System.Text;

namespace Larp.domain.Entities
{
    public class Device
    {

        public int DeviceId { get; set; }
        public string DeviceCode { get; set; } = string.Empty;
        public string DeviceName { get; set; } = string.Empty;
        public string DeviceType { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public Company? Company { get; set; }

        public string ActivationCode { get; set; }  
        public bool IsActivated { get; set; }
        public DateTime? ActivatedAt { get; set; }
        public DateTime? LastSeenAt { get; set; }

    }

}
