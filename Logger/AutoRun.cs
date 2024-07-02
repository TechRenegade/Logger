using Microsoft.Win32;

namespace Logger
{
    public class AutoRun
    {
        public void AddToStartup()
        {
            string appName = "Logger";
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            using (RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                if (rk.GetValue(appName) == null)
                {
                    rk.SetValue(appName, exePath);
                }
            }
        }
    }
}