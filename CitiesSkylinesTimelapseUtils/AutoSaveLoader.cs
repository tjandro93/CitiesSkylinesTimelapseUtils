using ICities;
using UnityEngine;

namespace CitiesSkylinesTimelapseUtils
{
    public class AutoSaveLoader : LoadingExtensionBase
    {

        private GameObject autoSaveGameObject;
        private AutoSaveComponent autoSaveComponent;

        public override void OnLevelLoaded(LoadMode mode)
        {
            if (mode == LoadMode.LoadGame)
            {
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "AutoSave.OnLevelLoaded()");

                autoSaveGameObject = new GameObject();
                autoSaveGameObject.AddComponent<AutoSaveComponent>();
                autoSaveComponent = autoSaveGameObject.GetComponent<AutoSaveComponent>();
                autoSaveComponent.SerializableData = managers.serializableData;
                autoSaveComponent.Threading = managers.threading;
            }
        }


        public override void OnLevelUnloading()
        {
            if (autoSaveComponent != null)
            {
                autoSaveComponent.Stop();
            }
        }
    }
}
