using System;
using System.Windows.Forms;
using Krypton.Toolkit;
namespace Customer
{
    internal static class Program
    {
        public static string LoggedInUserName { get; set; }
        public static UserRole LoggedInUserRole { get; set; }

        [STAThread]
        static void Main()
        {
         
          


            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            KryptonManager kryptonManager = new KryptonManager();
            kryptonManager.GlobalPaletteMode = PaletteMode.Microsoft365Black;

            // 👈 مهم جداً
            AppDataStore.Load();

            Application.Run(new LoginForm());
        }
    }
}
