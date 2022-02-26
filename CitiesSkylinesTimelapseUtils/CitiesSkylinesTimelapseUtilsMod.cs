using ICities;
using System;

namespace CitiesSkylinesTimelapseUtils
{
    public class CitiesSkylinesTimelapseUtilsMod : IUserMod
    {

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
            var autoSaveConfig = AutoSaveConfig.Instance;
            var group = helper.AddGroup("Auto Save Settings");
            group.AddCheckbox("Enable auto save", autoSaveConfig.Enabled, (isChecked) => autoSaveConfig.ChangeEnabled(isChecked));
            group.AddTextfield("Auto save interval (in seconds)", autoSaveConfig.AutoSaveInterval.ToString(), (value) => { }, HandleAutoSaveIntervalChange);
        }

        public OnTextSubmitted HandleAutoSaveIntervalChange = (string value) =>
        {
            int interval;
            try
            {
                interval = int.Parse(value);
                if (interval < 1)
                {
                    interval = AutoSaveConfig.DefaultAutoSaveInterval;
                }
            }
            catch (Exception)
            {
                interval = AutoSaveConfig.DefaultAutoSaveInterval;
            }

            AutoSaveConfig.Instance.ChangeAutoSaveInterval(interval);
        };
    }
}
