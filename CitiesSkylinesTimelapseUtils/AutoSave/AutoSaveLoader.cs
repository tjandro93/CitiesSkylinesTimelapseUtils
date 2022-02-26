﻿using ICities;
using UnityEngine;

namespace CitiesSkylinesTimelapseUtils.AutoSave
{
    public class AutoSaveLoader : LoadingExtensionBase
    {

        private DebugUtil debug;
        private GameObject autoSaveGameObject;
        private AutoSaveComponent autoSaveComponent;

        public AutoSaveLoader()
        {
            debug = new DebugUtil("AutoSaveLoader");
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            if (loadingManager.currentMode == AppMode.Game)
            {
                debug.Log("OnLevelLoaded()");

                autoSaveGameObject = new GameObject();
                autoSaveGameObject.AddComponent<AutoSaveComponent>();
                autoSaveComponent = autoSaveGameObject.GetComponent<AutoSaveComponent>();
                autoSaveComponent.SerializableData = managers.serializableData;
                autoSaveComponent.Threading = managers.threading;
            }
        }


        public override void OnLevelUnloading()
        {
            debug.Log("OnLevelUnloading()");
            if (autoSaveComponent != null)
            {
                debug.Log("autoSaveComponent exists; stopping it");
                autoSaveComponent.Stop();
            }

            AutoSaveConfig.ResetSubscriptions();
        }
    }
}