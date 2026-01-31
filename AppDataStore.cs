using System;
using System.IO;
using Newtonsoft.Json;

namespace Customer
{
    public static class AppDataStore
    {
        private static readonly string FilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appdata.json");

        public static AppData Current { get; private set; } = new AppData();

        public static void Load()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    Current = CreateDefaultData();
                    Save();
                    return;
                }

                string json = File.ReadAllText(FilePath);
                Current = JsonConvert.DeserializeObject<AppData>(json) ?? new AppData();
            }
            catch
            {
                Current = CreateDefaultData();
                Save();
            }
        }

        public static void Save()
        {
            string json = JsonConvert.SerializeObject(Current, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        private static AppData CreateDefaultData()
        {
            var data = new AppData();

            // مستخدمين افتراضيين
            data.Users.Add(new UserInfo
            {
                UserName = "Admin",
                Password = "2222",
                Role = UserRole.Admin
            });

            data.Users.Add(new UserInfo
            {
                UserName = "Employee1",
                Password = "1111",
                Role = UserRole.Employee
            });

            return data;
        }
    }
}
