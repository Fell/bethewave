using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    private float m_time;
    private bool m_ticking = true;

    public float Time
    {
        get
        {
            return m_time;
        }

        set
        {
            m_time = value;
        }
    }

    public bool Ticking
    {
        get
        {
            return m_ticking;
        }

        set
        {
            m_ticking = value;
        }
    }

    public event Action OnTimerFinished;

    // Use this for initialization
    void Start()
    {
        //Time = 300; // 5:00
        // Update Text
        TextMesh theMesh = gameObject.GetComponent<TextMesh>();
        theMesh.text = getTimeString();
    }

    // Update is called once per frame
    void Update()
    {

        if ( GameManager.Instance.m_isPaused )
            return;

        if ( m_ticking && Time > 0 )
        {
            // Update Time
            m_time -= UnityEngine.Time.deltaTime;

            // Update Text
            TextMesh theMesh = gameObject.GetComponent<TextMesh>();
            theMesh.text = getTimeString();
        }

        if ( Time <= 0 )
        {
            m_ticking = false;
            if ( OnTimerFinished != null )
                OnTimerFinished();
        }
    }

    private string getTimeString()
    {
        int minutes = (int)( Time / 60 );
        int seconds = (int)( Time % 60 );

        if ( seconds % 2 == 0 )
            return minutes.ToString( "D2" ) + ":" + seconds.ToString( "D2" );
        else
            return minutes.ToString( "D2" ) + " " + seconds.ToString( "D2" );
    }
}
