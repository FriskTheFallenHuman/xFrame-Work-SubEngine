/*
    Copyright 2022 Krispy

    Use of this source code is governed by an MIT-style
    license that can be found in the LICENSE file or at
    https://opensource.org/licenses/MIT.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISingletonInterface<T> : MonoBehaviour where T : Component
{
    private static object _lock = new object();
    private static T _Instance;
    public static T Instance 
    {
		get
		{
            if ( applicationIsQuitting )
                return null;

            lock ( _lock )
            {
                if ( !_Instance )
                {
                    _Instance = new GameObject().AddComponent<T>();
                    /* name it for easy recognition */
                    _Instance.name = _Instance.GetType().ToString();
                    /* mark root as DontDestroyOnLoad(); */
                    DontDestroyOnLoad( _Instance.gameObject );
                }
            }

			return _Instance;
		}
    }

    private static bool applicationIsQuitting = false;

    public virtual void OnDestroy()
    {
        Debug.Log("Gets destroyed");
        applicationIsQuitting = true;   
    }
}