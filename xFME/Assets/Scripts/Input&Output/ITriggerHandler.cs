/*
                             MIT License

                    Copyright (c) 2023 Krispy

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode, DisallowMultipleComponent]
public abstract class ITriggerHandler : MonoBehaviour
{
    /* Variables */

    /* Trigger Stuffs */
    public BoxCollider triggerCollider = default;
    public Renderer triggerRender = default;
    public Material triggerMaterial = default;

    /* The List */

    /* OnEnter */
    public List<string> onTriggerEnterTag;
    public List<UnityEvent> onTriggerEnter;

    /* OnStay */
    public List<string> onTriggerStayTag;
    public List<UnityEvent> onTriggerStay;

    /* OnExit */
    public List<string> onTriggerExitTag;
    public List<UnityEvent> onTriggerExit;

    /* Awake is called when the script instance is being loaded. */
    public void Awake()
    {
        /* Don't display our gameObject in Runtime */
#if UNITY_EDITOR
        gameObject.SetActive( true );
#else
        gameObject.SetActive( false );
#endif // UNITY_EDITOR

        /* Add our box collider by default and set it to trigger */
        if ( triggerCollider == null ) 
        {
            triggerCollider = gameObject.AddComponent<BoxCollider>();
            triggerCollider.isTrigger = true;
        }
    }

    /* Update is called once per frame */
    public void Update()
    {
        /* Get our Render compoment */
        triggerRender = gameObject.GetComponent<Renderer>();
        if ( triggerMaterial != null )
            triggerRender.material = triggerMaterial;
    }

    /* Methods that sub classes implements */
    public abstract void OnTriggerEnter( Collider other );
    public abstract void OnTriggerStay( Collider other );
    public abstract void OnTriggerExit( Collider other );
}
