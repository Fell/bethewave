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

    public MenuPoint[] m_options;


    #endregion

    #region Methods

    public void Show( PerformanceReport _report, bool _hasAnotherLevel )
    {
        gameObject.SetActive( true );

        var _result = _report.GetResult();

        m_positiveHeader.gameObject.SetActive( _result != Result.Burnt );
        m_negativeHeader.gameObject.SetActive( _result == Result.Burnt );

        m_medalImage.sprite = m_medals[ (int)_result ];
    }



    #endregion

}


