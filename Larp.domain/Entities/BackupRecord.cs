using System;
using System.Collections.Generic;
using System.Text;

namespace Larp.domain.Entities
{
    public class BackupRecord
    {
        public string FileName { get; set; }
        public DateTime CreatedAt { get; set; }
        public long SizeBytes { get; set; }
        public string StorageLocation { get; set; } // blob URL or local path
    }
}
