using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Logger
{
    public class ProcessManager
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        static extern bool GetKernelObjectSecurity(IntPtr Handle, int securityInformation, [Out] byte[] pSecurityDescriptor, uint nLength, out uint lpnLengthNeeded);

        public static RawSecurityDescriptor GetProcessSecurityDescriptor(IntPtr processHandle)
        {
            const int DACL_SECURITY_INFORMATION = 0x00000004;
            byte[] psd = new byte[0];
            uint bufSizeNeeded;

            GetKernelObjectSecurity(processHandle, DACL_SECURITY_INFORMATION, psd, 0, out bufSizeNeeded);
            if (bufSizeNeeded < 0 || bufSizeNeeded > short.MaxValue)
            {
                throw new Win32Exception();
            }

            if (!GetKernelObjectSecurity(processHandle, DACL_SECURITY_INFORMATION, psd = new byte[bufSizeNeeded], bufSizeNeeded, out bufSizeNeeded))
            {
                throw new Win32Exception();
            }


            return new RawSecurityDescriptor(psd, 0);
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        static extern bool SetKernelObjectSecurity(IntPtr Handle, int securityInformation, [In] byte[] pSecurityDescriptor);

        public static void SetProcessSecurityDescriptor(IntPtr processHandle, RawSecurityDescriptor dacl)
        {
            const int DACL_SECURITY_INFORMATION = 0x00000004;
            byte[] rawsd = new byte[dacl.BinaryLength];
            dacl.GetBinaryForm(rawsd, 0);
            if (!SetKernelObjectSecurity(processHandle, DACL_SECURITY_INFORMATION, rawsd))
            {
                throw new Win32Exception();
            }
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentProcess();

        [Flags]
        public enum ProcessAccessRights
        {
            PROCESS_CREATE_PROCESS = 0x0080, //  для создания процесса.
            PROCESS_CREATE_THREAD = 0x0002, //  для создания потока.
            PROCESS_DUP_HANDLE = 0x0040, //  для дублирования дескриптора с помощью DuplicationHandle.
            PROCESS_QUERY_INFORMATION = 0x0400, //  для получения определенной информации о процессе, такой как его токен, код выхода и класс приоритета (см. OpenProcessToken, GetExitCodeProcess, GetPriorityClass и IsProcessInJob).
            PROCESS_QUERY_LIMITED_INFORMATION = 0x1000, //  для получения определенной информации о процессе (см. QueryFullProcessImageName). Дескриптору, имеющему право доступа PROCESS_QUERY_INFORMATION, автоматически предоставляется PROCESS_QUERY_LIMITED_INFORMATION. Windows Server 2003 и Windows XP/2000: это право доступа не поддерживается.
            PROCESS_SET_INFORMATION = 0x0200, //  установить определенную информацию о процессе, например его класс приоритета (см. SetPriorityClass).
            PROCESS_SET_QUOTA = 0x0100, //  для установки ограничений памяти с помощью SetProcessWorkingSetSize.
            PROCESS_SUSPEND_RESUME = 0x0800, //  для приостановки или возобновления процесса.
            PROCESS_TERMINATE = 0x0001, //  для завершения процесса с помощью TerminateProcess.
            PROCESS_VM_OPERATION = 0x0008, //  для выполнения операции над адресным пространством процесса (см. VirtualProtectEx и WriteProcessMemory).
            PROCESS_VM_READ = 0x0010, //  для чтения памяти в процессе с помощью ReadProcessMemory.
            PROCESS_VM_WRITE = 0x0020, //  для записи в память в процессе с использованием WriteProcessMemory.
            DELETE = 0x00010000, //  для удаления объекта.
            READ_CONTROL = 0x00020000, //  для чтения информации в дескрипторе безопасности объекта, не включая информацию в SACL. Для чтения или записи SACL необходимо запросить право доступа ACCESS_SYSTEM_SECURITY. Дополнительные сведения см. в разделе «Права доступа SACL».
            SYNCHRONIZE = 0x00100000, //  право на использование объекта для синхронизации. Это позволяет потоку ждать, пока объект не перейдет в сигнальное состояние.
            WRITE_DAC = 0x00040000, //  для изменения DACL в дескрипторе безопасности объекта.
            WRITE_OWNER = 0x00080000, //  для смены владельца в дескрипторе безопасности объекта.
            STANDARD_RIGHTS_REQUIRED = 0x000f0000,
            PROCESS_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | SYNCHRONIZE | 0xFFF), // все возможные права доступа для объекта процесса.
        }
        public void block()
        {
            IntPtr hProcess = GetCurrentProcess();

            var dacl = GetProcessSecurityDescriptor(hProcess);

            dacl.DiscretionaryAcl.InsertAce(0, new CommonAce(AceFlags.None, AceQualifier.AccessDenied, (int)ProcessAccessRights.PROCESS_ALL_ACCESS, new SecurityIdentifier(WellKnownSidType.WorldSid, null), false, null));

            SetProcessSecurityDescriptor(hProcess, dacl);
        }
    }
}
