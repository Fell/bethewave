﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }

    public Timer m_timer;

    public PowerMeter m_powerMeter;

    public Microwave m_microwave;

    public CanvasGroup m_fader;

    public float m_fadeTime;

    public Text m_countdownText;

    public AnimationCurve m_countdownCurve;

    private void Awake()
    {
        if ( Instance && Instance != this )
        {
            Destroy( gameObject );
            return;
        }

        Instance = this;

        m_timer.OnTimerFinished += OnTimerFinished;

        StartCoroutine( DoSceneIntro() );
    }


    public void DoResultScreen()
    {

    }

    private IEnumerator DoSceneIntro()
    {
        DynamicGI.UpdateEnvironment();

        GameManager.Instance.CreateField();

        yield return StartCoroutine( Fade( 0 ) );

        yield return StartCoroutine( CountDown() );

        GameManager.Instance.StartScene();
    }

    private IEnumerator Fade( float _targetAlpha )
    {
        float _time = 0;
        float _startAlpha = m_fader.alpha;
        if ( m_fadeTime > 0 )
        {
            while ( _time < m_fadeTime )
            {
                _time += Time.deltaTime;
                m_fader.alpha = Mathf.Lerp( _startAlpha, _targetAlpha, ( _time / m_fadeTime ) );
                yield return null;
            }
        }
        else
        {
            m_fader.alpha = _targetAlpha;
        }
    }

    private IEnumerator CountDown()
    {
        m_countdownText.GetComponent<CanvasGroup>().alpha = 1;

        for ( int i = 3; i > 0; i-- )
        {
            float _progress = 0;
            m_countdownText.text = "" + i;
            while ( _progress < 1 )
            {
                _progress += Time.deltaTime;
                var _scale = m_countdownCurve.Evaluate( _progress );
                m_countdownText.rectTransform.localScale = new Vector3( _scale, _scale, _scale );
                yield return null;
            }
        }
        m_countdownText.GetComponent<CanvasGroup>().alpha = 0;
    }


    private void OnTimerFinished()
    {

    }
}
