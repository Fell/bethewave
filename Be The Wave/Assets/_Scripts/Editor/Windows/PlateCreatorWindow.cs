using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;

public class PlateCreatorWindow : EditorWindow
{
    #region Constants

    const string PATH = "_Art/Prefabs/Plates";

    #endregion


    #region Fields

    private Plate m_plate;

    Editor m_editor;

    #endregion

    #region Methods


    private void OnEnable()
    {
        if ( m_plate == null )
        {
            m_plate = GameObject.FindObjectOfType<Plate>();

        }

        if ( m_plate != null && m_editor == null )
            m_editor = Editor.CreateEditor( m_plate );
    }

    private void OnGUI()
    {
        if ( m_plate == null )
        {
            if ( GUILayout.Button( "Create Plate" ) )
            {
                m_plate = new GameObject( "Plate" ).AddComponent<Plate>();
                m_editor = Editor.CreateEditor( m_plate );
            }

        }
        else
        {
            m_plate.name = EditorGUILayout.TextField( "Name", m_plate.name );
            m_editor.OnInspectorGUI();

            if ( GUILayout.Button( "Save as Prefab" ) )
                m_plate.gameObject.CreatePrefab( PATH );
        }
    }

    [MenuItem( "Window/Be The Wave/Plate Creator" )]
    private static void Open()
    {
        GetWindow<PlateCreatorWindow>();
    }

    #endregion
}
