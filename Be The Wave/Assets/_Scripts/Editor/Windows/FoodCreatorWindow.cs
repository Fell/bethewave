using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class FoodCreatorWindow : EditorWindow
{

    List<Food> m_prefabs;

    string[] m_names;

    Food m_current;

    int m_index;

    bool m_isCreating;

    string m_newName = "";

    const string PATH = "_Art/Prefabs/Foods";

    Editor m_foodEditor;

    bool m_hasChanged;

    private void OnEnable()
    {
        RecreatePopup();

        m_index = 0;
        m_current = m_prefabs[ m_index ];
        if ( m_current )
            m_foodEditor = Editor.CreateEditor( m_current );
    }

    private void OnGUI()
    {
        if ( m_isCreating )
            Create();
        else
            Display();
    }

    [MenuItem( "Window/Be The Wave/Food Creator" )]
    private static void Open()
    {
        GetWindow<FoodCreatorWindow>();
    }

    private void Create()
    {
        m_newName = EditorGUILayout.TextField( "Name", m_newName );

        if ( GUILayout.Button( "Create" ) )
            CreateFood();
    }

    private void Display()
    {
        EditorGUI.BeginChangeCheck();

        m_index = EditorGUILayout.Popup( m_index, m_names );

        if ( m_index == m_names.Length - 1 )
        {
            m_isCreating = true;
            return;
        }

        if ( EditorGUI.EndChangeCheck() )
        {
            m_current = m_prefabs[ m_index ];
            m_foodEditor = Editor.CreateEditor( m_current );
        }

        EditorGUI.BeginChangeCheck();

        m_foodEditor.OnInspectorGUI();

        m_hasChanged |= EditorGUI.EndChangeCheck();

        GUI.enabled = m_hasChanged;

        if ( GUILayout.Button( "Apply" ) )
        {
            m_hasChanged = false;
            AssetDatabase.Refresh();
        }

        GUI.enabled = true;
    }


    private void RecreatePopup()
    {
        m_prefabs = Helper.GetPrefabsInFolder<Food>( PATH );
        m_prefabs.Add( null );
        m_names = new string[ m_prefabs.Count ];
        for ( int i = 0; i < m_prefabs.Count; i++ )
        {
            m_names[ i ] = m_prefabs[ i ] ? m_prefabs[ i ].name : "Create New";
        }

        m_index = m_prefabs.IndexOf( m_current );
    }

    private void CreateFood()
    {
        var _obj = GameObject.CreatePrimitive( PrimitiveType.Quad );
        _obj.GetComponent<MeshRenderer>().material = AssetDatabase.LoadAssetAtPath<Material>( "Assets/_Art/Materials/FoodMaterial.mat" );
        _obj.AddComponent<Food>();
        _obj.name = m_newName;

        var _canvas = new GameObject( "Canvas", typeof( RectTransform ), typeof( Canvas ), typeof( CanvasScaler ), typeof( GraphicRaycaster ) ).GetComponent<Canvas>();
        var _cTrans = _canvas.transform as RectTransform;

        _cTrans.SetParent( _obj.transform, false );
        _cTrans.localPosition = Vector3.up * 0.75f;
        _cTrans.localRotation = Quaternion.identity;

        _cTrans.localScale = Vector3.one * 0.005f;

        var _image = new GameObject( "Image", typeof( RectTransform ), typeof( Image ) ).GetComponent<Image>();
        var _imageTrans = _image.rectTransform;
        _imageTrans.SetParent( _cTrans, false );

        var _sprite = AssetDatabase.LoadAssetAtPath<Sprite>( "Assets/_Art/Textures/Sprites/FOOD_BTW2.png" );

        _image.sprite = _sprite;
        _image.type = Image.Type.Filled;
        _image.fillAmount = 0;
        _image.fillClockwise = true;
        _image.fillMethod = Image.FillMethod.Radial360;
        _image.fillOrigin = 2;

        _obj.CreatePrefab( PATH );


        RecreatePopup();

        m_current = m_prefabs.First( p => p != null ? p.name == m_newName : false );

        m_newName = "";

        m_isCreating = false;
    }
}
