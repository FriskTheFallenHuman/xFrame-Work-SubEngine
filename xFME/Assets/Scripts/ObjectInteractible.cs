using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectInteractible : MonoBehaviour
{
    // Awake is called when the script instance is being loaded.
    public void Awake()
    {
        gameObject.layer = 3;
    }
    
    public abstract void OnInteract();
    public abstract void OnFocus();
    public abstract void OnLoseFocus();
}