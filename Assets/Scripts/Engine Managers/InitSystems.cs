/*
    Copyright 2022 Krispy

    Use of this source code is governed by an MIT-style
    license that can be found in the LICENSE file or at
    https://opensource.org/licenses/MIT.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSystems : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /* Initis the GameManager */
        GameManager.Instance.ExecuteLuaArgument( "require('gamemanager')" );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
