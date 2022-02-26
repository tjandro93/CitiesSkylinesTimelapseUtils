using ICities;
using UnityEngine;

namespace CitiesSkylinesTimelapseUtils
{
    public class AutoSaveComponent : MonoBehaviour
    {

        public ISerializableData SerializableData { get; set; }
        public IThreading Threading { get; set; }


        public void Start()
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "AutoSaveComponent.Start()");

            InvokeRepeating("Save", 15, 15);
        }

        public void Save()
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "AutoSaveComponent.Save()");

            var saveName = string.Format("AutoSaveComponent {0:yyyy-MM-dd HH-mm}", Threading.renderTime);
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "saveName " + saveName);

            SerializableData.SaveGame(saveName);
        }

        public void Stop()
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "AutoSaveComponent.Stop()");

            CancelInvoke();
        }
    }
}
