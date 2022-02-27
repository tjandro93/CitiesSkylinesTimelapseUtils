using ICities;
using UnityEngine;

namespace TimelapseUtils.AutoSave
{
    public class AutoSaveLoader : LoadingExtensionBase
    {

        private GameObject autoSaveGameObject;
        private AutoSaveComponent autoSaveComponent;

        public override void OnLevelLoaded(LoadMode mode)
        {
            if (loadingManager.currentMode == AppMode.Game)
            {
                DebugUtils.Log("AutoSaveLoader: OnLevelLoaded()");

                autoSaveGameObject = new GameObject();
                autoSaveGameObject.AddComponent<AutoSaveComponent>();
                autoSaveComponent = autoSaveGameObject.GetComponent<AutoSaveComponent>();
                autoSaveComponent.SerializableData = managers.serializableData;
                autoSaveComponent.Threading = managers.threading;
            }
        }


        public override void OnLevelUnloading()
        {
            DebugUtils.Log("AutoSaveLoader: OnLevelUnloading()");
            if (autoSaveComponent != null)
            {
                DebugUtils.Log("AutoSaveLoader: autoSaveComponent exists; stopping it");
                autoSaveComponent.Stop();
            }

            AutoSaveConfig.ResetSubscriptions();
        }
    }
}
