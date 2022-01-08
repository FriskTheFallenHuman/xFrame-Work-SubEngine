/*
    Copyright 2022 Krispy

    Use of this source code is governed by an MIT-style
    license that can be found in the LICENSE file or at
    https://opensource.org/licenses/MIT.
*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class GameManager : ISingletonInterface<GameManager>
{
   /* Initialize the LuaVM */
    LuaEnv luaenv = null;
    [Tooltip("Toggles LuaVM.")]
    public bool luaEnable = true;

    /* Toggle debug mode */
    [Tooltip("Toggles debug mode.")]
    public bool debugMode;

    // Start is called before the first frame update
    void Start()
    {
        /* Execute our main.lua file */
        if ( luaEnable )
        {
            /* Creates our LuaVM and setup the custom path */
            luaenv = new LuaEnv();
            luaenv.AddLoader( LuaScripts );

            Logger.Log( Channel.LuaNative, Priority.Info, "[xFM] LuaVM is Active" );

            luaenv.DoString( "require('main')" );
        }
        else { Logger.Log( Channel.LuaNative, Priority.Info, "[xFM] LuaVM is de-active" ); }
    }

    // The LUA folder is on the asset path + subfolders
    // The loader works for both Editor + Runner
    private List<string> folders;
    public byte[] LuaScripts( ref string fileName )
    {
 //#if UNITY_EDITOR
        /* If we don't had any folder on our List, add them! */
        if ( folders == null )
        {
            folders = new List<string>();
            string luaPath = Application.dataPath + "/Lua/";
            folders.Add( luaPath );

            /* Add all of our subfolders */
            string[] dirs = Directory.GetDirectories( luaPath );
            foreach ( var subfolder in dirs )
            {
                folders.Add( subfolder + "/" );
            }
        }

        /* Check if our subfolders has a .lua file */
        string name = fileName + ".lua";
        for ( var i = 0; i < folders.Count; i++ )
        {
            if ( File.Exists( folders[i] + name ) )
            {
                return File.ReadAllBytes( folders[i] + name );
            }
        }
//#else

//#endif
        return null;
    }

    public void ExecuteLuaArgument( string luaArg )
    {
        if ( luaEnable )
        {
            Logger.Log( Channel.LuaNative, Priority.Info, "[xFM] Executing: " + luaArg );
            luaenv.DoString( luaArg );
        }
    }

    // Update is called once per frame
    public virtual void  Update()
    {
        if ( luaenv != null )
            luaenv.Tick();
    }

    // OnDestroy occurs when a Scene or game ends.
    public virtual void OnDestroy()
    {
        if ( luaenv != null )
            luaenv.Dispose();

        base.OnDestroy();
    }
}
