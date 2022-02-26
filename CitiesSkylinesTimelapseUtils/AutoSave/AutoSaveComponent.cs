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
        private DebugUtil debug;

        public AutoSaveComponent()
        {
            debug = new DebugUtil("AutoSaveComponent", true);
        }

        public void Start()
        {
            debug.Log("Start()");

            if (AutoSaveConfig.Enabled.value)
            {
                debug.Log("AutoSave already enabled, starting coroutine immediately");
                saveCoroutine = StartCoroutine(Save());
            }

            AutoSaveConfig.EnabledChanged += (o, args) =>
            {
                debug.Log("EnabledChanged");
                // start coroutine if we're enabling
                if (args.NewValue)
                {
                    debug.Log("EnabledChanged, new Value");
                    if (saveCoroutine != null)
                    {
                        debug.Log("EnabledChanged, stopping old coroutine");
                        Stop();
                    }
                    debug.Log("EnabledChanged, starting coroutine");
                    saveCoroutine = StartCoroutine(Save());
                }
                // we're disabling, so cancel coroutine only if there is one running
                else if (saveCoroutine != null)
                {
                    debug.Log("EnabledChangedto disabled. stopping coroutine");
                    Stop();
                }
            };

            AutoSaveConfig.AutoSaveIntervalChanged += (o, args) =>
            {
                debug.Log("AutoSaveInterval changed");
                // if we're already enabled we need to cancel the existing auto save coroutine
                // and start a new one with the new value
                if (saveCoroutine != null)
                {
                    debug.Log("AutoSaveInterval changed. Stopping old coroutine");
                    Stop();
                }
                debug.Log("AutoSaveInterval changed. Starting new coroutine");
                saveCoroutine = StartCoroutine(Save());
            };
        }

        public IEnumerator Save()
        {
            debug.Log("Save()");

            // as long as auto save is enabled, loop
            while (AutoSaveConfig.Enabled.value)
            {
                debug.Log("AutoSave Enabled, waiting");
                // delay for the specified auto save interval seconds
                yield return new WaitForSeconds(AutoSaveConfig.AutoSaveInterval.value);
                debug.Log("Finished waiting");
                SaveGame();
            }
        }

        public void Stop()
        {
            debug.Log("Stop()");

            if (saveCoroutine != null)
            {
                StopCoroutine(saveCoroutine);
            }
        }

        private void SaveGame()
        {
            var saveName = string.Format(AutoSaveConfig.AutoSaveNameFormat.value, Threading.renderTime, DateTime.Now);
            debug.Log("saveName " + saveName);
            SerializableData.SaveGame(saveName);
        }
    }
}
