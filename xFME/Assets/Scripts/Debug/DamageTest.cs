using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTest : MonoBehaviour
{
    // When a GameObject collides with another GameObject, Unity calls OnTriggerEnter.
    private void OnTriggerEnter( Collider other )
    {
        if ( other.CompareTag( "Player" ) )
            PlayerController.OnTakeDamage( 15 );
    }
}
