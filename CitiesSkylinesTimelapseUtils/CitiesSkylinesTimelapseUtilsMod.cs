using ICities;
using UnityEngine;

namespace CitiesSkylinesTimelapseUtils
{
	public class CitiesSkylinesTimelapseUtilsMod : IUserMod
	{
		public string Name
		{
			get { return "CitiesSkylinesTimelapseUtils"; }
		}

		public string Description
		{
			get { return "My first awesome mod that doesn't do anything! *JEEY*"; }
		}
	}

	#region Mod Behavior
	/// <summary>
	/// Here we are creating a custom ILoadingExtension; 
	/// LoadingExtensionBase implemented ILoadingExtension and provides some default behavior so we are inheriting from that.
	/// </summary>
	public class CustomLoader : LoadingExtensionBase
	{
		/// <summary>
		/// This event is triggerred when a level is loaded
		/// </summary>
		public override void OnLevelLoaded(LoadMode mode)
		{
			// Instantiate a custom object
			GameObject go = new GameObject("Test Object");
			go.AddComponent<CustomComponent>();

			base.OnLevelLoaded(mode);
		}
	}
	#endregion

	#region Custom Game Object Components
	/// <summary>
	/// Here we creating a custom game object that directly utilize Unity Game Engine;
	/// See https://docs.unity3d.com/Manual/CreatingAndUsingScripts.html for more detail.
	/// </summary>
	public class CustomComponent : MonoBehaviour
	{
		/// <summary>
		/// This event is triggered when this object is created
		/// </summary>
		void Start()
		{
			DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "Hello World 2!");
		}

		/// <summary>
		/// This event is triggered every frame, we can use this to add some animation etc.
		/// </summary>
		void Update()
		{

		}
	}
	#endregion
}
