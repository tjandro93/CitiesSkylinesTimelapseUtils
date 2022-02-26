using System;

namespace CitiesSkylinesTimelapseUtils
{
    public class AutoSaveConfig
    {
        public static AutoSaveConfig Instance = new AutoSaveConfig();

        private int _autoSaveInterval = DefaultAutoSaveInterval;
        public int AutoSaveInterval
        {
            get => _autoSaveInterval;
            private set => _autoSaveInterval = value;
        }

        public const int DefaultAutoSaveInterval = 60;

        private bool _enabled = true;
        public bool Enabled
        {
            get => _enabled;
            private set => _enabled = value;
        }

        public event EventHandler<AutoSaveIntervalChangeArgs> AutoSaveIntervalChanged;

        public event EventHandler<EnabledChangeArgs> EnabledChanged;

        private DebugUtil debug;

        public AutoSaveConfig()
        {
            debug = new DebugUtil("AutoSaveConfig");
            AutoSaveIntervalChanged += (object o, AutoSaveIntervalChangeArgs args) =>
            {
                debug.Log($"AutoSaveIntervalChanged from {args.OldValue} to {args.NewValue}");
            };

            EnabledChanged += (object o, EnabledChangeArgs args) =>
            {
                debug.Log($"EnabledChanged from {args.OldValue} to {args.NewValue}");
            };
        }

        public void ChangeAutoSaveInterval(int newValue)
        {
            if (newValue > 0 && newValue != AutoSaveInterval)
            {
                var oldValue = AutoSaveInterval;
                AutoSaveInterval = newValue;
                debug.Log("Calling RaiseAutoSaveIntervalChanged");
                RaiseAutoSaveIntervalChanged(new AutoSaveIntervalChangeArgs(oldValue, newValue));
            }
        }

        private void RaiseAutoSaveIntervalChanged(AutoSaveIntervalChangeArgs args)
        {
            debug.Log("Raising AutoSaveIntervalChanged");
            EventHandler<AutoSaveIntervalChangeArgs> raiseEvent = AutoSaveIntervalChanged;

            if (raiseEvent != null)
            {
                raiseEvent(this, args);
            }
        }

        public void ChangeEnabled(bool newValue)
        {
            debug.Log($"newValue: {newValue}, Enabled: {Enabled}");
            if (newValue != Enabled)
            {
                var oldValue = Enabled;
                Enabled = newValue;
                debug.Log("Calling RaiseEnabledChanged");
                RaiseEnabledChanged(new EnabledChangeArgs(oldValue, newValue));
            }
        }

        private void RaiseEnabledChanged(EnabledChangeArgs args)
        {
            debug.Log("Raising EnabledChanged");
            EventHandler<EnabledChangeArgs> raiseEvent = EnabledChanged;

            if (raiseEvent != null)
            {
                raiseEvent(this, args);
            }
        }

        public void ResetSubscriptions()
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
