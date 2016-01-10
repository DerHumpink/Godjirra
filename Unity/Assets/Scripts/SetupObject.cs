using UnityEditor;
using UnityEngine;

namespace Editor
{
	public abstract class SetupObject<T> : ScriptableObject where T : SetupObject<T>
	{
		private static T _instance;

		public static T Instance
		{
			get
			{
				if(_instance!=null)
					return _instance;

				var type = typeof (T).ToString();
				Resources.Load<T>("Setup/" + type);
				var instances = Resources.FindObjectsOfTypeAll<T>();
				if (instances.Length == 1)
				{
					_instance= instances[0];
					return instances[0];
				}
				if (instances.Length > 1)
				{
					Debug.LogError("WARNING: There is more than one object of type " + typeof (T));
					_instance = instances[0];
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

			if (AssetDatabase.FindAssets(typeof (T).ToString(), searchFolder).Length == 0)
			{
				var asset = CreateInstance(typeof (T));
				AssetDatabase.CreateAsset(asset, "Assets/Resources/Setup/" + typeof (T) + ".asset");
				EditorUtility.FocusProjectWindow();
				Selection.activeObject = asset;
			}
		}
	}
}