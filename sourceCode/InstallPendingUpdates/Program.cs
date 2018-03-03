using System;
using System.Collections.Generic;
using System.Text;
using WUApiLib;
using System.Management;

namespace InstallPendingUpdates
{
    class Program
    {
        private struct arguments
        {
            internal string SearchString { get; set; }
            internal bool IncludeUpdatesWithRebootRequire { get; set; }
            internal bool CancelIfRebootRequire { get; set; }
        }

        private static arguments _arguments = new arguments();

        static void Main(string[] args)
        {
            Logger.Initialize(@"C:\Windows\", "InstallPendingUpdates.log", Logger.Destination.File);
            Logger.Write("===========================================================================================================");
            Logger.Write("Starting with " + args.Length + " arguments.");
            for (int i = 0; i < args.Length; i++)
            {
                Logger.Write("Arguments N° " + i + " : " + args[i]);
            }
            _arguments = GetArguments(args);
            Logger.Write(_arguments.SearchString);
            Logger.Write(_arguments.IncludeUpdatesWithRebootRequire.ToString());
            Logger.Write(_arguments.CancelIfRebootRequire.ToString());
            
            if (!_arguments.CancelIfRebootRequire || !IsRebootRequireBeforeInstallation())
            {
                UpdateCollection pendingUpdates = SearchUpdates();
                InstallUpdates(pendingUpdates);
                System.Threading.Thread.Sleep(3000);
                ReportNow();
            }
            Logger.Write("End of InstallPendingUpdates.");
        }

        private static bool IsRebootRequireBeforeInstallation()
        {
            try
            {
                UpdateSession uSession = new UpdateSession();
                IUpdateInstaller uInstaller = uSession.CreateUpdateInstaller();
                Logger.Write("Require Reboot before installation : " + uInstaller.RebootRequiredBeforeInstallation.ToString());
                return uInstaller.RebootRequiredBeforeInstallation;
            }
            catch (Exception ex)
            {
                Logger.Write("Problem when determining if a reboot is require before installation. " + ex.Message);
                return true;
            }
        }

        private static UpdateCollection SearchUpdates()
        {
            Logger.Write("Starting to search for Pending Updates.");
            UpdateCollection installableUpdates = new UpdateCollection();
            UpdateCollection pendingUpdates = new UpdateCollection();

            try
            {
                UpdateSession uSession = new UpdateSession();
                IUpdateSearcher uSearcher = uSession.CreateUpdateSearcher();

                uSearcher.ServerSelection = ServerSelection.ssManagedServer;
                uSearcher.IncludePotentiallySupersededUpdates = false;
                uSearcher.Online = false;

                ISearchResult sResult = uSearcher.Search(_arguments.SearchString);
                if (sResult.ResultCode == OperationResultCode.orcSucceeded && sResult.Updates.Count != 0)
                    pendingUpdates = sResult.Updates;

                Logger.Write("Found " + pendingUpdates.Count + " Pending Updates.");
                foreach (IUpdate update in pendingUpdates)
                {
                    if (update.InstallationBehavior.RebootBehavior == InstallationRebootBehavior.irbNeverReboots || _arguments.IncludeUpdatesWithRebootRequire == true)
                    {
                        Logger.Write("Selecting : " + update.Title);
                        installableUpdates.Add(update);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write("Problem when seraching for Pending Updates. " + ex.Message);
            }
            Logger.Write("Found " + installableUpdates.Count + " Installable update(s).");
            return installableUpdates;
        }

        private static void InstallUpdates(UpdateCollection installableUpdates)
        {
            Logger.Write("Trying to install update(s).");
            try
            {
                UpdateSession uSession = new UpdateSession();
                UpdateDownloader uDownloader = uSession.CreateUpdateDownloader();
                IUpdateInstaller uInstaller = uSession.CreateUpdateInstaller();

                if (installableUpdates.Count != 0)
                {
                    Logger.Write(installableUpdates.Count + " update(s) to install.");
                    uInstaller.ClientApplicationID = "InstallPendingUpdates";
                    uInstaller.AllowSourcePrompts = false;

                    uDownloader.ClientApplicationID = "InstallPendingUpdates";
                    uDownloader.Updates = installableUpdates;
                    Logger.Write("Starting to download update(s).");
                    uDownloader.Download();
                    Logger.Write("Download finnish.");

                    uInstaller.Updates = installableUpdates;
                    Logger.Write("Starting to install " + installableUpdates.Count + " update(s).");
                    IInstallationResult installResult = uInstaller.Install();
                    OperationResultCode resultCode = installResult.ResultCode;
                    Logger.Write("Finnish to install update(s). Result : " + resultCode.ToString());
                }
                else
                    Logger.Write("No udpdate to install.");
            }
            catch (Exception ex)
            {
                Logger.Write("Problem when installing update(s). " + ex.Message);
            }
        }

        private static void ReportNow()
        {
            Logger.Write("Sending ReportNow.");
            try
            {
                ConnectionOptions connectoptions = new ConnectionOptions();
                
                System.Management.ManagementScope mgmtScope = new System.Management.ManagementScope(@"\\.\ROOT\CIMV2", connectoptions);
                mgmtScope.Connect();
                System.Management.ObjectGetOptions objectGetOptions = new System.Management.ObjectGetOptions();
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath("Win32_Process");
                System.Management.ManagementClass processClass = new System.Management.ManagementClass(mgmtScope, mgmtPath, objectGetOptions);
                System.Management.ManagementBaseObject inParams = processClass.GetMethodParameters("Create");
                ManagementClass startupInfo = new ManagementClass("Win32_ProcessStartup");
                startupInfo.Properties["ShowWindow"].Value = 0;

                inParams["CommandLine"] = "WuAuClt.exe /ReportNow";
                inParams["ProcessStartupInformation"] = startupInfo;

                processClass.InvokeMethod("Create", inParams, null);
            }
            catch (Exception ex)
            {
                Logger.Write("Problem when doing ReportNow. " + ex.Message);
            }
            Logger.Write("ReportNow done.");
        }

        private static arguments GetArguments(string[] args)
        {
            arguments commandLine = new arguments();
            commandLine.IncludeUpdatesWithRebootRequire = false;
            commandLine.SearchString = "IsInstalled=0 And IsHidden=0 And Type='Software'";
            commandLine.CancelIfRebootRequire = true;

            if (args != null && args.Length != 0)
            {
                foreach (string arg in args)
                {
                    string currentArg = string.Empty;
                    if (arg.Contains("="))
                        currentArg = arg.Substring(0, arg.IndexOf('=') + 1).ToLower();
                    switch (currentArg)
                    {
                        case "searchstring=":
                            commandLine.SearchString = arg.Substring(arg.IndexOf('=') + 1);
                            break;
                        case "includeupdateswithrebootrequire=":
                            if (arg.Substring(arg.IndexOf('=') + 1).ToLower() == "true")
                                commandLine.IncludeUpdatesWithRebootRequire = true;
                            break;
                        case "cancelifrebootrequire=":
                            if (arg.Substring(arg.IndexOf('=') + 1).ToLower() == "false")
                                commandLine.CancelIfRebootRequire = false;
                            break;
                        default:
                            break;
                    }
                }
            }
            return commandLine;
        }
    }
}
