using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractible : ObjectInteractible
{
    public override void OnInteract()
    {
        print( "[xFME] Interacted with " + gameObject.name );
    }

    public override void OnFocus()
    {
        print( "[xFME] Looking at GameObject " + gameObject.name );
    }

    public override void OnLoseFocus()
    {
        print( "[xFME] Stopped looking at GameObject " + gameObject.name );
    }
}
