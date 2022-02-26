using CitiesSkylinesTimelapseUtils.AutoSave;
using ColossalFramework;
using ICities;
using System;

namespace CitiesSkylinesTimelapseUtils
{
    public class CitiesSkylinesTimelapseUtilsMod : IUserMod
    {

        public const string settingsFileName = "timelapseUtils";
        public CitiesSkylinesTimelapseUtilsMod()
        {

            try
            {
                DebugUtils.Log("CitiesSkylinesTimelapseUtilsMod: Finding settings file");
                // Creating setting file
                if (GameSettings.FindSettingsFileByName(settingsFileName) == null)
                {
                    DebugUtils.Log("CitiesSkylinesTimelapseUtilsMod: Didn't finding settings file, creating it");
                    GameSettings.AddSettingsFile(new SettingsFile[] { new SettingsFile() { fileName = settingsFileName } });
                    DebugUtils.Log("CitiesSkylinesTimelapseUtilsMod: Settings file created");
                }
                else
                {
                    DebugUtils.Log("CitiesSkylinesTimelapseUtilsMod: Settings file found");
                }
            }
            catch (Exception)
            {
                DebugUtils.Log("CitiesSkylinesTimelapseUtilsMod: Could load/create the setting file.");
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
            var generalGroup = helper.AddGroup("General Settings");
            generalGroup.AddCheckbox("Hide debug messages", DebugUtils.HideDebugMessages.value, (isChecked) => DebugUtils.HideDebugMessages.value = isChecked);

            var autoSaveGroup = helper.AddGroup("Auto Save Settings");
            autoSaveGroup.AddCheckbox("Enable auto save", AutoSaveConfig.Enabled.value, (isChecked) => AutoSaveConfig.ChangeEnabled(isChecked));
            autoSaveGroup.AddTextfield("Auto save interval (in seconds)", AutoSaveConfig.AutoSaveInterval.value.ToString(), (value) => { }, HandleAutoSaveIntervalChange);
            autoSaveGroup.AddTextfield("Auto save format", AutoSaveConfig.AutoSaveNameFormat.value, (value) => AutoSaveConfig.AutoSaveNameFormat.value = value);
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
