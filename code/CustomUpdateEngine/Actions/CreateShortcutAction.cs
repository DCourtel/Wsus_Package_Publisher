using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace CustomUpdateEngine
{
    public class CreateShortcutAction : GenericAction
    {
        public CreateShortcutAction(string xmlFragment)
        {
            LogInitialization(xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("Target"))
                throw new ArgumentException("Unable to find token : Target");
            Target = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("ShortcutName"))
                throw new ArgumentException("Unable to find token : ShortcutName");
            ShortcutName = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("Description"))
                throw new ArgumentException("Unable to find token : Description");
            Description = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("Icon"))
                throw new ArgumentException("Unable to find token : Icon");
            Icon = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("Arguments"))
                throw new ArgumentException("Unable to find token : Arguments");
            Arguments = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("WorkingDirectory"))
                throw new ArgumentException("Unable to find token : WorkingDirectory");
            WorkingDirectory = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("WindowStyle"))
                throw new ArgumentException("Unable to find token : WindowStyle");
            WindowStyle = reader.ReadElementContentAsInt();
            if (!reader.ReadToFollowing("DesktopTarget"))
                throw new ArgumentException("Unable to find token : DesktopTarget");
            DesktopTarget = reader.ReadElementContentAsInt();
            if (!reader.ReadToFollowing("IsDesktopLocation"))
                throw new ArgumentException("Unable to find token : IsDesktopLocation");
            IsDesktopLocation = bool.Parse(reader.ReadElementContentAsString());
            if (!reader.ReadToFollowing("IsPersoLocation"))
                throw new ArgumentException("Unable to find token : IsPersoLocation");
            IsPersoLocation = bool.Parse(reader.ReadElementContentAsString());
            if (!reader.ReadToFollowing("PersoLocation"))
                throw new ArgumentException("Unable to find token : PersoLocation");
            PersoLocation = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("AbortIfTargetDontExist"))
                throw new ArgumentException("Unable to find token : AbortIfTargetDontExist");
            AbortIfTargetDontExist = bool.Parse(reader.ReadElementContentAsString());

            LogCompletion();
        }

        public string Target { get; private set; }
        public string ShortcutName { get; private set; }
        public string Description { get; private set; }
        public string Icon { get; private set; }
        public string Arguments { get; private set; }
        public string WorkingDirectory { get; private set; }
        public int WindowStyle { get; private set; }
        public int DesktopTarget { get; private set; }
        public bool IsDesktopLocation { get; private set; }
        public bool IsPersoLocation { get; private set; }
        public string PersoLocation { get; private set; }
        public bool AbortIfTargetDontExist { get; private set; }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running CreateShortcutAction. Target = " + this.Target + " and ShortcutName = " + this.ShortcutName);

            try
            {
                string shortcutLocation = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory);

                if (IsDesktopLocation)
                {
                    if (DesktopTarget == 1)
                        shortcutLocation = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                }
                else
                {
                    shortcutLocation = PersoLocation;
                }

                if (!AbortIfTargetDontExist || System.IO.File.Exists(Target))
                    CreateShortcut(Target, ShortcutName, shortcutLocation, Description, Icon, Arguments, WorkingDirectory, WindowStyle);
                else
                    Logger.Write("The Target of the shortcut doesn't exits, aborting");
            }
            catch (Exception ex)
            {
                Logger.Write("Failed to create the shortcut : " + this.ShortcutName + "\r\n" + ex.Message);
            }
            Logger.Write("End of CreateShortcutAction.");
        }

        public static void CreateShortcut(string shortcutTarget, string shortcutName, string shortcutLocation, string shortcutDescription, string shortcutIcon, string shortcutArgunents, string shortcutWorkingDirectory, int shortcutWindowStyle)
        {
            IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
            IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(System.IO.Path.Combine(shortcutLocation, shortcutName + ".lnk"));

            shortcut.TargetPath = shortcutTarget;

            if (!String.IsNullOrEmpty(shortcutDescription))
                shortcut.Description = shortcutDescription;

            if (!String.IsNullOrEmpty(shortcutIcon))
                shortcut.IconLocation = shortcutIcon;

            if (!String.IsNullOrEmpty(shortcutArgunents))
                shortcut.Arguments = shortcutArgunents;

            if (!String.IsNullOrEmpty(shortcutWorkingDirectory))
                shortcut.WorkingDirectory = shortcutWorkingDirectory;

            shortcut.WindowStyle = shortcutWindowStyle;
            shortcut.Save();
            Logger.Write("The shortcut have been created.");
        }
    }
}
