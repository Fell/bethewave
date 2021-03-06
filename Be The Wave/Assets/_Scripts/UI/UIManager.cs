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

    public Image m_tutImageSergeant;
    public Image m_tutImageWavey;

    public Image m_tutBubble;

    public Text m_tutText;

    public float m_fadeTime;

    public Text m_countdownText;

    public AnimationCurve m_countdownCurve;

    public ResultScreen m_resultScreen;

    public UnityStandardAssets.ImageEffects.BlurOptimized m_blur;

    private void Awake()
    {
        if ( Instance && Instance != this )
        {
            Destroy( gameObject );
            return;
        }

        Instance = this;

        m_timer.OnTimerFinished += OnTimerFinished;

        m_tutImageSergeant.gameObject.SetActive( false );
        m_tutImageWavey.gameObject.SetActive( false );
        m_tutBubble.gameObject.SetActive( false );
        m_tutText.gameObject.SetActive( false );


        StartCoroutine( DoSceneIntro() );
    }


    public IEnumerator DoResultScreen( PerformanceReport _report, bool _hasAnotherLevel )
    {
        var _soundManager = FindObjectOfType<SoundManager>();
        _soundManager.StartCoroutine( _soundManager.PlayShutdown() );

        yield return new WaitForSeconds( 5.0f );

        yield return StartCoroutine( Fade( 1 ) );

        m_resultScreen.Show( _report, _hasAnotherLevel );

        m_blur.enabled = true;

        yield return StartCoroutine( Fade( 0 ) );
    }


    public IEnumerator CloseResultScreen( System.Action p_onFinished )
    {
        yield return StartCoroutine( Fade( 1 ) );

        p_onFinished();
    }

    private IEnumerator DoSceneIntro()
    {
        DynamicGI.UpdateEnvironment();

        GameManager.Instance.CreateField();

        yield return StartCoroutine( Fade( 0 ) );

        yield return StartCoroutine( ShowTutorial() );

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

    private IEnumerator ShowTutorial()
    {
        if ( m_microwave.GetPlate().m_tutSergeant )
        {
            m_tutImageSergeant.gameObject.SetActive( true );
        }
        else
        {
            m_tutImageWavey.gameObject.SetActive( true );
        }
        m_tutBubble.gameObject.SetActive( true );
        m_tutText.gameObject.SetActive( true );

        string[] tuts = m_microwave.GetPlate().m_tutTexts;

        for ( int i = 0; i < tuts.Length; i++ )
        {
            m_tutText.text = tuts[ i ];

            while ( !Input.GetKeyDown( KeyCode.Space ) )
            {
                yield return null;
            }
            yield return null;
        }

        m_tutImageSergeant.gameObject.SetActive( false );
        m_tutImageWavey.gameObject.SetActive( false );
        m_tutBubble.gameObject.SetActive( false );
        m_tutText.gameObject.SetActive( false );
    }

    private void OnTimerFinished()
    {

    }
}
