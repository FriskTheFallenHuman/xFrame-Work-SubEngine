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

public class TriggerMultiple : ITriggerHandler
{
    public override void OnTriggerEnter( Collider other )
    {
        /* Don't execute us if we're not using a tag */
        if ( onTriggerEnter.Count == 1 && onTriggerEnterTag.Count == 0 )
        {
            onTriggerEnter[0].Invoke();
            return;
        }

        /* Bail if we don't match outselves */
        if ( onTriggerEnter.Count != onTriggerEnterTag.Count )
        {
            Debug.LogError( "[xFME] OnTriggerEnter Mistmatch!" );
            return;
        }

        /* Lop trough our UnityEvents */
        for ( int i = 0; i < onTriggerEnter.Count; i++ )
        {
            /* Invoke our event */
            if ( other.CompareTag( onTriggerEnterTag[i] ) )
            {
                onTriggerEnter[i].Invoke();
                Debug.Log( "[xFME] OnTriggerEnter Triggering" + onTriggerEnter[i].ToString() );
            }
        }
    }

    public override void OnTriggerStay( Collider other )
    {
        /* Don't execute us if we're not using a tag */
        if ( onTriggerStay.Count == 1 && onTriggerStayTag.Count == 0 )
        {
            onTriggerStay[0].Invoke();
            return;
        }

        /* Bail if we don't match outselves */
        if ( onTriggerStay.Count != onTriggerStayTag.Count )
        {
            Debug.LogError( "[xFME] OnTriggerStay Mistmatch!" );
            return;
        }

        /* Lop trough our UnityEvents */
        for ( int i = 0; i < onTriggerStay.Count; i++ )
        {
            /* Invoke our event */
            if ( other.CompareTag( onTriggerStayTag[i] ) )
            {
                onTriggerStay[i].Invoke();
                Debug.Log( "[xFME] OnTriggerStay Triggering: " + onTriggerStay[i].ToString() );
            }
        }
    }

    public override void OnTriggerExit( Collider other )
    {
        /* Don't execute us if we're not using a tag */
        if ( onTriggerExit.Count == 1 && onTriggerExitTag.Count == 0 )
        {
            onTriggerExit[0].Invoke();
            return;
        }

        /* Bail if we don't match outselves */
        if ( onTriggerExit.Count != onTriggerExitTag.Count )
        {
            Debug.LogError( "[xFME] OnTriggerExit Mistmatch!" );
            return;
        }

        /* Lop trough our UnityEvents */
        for ( int i = 0; i < onTriggerExit.Count; i++ )
        {
            /* Invoke our event */
            if ( other.CompareTag( onTriggerExitTag[i] ) )
            {
                onTriggerExit[i].Invoke();
                Debug.Log( "[xFME] OnTriggerExit Triggering: " + onTriggerExit[i].ToString() );
            }
        }
    }
}
