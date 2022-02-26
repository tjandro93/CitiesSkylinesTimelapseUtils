using ICities;
using UnityEngine;

namespace CitiesSkylinesTimelapseUtils
{
    public class AutoSaveLoader : LoadingExtensionBase
    {

        private GameObject autoSaveGameObject;

        public override void OnLevelLoaded(LoadMode mode)
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "AutoSave.OnLevelLoaded()");

            autoSaveGameObject = new GameObject();

            autoSaveGameObject.AddComponent<AutoSaveComponent>();
        }

        public override void OnReleased()
        {
            autoSaveGameObject.GetComponent<AutoSaveComponent>().Stop();
        }
    }
}
