using UnityEditor;
using UnityEngine;

namespace Editor
{
	public abstract class SetupObject<T> : ScriptableObject where T : SetupObject<T>
	{


		public static T Instance
		{
			get
			{
				Resources.Load("Setup/ParameterSetup");
				T[] instances = Resources.FindObjectsOfTypeAll<T>();
				if (instances.Length == 1)
				{
					return instances[0];
				}
				if (instances.Length > 1)
				{
					Debug.LogError("WARNING: There is more than one object of type " + typeof (T));
					return instances[0];
				}
				Debug.LogError("WARNING: There is no object of type " + typeof (T));
				return null;
			}
		}

		public static void CreateIfNotExisting()
		{
			//if (Instance == null)
			//{

			string[] searchFolder = {"Assets/Resources/Setup/"};

			if (AssetDatabase.FindAssets(typeof(T).ToString(), searchFolder).Length==0) {
				ScriptableObject asset = ScriptableObject.CreateInstance(typeof(T));
				AssetDatabase.CreateAsset(asset, "Assets/Resources/Setup/"+typeof(T)+".asset");
				EditorUtility.FocusProjectWindow();
				Selection.activeObject = asset;
			}
		}
	}
}