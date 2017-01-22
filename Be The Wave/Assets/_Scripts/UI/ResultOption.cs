using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ResultOption : MonoBehaviour
{
    public Image m_selectionMarker;

    public Image m_progressMarker;

    public bool m_isSelected;

    public System.Action OnNavigate;

    public float m_downTimeStamp = 0;

    public float m_minHoldTime = 0.2f;

    public float m_invokeTime = 1.0f;

    public bool m_justSelected = false;

    public void SetNaviFunc( System.Action p_onNavigate )
    {
        OnNavigate = p_onNavigate;
    }

    public void Select( bool _selected )
    {
        m_selectionMarker.gameObject.SetActive( _selected );
        m_isSelected = _selected;

        if ( _selected )
            m_justSelected = true;

        m_progressMarker.fillAmount = 0;
    }

    public void Update()
    {
        if ( m_isSelected )
        {
            if ( Input.GetKeyDown( KeyCode.Space ) )
            {
                m_downTimeStamp = Time.time;
                m_justSelected = false;
            }
            else if ( !m_justSelected && Input.GetKeyUp( KeyCode.Space ) )
            {
                if ( Time.time < m_downTimeStamp + m_minHoldTime )
                    OnNavigate();
                else if ( Time.time > m_downTimeStamp + m_invokeTime )
                    InvokeAction();

                m_progressMarker.fillAmount = 0;
            }

            if ( Input.GetKey( KeyCode.Space ) )
            {
                m_progressMarker.fillAmount = Mathf.Clamp01( 1 - ( m_downTimeStamp + m_invokeTime - Time.time ) / m_invokeTime );
            }
        }
    }


    public abstract void InvokeAction();
}
