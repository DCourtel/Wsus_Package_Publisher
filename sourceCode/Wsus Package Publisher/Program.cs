using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Wsus_Package_Publisher
{
    static class Program
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //const int SW_HIDE = 0;
            //const int SW_SHOWNORMAL = 1;
            //const int SW_SHOWMINIMIZED = 2;
            //const int SW_SHOWMAXIMIZED = 3;
            //const int SW_SHOWNOACTIVATE = 4;
            const int SW_RESTORE = 9;
            //const int SW_SHOWDEFAULT = 10;

            FrmWsusPackagePublisher instance;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if !DEBUG
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            if (IsWsusConsoleInstalled())
            {
                bool createdNew = true;
                using (System.Threading.Mutex mutex = new System.Threading.Mutex(true, "Wsus Package Publisher", out createdNew))
                {
                    if (createdNew)
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        instance = new FrmWsusPackagePublisher();
                        Application.Run(instance);

                        if (instance.restart)
                            Application.Restart();
                    }
                    else
                    {
                        System.Diagnostics.Process current = System.Diagnostics.Process.GetCurrentProcess();
                        foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName(current.ProcessName))
                        {
                            if (process.Id != current.Id)
                            {
                                IntPtr hWnd = process.MainWindowHandle;

                                if (IsIconic(hWnd))
                                {
                                    ShowWindowAsync(hWnd, SW_RESTORE);
                                }
                                SetForegroundWindow(hWnd);
                                break;
                            }
                        }
                    }
                }
            }
            else
                MessageBox.Show("Unable to find the Wsus Console. Please install it before running this application.\r\nEnsure that the Wsus Console version match the Wsus Server Version.");
#else
            if (IsWsusConsoleInstalled())
            {
                bool createdNew = true;
                using (System.Threading.Mutex mutex = new System.Threading.Mutex(true, "Wsus Package Publisher", out createdNew))
                {
                    if (createdNew)
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        instance = new FrmWsusPackagePublisher();
                        Application.Run(instance);

                        if (instance.restart)
                            Application.Restart();
                    }
                    else
                    {
                        System.Diagnostics.Process current = System.Diagnostics.Process.GetCurrentProcess();
                        foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName(current.ProcessName))
                        {
                            if (process.Id != current.Id)
                            {
                                IntPtr hWnd = process.MainWindowHandle;

                                if (IsIconic(hWnd))
                                {
                                    ShowWindowAsync(hWnd, SW_RESTORE);
                                }
                                SetForegroundWindow(hWnd);
                                break;
                            }
                        }
                    }
                }
            }
            else
                MessageBox.Show("Unable to find the Wsus Console. Please install it before running this application.\r\nEnsure that the Wsus Console version match the Wsus Server Version.");
#endif
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Logger.Write("ThreadException. Something Went Wrong");
            SendLogInformations(e.Exception.Message);
            Environment.Exit(e.Exception.GetHashCode());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Write("UnhandledException. Something Went Wrong");
            Exception ex = null;
            try
            {
                ex = (Exception)e.ExceptionObject;
            }
            catch (Exception) { ex = new Exception("Failed to cast 'UnhandledExceptionEventArgs' to Exception. Nomore informations on what went wrong in WPP."); }

            SendLogInformations(ex.Message);
            Environment.Exit(e.GetHashCode());
        }

        private static void SendLogInformations(string message)
        {
            Logger.Write("**** " + message);
            FrmSendDebugInfo frmSend = new FrmSendDebugInfo();
            frmSend.SendingReason = FrmSendDebugInfo.SendingReasons.UnexpectedError;
            frmSend.ErrorMessage = message;

            frmSend.ShowDialog();
        }

        private static bool IsWsusConsoleInstalled()
        {
            try
            {
                Microsoft.Win32.RegistryKey HKLM = Microsoft.Win32.Registry.LocalMachine;

                Microsoft.Win32.RegistryKey consoleKey3 = HKLM.OpenSubKey("Windows Server Update Services 3.0 SP2", false);
                Microsoft.Win32.RegistryKey consoleKey6 = HKLM.OpenSubKey(@"SOFTWARE\Microsoft\Update Services\Server\Setup", false);

                return (consoleKey3 != null || consoleKey6 != null);
            }
            catch (Exception) { }
            return true;
        }
    }
}
