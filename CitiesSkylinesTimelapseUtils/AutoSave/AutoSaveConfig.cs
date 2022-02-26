using ColossalFramework;
using System;

namespace CitiesSkylinesTimelapseUtils.AutoSave
{
    public static class AutoSaveConfig
    {
        public static SavedBool Enabled = new SavedBool("autoSaveEnabled", CitiesSkylinesTimelapseUtilsMod.settingsFileName, true, true);
        public static SavedInt AutoSaveInterval = new SavedInt("autoSaveInterval", CitiesSkylinesTimelapseUtilsMod.settingsFileName, 60, true);
        public static SavedString AutoSaveNameFormat = new SavedString("autoSaveNameFormat", CitiesSkylinesTimelapseUtilsMod.settingsFileName, "AutoSave {0:yyyy-MM-dd HH-mm-ss}", true);

        public static event EventHandler<AutoSaveIntervalChangeArgs> AutoSaveIntervalChanged;

        public static event EventHandler<EnabledChangeArgs> EnabledChanged;

        static AutoSaveConfig()
        {
            AutoSaveIntervalChanged += (o, args) =>
            {
                DebugUtils.Log($"AutoSaveConfig: AutoSaveIntervalChanged from {args.OldValue} to {args.NewValue}");
            };

            EnabledChanged += (o, args) =>
                    {
                        DebugUtils.Log($"AutoSaveConfig: EnabledChanged from {args.OldValue} to {args.NewValue}");
                    };
        }

        public static void ChangeAutoSaveInterval(int newValue)
        {
            if (newValue > 0 && newValue != AutoSaveInterval)
            {
                var oldValue = AutoSaveInterval;
                AutoSaveInterval.value = newValue;
                DebugUtils.Log("AutoSaveConfig: Calling RaiseAutoSaveIntervalChanged");
                RaiseAutoSaveIntervalChanged(new AutoSaveIntervalChangeArgs(oldValue, newValue));
            }
        }

        private static void RaiseAutoSaveIntervalChanged(AutoSaveIntervalChangeArgs args)
        {
            DebugUtils.Log("AutoSaveConfig: Raising AutoSaveIntervalChanged");
            EventHandler<AutoSaveIntervalChangeArgs> raiseEvent = AutoSaveIntervalChanged;

            if (raiseEvent != null)
            {
                raiseEvent(null, args);
            }
        }

        public static void ChangeEnabled(bool newValue)
        {
            DebugUtils.Log($"AutoSaveConfig: newValue: {newValue}, Enabled: {Enabled}");
            if (newValue != Enabled)
            {
                var oldValue = Enabled;
                Enabled.value = newValue;
                DebugUtils.Log("AutoSaveConfig: Calling RaiseEnabledChanged");
                RaiseEnabledChanged(new EnabledChangeArgs(oldValue, newValue));
            }
        }

        private static void RaiseEnabledChanged(EnabledChangeArgs args)
        {
            DebugUtils.Log("AutoSaveConfig: Raising EnabledChanged");
            EventHandler<EnabledChangeArgs> raiseEvent = EnabledChanged;

            if (raiseEvent != null)
            {
                raiseEvent(null, args);
            }
        }

        public static void ResetSubscriptions()
        {
            AutoSaveIntervalChanged = null;
            EnabledChanged = null;
        }
    }

    public class AutoSaveIntervalChangeArgs : EventArgs
    {
        public int OldValue { get; set; }
        public int NewValue { get; set; }

        public AutoSaveIntervalChangeArgs(int oldValue, int newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    public class EnabledChangeArgs : EventArgs
    {
        public bool OldValue { get; set; }
        public bool NewValue { get; set; }

        public EnabledChangeArgs(bool oldValue, bool newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}