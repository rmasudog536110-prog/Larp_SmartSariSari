using System;
using System.IO;
using System.Text.Json;

namespace Larp.domain.Entities
{
    public class DeviceConfig
    {
        public int CompanyId { get; set; }
        public int DeviceId { get; set; }

        public bool IsActivated => CompanyId != 0;

        private static string ConfigPath =>
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                "SmartSariSari",
               "device.json");

        public static DeviceConfig Load()
        {
            if (!File.Exists(ConfigPath))
                return new DeviceConfig();

            var json = File.ReadAllText(ConfigPath);

            return JsonSerializer.Deserialize<DeviceConfig>(json)
                   ?? new DeviceConfig();
        }

        public void Save()
        {
            var directory = Path.GetDirectoryName(ConfigPath);

            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllText(
                   ConfigPath,
                   JsonSerializer.Serialize(this));
        }
    }
}