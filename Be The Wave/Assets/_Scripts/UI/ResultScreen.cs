using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{

    #region Fields

    public Text m_positiveHeader;
    public Text m_negativeHeader;

    public Image m_medalImage;
    public Sprite[] m_medals;

    public MPNextLevel m_nextLevel;
    public MPBackToMain m_toMain;

    private List<ResultOption> m_options = new List<ResultOption>();
    private int m_selected = 0;

    #endregion

    #region Methods

    public void Show( PerformanceReport _report, bool _hasAnotherLevel )
    {
        gameObject.SetActive( true );

        var _result = _report.GetResult();

        m_positiveHeader.gameObject.SetActive( _result != Result.Burnt );
        m_negativeHeader.gameObject.SetActive( _result == Result.Burnt );

        m_medalImage.sprite = m_medals[ (int)_result ];

        if ( _hasAnotherLevel )
            m_options.Add( m_nextLevel );

        m_nextLevel.gameObject.SetActive( _hasAnotherLevel );

        m_options.Add( m_toMain );

        for ( int i = 0; i < m_options.Count; i++ )
        {
            m_options[ i ].Select( i == m_selected );
            m_options[ i ].SetNaviFunc( OnNavigate );
        }
    }

    private void OnNavigate()
    {
        m_options[ m_selected ].Select( false );
        m_selected = ( m_selected + 1 ) % m_options.Count;
    }


    #endregion

}


