using ColossalFramework;

namespace TimelapseUtils
{
    public static class DebugUtils
    {

        public static SavedBool HideDebugMessages = new SavedBool("hideDebugMessages", TimelapseUtilsMod.settingsFileName, true, true);

        public static void Log(string message)
        {
            if (HideDebugMessages.value) return;

            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, message);
        }
    }
}
