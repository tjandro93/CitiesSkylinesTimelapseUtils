using CitiesSkylinesTimelapseUtils.AutoSave;
using ColossalFramework;
using ICities;
using System;

namespace CitiesSkylinesTimelapseUtils
{
    public class CitiesSkylinesTimelapseUtilsMod : IUserMod
    {

        public const string settingsFileName = "timelapseUtils";

        private readonly DebugUtil debug;

        public CitiesSkylinesTimelapseUtilsMod()
        {
            debug = new DebugUtil("CitiesSkylinesTimelapseUtilsMod", true);

            try
            {
                debug.Log("Finding settings file");
                // Creating setting file
                if (GameSettings.FindSettingsFileByName(settingsFileName) == null)
                {
                    debug.Log("Didn't finding settings file, creating it");
                    GameSettings.AddSettingsFile(new SettingsFile[] { new SettingsFile() { fileName = settingsFileName } });
                    debug.Log("Settings file created");
                }
                else
                {
                    debug.Log("Settings file found");
                }
            }
            catch (Exception)
            {
                debug.Log("Could load/create the setting file.");
            }
        }

        public string Name
        {
            get { return "CitiesSkylinesTimelapseUtils"; }
        }

        public string Description
        {
            get { return "A simple mod to help in creating timelapses"; }
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            var group = helper.AddGroup("Auto Save Settings");
            group.AddCheckbox("Enable auto save", AutoSaveConfig.Enabled.value, (isChecked) => AutoSaveConfig.ChangeEnabled(isChecked));
            group.AddTextfield("Auto save interval (in seconds)", AutoSaveConfig.AutoSaveInterval.value.ToString(), (value) => { }, HandleAutoSaveIntervalChange);
            group.AddTextfield("Auto save format", AutoSaveConfig.AutoSaveNameFormat.value, (value) => AutoSaveConfig.AutoSaveNameFormat.value = value);
        }

        public OnTextSubmitted HandleAutoSaveIntervalChange = (string value) =>
        {
            int interval;
            try
            {
                interval = int.Parse(value);
                if (interval < 1)
                {
                    interval = AutoSaveConfig.AutoSaveInterval.value;
                }
            }
            catch (Exception)
            {
                interval = AutoSaveConfig.AutoSaveInterval.value;
            }

            AutoSaveConfig.ChangeAutoSaveInterval(interval);
        };
    }
}
