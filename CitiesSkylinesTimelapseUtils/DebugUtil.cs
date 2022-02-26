namespace CitiesSkylinesTimelapseUtils
{
    public class DebugUtil
    {

        private readonly string className;
        private readonly bool showMessages;
        private const bool defaultShowMessages = false;

        public DebugUtil(string className, bool showMessages = defaultShowMessages)
        {
            this.className = className;
            this.showMessages = showMessages;
        }

        public void Log(string message)
        {
            if (showMessages)
            {
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, $"{className}: {message}");
            }
        }
    }
}
