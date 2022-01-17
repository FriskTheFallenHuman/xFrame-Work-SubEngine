using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vector3 = UnityEngine.Vector3;

public class DoorObject : ObjectInteractible
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
