using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
	protected static Singleton<T> _instance;

	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				var findObjectsOfType = FindObjectsOfType(typeof (T));
				if (findObjectsOfType.Length > 0)
				{
					var audioSequencer = (T) findObjectsOfType[0];
					audioSequencer.Awake();
				}
			}
			return _instance as T;
		}
	}

	protected virtual void Awake()
	{
		if (_instance != null)
		{
			Debug.Log(string.Format("Multiple instances of script {0}!", GetType()), gameObject);
			Debug.Log("Other instance: " + _instance, _instance);
		}
		else
		{
			_instance = this;
		}
	}

	protected virtual void OnDestroy()
	{
		_instance = null;
	}
}