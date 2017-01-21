using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;

[CustomEditor( typeof( Plate ) )]
public class PlateEditor : Editor
{
    const float HEIGHT = 64;

    const float SINGLE_HEIGHT = 25;

    #region Fields

    ReorderableList m_list;



    #endregion


    #region Methods

    public override void OnInspectorGUI()
    {
        if ( m_list == null )
        {
            m_list = new ReorderableList( serializedObject, serializedObject.FindProperty( "m_foods" ), true, true, true, true );
            m_list.drawElementCallback = OnDrawElement;
            m_list.drawHeaderCallback = OnDrawHeader;
            m_list.elementHeightCallback = OnElementHeight;
        }


        m_list.DoLayoutList();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_rotSpeed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_distance"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_triggerAngle"));
        serializedObject.ApplyModifiedProperties();
    }

    private float OnElementHeight( int index )
    {
        var _instance = m_list.serializedProperty.GetArrayElementAtIndex( index ).objectReferenceValue;
        if ( _instance )
        {
            return HEIGHT;
        }
        return SINGLE_HEIGHT;
    }

    private void OnDrawHeader( Rect rect )
    {
        EditorGUI.LabelField( rect, "Foods" );
    }

    private void OnDrawElement( Rect rect, int index, bool isActive, bool isFocused )
    {
        var _property = m_list.serializedProperty.GetArrayElementAtIndex( index );
        Food _food = _property.objectReferenceValue as Food;

        if ( _food == null )
        {
            EditorGUI.PropertyField( rect, _property, new GUIContent( "" + ( index + 1 ) ) );
        }
        else
        {
            var _nameRect = new Rect( rect.position, new Vector2( 50, rect.height ) );

            var _pictureRect = new Rect( rect.position + new Vector2( 65, 0 ), new Vector2( HEIGHT, HEIGHT ) );

            EditorGUI.LabelField( _nameRect, _food.name );

            EditorGUI.LabelField( _pictureRect, new GUIContent( AssetPreview.GetAssetPreview( _food.gameObject ) ) );
        }
    }

    #endregion
}
