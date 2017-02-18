using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.Linq;
using UnityEngine.UI;

public static class Helper
{

    public static void CreatePrefab( this GameObject _obj, string _path )
    {
        var _folderPath = Path.Combine( Application.dataPath, _path );
        var _name = _obj.name + ".prefab";
        var _relPath = Path.Combine( Path.Combine( "Assets", _path ), _name ).Replace( "\\", "/" );

        if ( !Directory.Exists( _folderPath ) )
            Directory.CreateDirectory( _folderPath );

        var _old = AssetDatabase.LoadAssetAtPath<GameObject>( _relPath );

        if ( _old )
        {
            PrefabUtility.ReplacePrefab( _obj, _old, ReplacePrefabOptions.ConnectToPrefab );
        }
        else
        {
            PrefabUtility.CreatePrefab( _relPath, _obj, ReplacePrefabOptions.ConnectToPrefab );
        }

        AssetDatabase.ImportAsset( _relPath, ImportAssetOptions.ForceUpdate );
        AssetDatabase.Refresh();
        Object.DestroyImmediate( _obj );
    }

    public static List<T> GetPrefabsInFolder<T>( string _folderPath ) where T : Component
    {

        var _paths = GetPrefabPaths( _folderPath );

        List<T> _prefabs = new List<T>();

        if ( _paths == null )
            return _prefabs;

        foreach ( var _p in _paths )
        {
            var _prefab = AssetDatabase.LoadAssetAtPath<T>( _p );
            if ( _prefab != null )
                _prefabs.Add( _prefab );
        }

        return _prefabs;
    }
    //public static void CreatePrefab(this GameObject _obj, string _path)

    [MenuItem( "Assets/Be The Wave/Update Food" )]
    public static void UpdateFood()
    {
        var _paths = GetPrefabPaths( "_Art/Prefabs/Foods" );

        if ( _paths == null )
            return;

        for ( int i = 0; i < _paths.Length; i++ )
        {
            var _prefab = AssetDatabase.LoadAssetAtPath<Food>( _paths[ i ] );
            if ( _prefab == null )
                continue;

            var _instantiated = GameObject.Instantiate<Food>( _prefab );


            var _tMesh = _instantiated.GetComponentInChildren<TextMesh>();

            if ( _tMesh )
            {
                GameObject.DestroyImmediate( _tMesh.gameObject );
            }

            var _canvas = _instantiated.GetComponentInChildren<Canvas>();
            RectTransform _cTrans;

            if ( !_canvas )
            {
                _canvas = new GameObject( "Canvas", typeof( RectTransform ), typeof( Canvas ), typeof( CanvasScaler ), typeof( GraphicRaycaster ) ).GetComponent<Canvas>();
                _cTrans = _canvas.transform as RectTransform;

                _cTrans.SetParent( _instantiated.transform, false );
            }
            else
            {
                _cTrans = _canvas.transform as RectTransform;
            }
            _cTrans.localPosition = Vector3.up * 0.75f;
            _cTrans.localRotation = Quaternion.identity;
            _cTrans.localScale = Vector3.one * 0.005f;

            var _image = _canvas.GetComponentInChildren<Image>();
            RectTransform _imageTrans;

            if ( !_image )
            {
                _image = new GameObject( "Image", typeof( RectTransform ), typeof( Image ) ).GetComponent<Image>();
                _imageTrans = _image.rectTransform;
                _imageTrans.SetParent( _cTrans, false );
            }

            var _sprite = AssetDatabase.LoadAssetAtPath<Sprite>( "Assets/_Art/Textures/Sprites/FOOD_BTW2.png" );

            Debug.LogFormat( "Sprite is {0}", _sprite != null ? "loaded" : "null" );

            _image.sprite = _sprite;
            _image.type = Image.Type.Filled;
            _image.fillAmount = 0;
            _image.fillClockwise = true;
            _image.fillMethod = Image.FillMethod.Radial360;
            _image.fillOrigin = 2;

            PrefabUtility.ReplacePrefab( _instantiated.gameObject, _prefab.gameObject, ReplacePrefabOptions.ConnectToPrefab | ReplacePrefabOptions.ReplaceNameBased );

            GameObject.DestroyImmediate( _instantiated.gameObject );
        }
    }

    public static string[] GetPrefabPaths( string _folderPath )
    {
        var _path = Path.Combine( Application.dataPath, _folderPath );

        DirectoryInfo _di = new DirectoryInfo( _path );
        if ( !_di.Exists )
        {
            Debug.LogWarningFormat( "Folder {0} does not exist", _di.FullName );
            return null;
        }

        var _files = _di.GetFiles( "*.prefab" );

        return _files.Select( f => ( f.FullName.Replace( "\\", "/" ).Replace( Application.dataPath, "Assets" ) ) ).ToArray();
    }

}
