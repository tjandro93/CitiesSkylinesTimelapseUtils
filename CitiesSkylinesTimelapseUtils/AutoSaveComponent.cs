using UnityEngine;

namespace CitiesSkylinesTimelapseUtils
{
    public class AutoSaveComponent : MonoBehaviour
    {

        private int count = 0;

        public void Start()
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "AutoSaveComponent.Start()");

            InvokeRepeating("Save", 5, 5);
        }

        public void Save()
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "AutoSaveComponent.Save(), count: " + count);

            count++;

            if (count > 10)
            {
                Stop();
            }
        }

        public void Stop()
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "AutoSaveComponent.Stop(), count: " + count);

            CancelInvoke();
        }
    }
}
