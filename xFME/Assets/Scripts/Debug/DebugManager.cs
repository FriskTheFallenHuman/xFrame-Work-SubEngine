using UnityEngine;

public class DebugManager : MonoBehaviour
{
    /* Singleton */
    public static bool isInstanced { get; private set; } = false;

    private static DebugManager _instance;
    public static DebugManager Instance 
    { 
       get {
            return _instance;
       }

       private set { 

            if ( _instance != null & value != null )
                Destroy( _instance );

           _instance = value;

            /* Whe are Instanced meaning we exist now */
            isInstanced = true;
       }
    }

    /* Variables */

    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        DontDestroyOnLoad( this );

        Debug.Log( "[xFME] Debug Manager Created" );
    }

    // Update is called once per frame
    void Update()
    {
    }
}
