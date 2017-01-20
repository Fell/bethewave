using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public static class Helper
{

    public static void CreatePrefab( this GameObject _obj, string _path )
    {
        var _folderPath = Path.Combine( Application.dataPath, _path );
        var _name = _obj.name + ".prefab";
        var _relPath = Path.Combine( Path.Combine( "Assets", _path ), _name ).Replace( "\\", "/" );

        if ( !Directory.Exists( _folderPath ) )
            Directory.CreateDirectory( _folderPath );

        PrefabUtility.CreatePrefab( _relPath, _obj );
        Object.DestroyImmediate( _obj );
    }

    //public static void CreatePrefab(this GameObject _obj, string _path)
}
