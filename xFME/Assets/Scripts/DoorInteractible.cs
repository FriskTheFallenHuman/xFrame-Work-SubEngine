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
using System.Collections;
using UnityEngine;
using Utils;

using Vector3 = UnityEngine.Vector3;

public class DoorInteractible : ObjectInteractible
{
    private bool isOpen = false;
    private bool canBeInteractedWith = true;
    private Animator anim;

    // Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
    private void Start()
    {
        /* Get our Door's Animator */
        anim = GetComponent<Animator>();        
    }

    public override void OnInteract()
    {
        /* Check if we can interact with this door */
        if ( canBeInteractedWith )
        {
            isOpen = !isOpen;

            /* Calculate the dot product */
            Vector3 doorTransformDirection = transform.TransformDirection( Vector3.forward );
            Vector3 playerTransformDirection = PlayerController.instance.transform.position - transform.position;
            float dot = Vector3.Dot( doorTransformDirection, playerTransformDirection );

            /* Set the dot product to our Animator */
            anim.SetFloat( "dot", dot );
            anim.SetBool( "isOpen", isOpen );

            /* Auto close our door */
            StartCoroutine( nameof( AutoClose ) );
        }
    }

    public override void OnFocus()
    {
    
    }

    public override void OnLoseFocus()
    {
    
    }

    private IEnumerator AutoClose()
    {
        Vector3 doorPosition = transform.position;
        Vector3 playerPosition = PlayerController.instance.transform.position;

        if ( isOpen )
        {
            yield return new WaitForSeconds( 3 );

            /* If we are 10 units away from our door close it */
            if ( Vector3.Distance( doorPosition, doorPosition ) > 10 )
            {
                isOpen = false;

                /* Set our animation parameters */
                anim.SetFloat( "dot", 0 );
                anim.SetBool( "isOpen", isOpen );
            }
        }
    }

    private void Animator_LockInteraction()
    {
        canBeInteractedWith = false;
    }

    private void Animator_UnlockInteraction()
    {
        canBeInteractedWith = true;
    }
}
