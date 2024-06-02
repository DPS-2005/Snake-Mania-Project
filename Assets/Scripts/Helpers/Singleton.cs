using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

	public static T Instance
	{
		get { return _instance; }
		set { _instance = value; }
	}

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = (T) (object) this;
        DontDestroyOnLoad(this.gameObject);
    }
}
