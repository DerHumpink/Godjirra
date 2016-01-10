using UnityEngine;
using System.Collections;

public abstract class SingletonAutoInstance<T> : MonoBehaviour where T : SingletonAutoInstance<T>
{
	protected static SingletonAutoInstance<T> _instance;
	private static bool _quit;

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

	public virtual void OnDestroy()
	{
		_instance = null;
	}

	public static T Instance
	{
		get
		{
			if (_instance == null && !_quit && Application.isPlaying)
			{
				GameObject go = new GameObject(typeof(T).Name + "_AutoInstance");
				T addComponent = go.AddComponent<T>();
				_instance = addComponent;
				addComponent.PostAutoInstance();
			}
			return _instance as T;
		}
	}

	protected virtual void PostAutoInstance()
	{

	}

	protected virtual void OnApplicationQuit()
	{
		_quit = true;
	}
}