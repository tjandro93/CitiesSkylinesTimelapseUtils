namespace CitiesSkylinesTimelapseUtils
{
    public class DebugUtil
    {

        private string className;

        public DebugUtil(string className)
        {
            this.className = className;
        }

        public void Log(string message)
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, $"{className}: {message}");
        }
    }
}
