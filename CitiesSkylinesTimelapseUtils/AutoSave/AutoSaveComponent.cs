using ICities;
using System;
using System.Collections;
using UnityEngine;

namespace CitiesSkylinesTimelapseUtils.AutoSave
{
    public class AutoSaveComponent : MonoBehaviour
    {

        public ISerializableData SerializableData { get; set; }
        public IThreading Threading { get; set; }

        private Coroutine saveCoroutine;


        public void Start()
        {
            DebugUtils.Log("AutoSaveComponent: Start()");

            if (AutoSaveConfig.Enabled.value)
            {
                DebugUtils.Log("AutoSaveComponent: AutoSave already enabled, starting coroutine immediately");
                saveCoroutine = StartCoroutine(Save());
            }

            AutoSaveConfig.EnabledChanged += (o, args) =>
            {
                DebugUtils.Log("AutoSaveComponent: EnabledChanged");
                // start coroutine if we're enabling
                if (args.NewValue)
                {
                    DebugUtils.Log("AutoSaveComponent: EnabledChanged, new Value");
                    if (saveCoroutine != null)
                    {
                        DebugUtils.Log("AutoSaveComponent: EnabledChanged, stopping old coroutine");
                        Stop();
                    }
                    DebugUtils.Log("AutoSaveComponent: EnabledChanged, starting coroutine");
                    saveCoroutine = StartCoroutine(Save());
                }
                // we're disabling, so cancel coroutine only if there is one running
                else if (saveCoroutine != null)
                {
                    DebugUtils.Log("AutoSaveComponent: EnabledChangedto disabled. stopping coroutine");
                    Stop();
                }
            };

            AutoSaveConfig.AutoSaveIntervalChanged += (o, args) =>
            {
                DebugUtils.Log("AutoSaveComponent: AutoSaveInterval changed");
                // if we're already enabled we need to cancel the existing auto save coroutine
                // and start a new one with the new value
                if (saveCoroutine != null)
                {
                    DebugUtils.Log("AutoSaveComponent: AutoSaveInterval changed. Stopping old coroutine");
                    Stop();
                }
                DebugUtils.Log("AutoSaveComponent: AutoSaveInterval changed. Starting new coroutine");
                saveCoroutine = StartCoroutine(Save());
            };
        }

        public IEnumerator Save()
        {
            DebugUtils.Log("AutoSaveComponent: Save()");

            // as long as auto save is enabled, loop
            while (AutoSaveConfig.Enabled.value)
            {
                DebugUtils.Log("AutoSaveComponent: AutoSave Enabled, waiting");
                // delay for the specified auto save interval seconds
                yield return new WaitForSeconds(AutoSaveConfig.AutoSaveInterval.value);
                DebugUtils.Log("AutoSaveComponent: Finished waiting");
                SaveGame();
            }
        }

        public void Stop()
        {
            DebugUtils.Log("AutoSaveComponent: Stop()");

            if (saveCoroutine != null)
            {
                StopCoroutine(saveCoroutine);
            }
        }

        private void SaveGame()
        {
            var saveName = string.Format(AutoSaveConfig.AutoSaveNameFormat.value, DateTime.Now, Threading.renderTime);
            DebugUtils.Log("AutoSaveComponent: saveName " + saveName);
            SerializableData.SaveGame(saveName);
        }
    }
}
