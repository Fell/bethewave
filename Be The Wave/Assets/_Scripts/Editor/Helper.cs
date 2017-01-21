using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.Linq;

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
        AssetDatabase.ImportAsset( _relPath, ImportAssetOptions.ForceUpdate );
        AssetDatabase.Refresh();
        Object.DestroyImmediate( _obj );
    }

    public static List<T> GetPrefabsInFolder<T>( string _folderPath ) where T : Component
    {
        var _path = Path.Combine( Application.dataPath, _folderPath );

        DirectoryInfo _di = new DirectoryInfo( _path );
        if ( !_di.Exists )
            return new List<T>();

        var _files = _di.GetFiles( "*.prefab" );

        var _paths = _files.Select( f => ( f.FullName.Replace( "\\", "/" ).Replace( Application.dataPath, "Assets" ) ) );

        List<T> _prefabs = new List<T>();

        foreach ( var _p in _paths )
        {
            var _prefab = AssetDatabase.LoadAssetAtPath<T>( _p );
            if ( _prefab != null )
                _prefabs.Add( _prefab );
        }

        return _prefabs;
    }

    //public static void CreatePrefab(this GameObject _obj, string _path)
}
