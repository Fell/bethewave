﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager s_instance;

    public List<LevelInfo> m_levels;

    public int m_currentIndex = 0;

    public string mainMenuScene = "MainMenu";

    public bool m_isPaused = true;

    public static GameManager Instance
    {
        get
        {
            if ( s_instance == null )
            {
                s_instance = ( Instantiate( Resources.Load( "Manager/GameManager" ) ) as GameObject ).GetComponent<GameManager>();
            }

            return s_instance;
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad( gameObject );
    }


    public void StartGame()
    {
        m_currentIndex = 0;
        m_isPaused = true;
        SceneManager.LoadScene( m_levels[ m_currentIndex ].m_level );
    }

    public void StartGame( int _index )
    {
        m_currentIndex = _index;
        m_isPaused = true;
        SceneManager.LoadScene( m_levels[ m_currentIndex ].m_level );
    }

    public void PauseGame()
    {

    }

    public void StopGame()
    {
        m_isPaused = true;
        SceneManager.LoadScene( mainMenuScene );
    }

    public void OpenNextScene()
    {
        m_isPaused = true;

        if ( m_currentIndex < m_levels.Count )
            SceneManager.LoadScene( m_levels[ m_currentIndex ].m_level );
        else
            StopGame();
    }

    public void CreateField()
    {
        UIManager.Instance.m_microwave.SetPlate( m_levels[ m_currentIndex ].m_plate );
        UIManager.Instance.m_timer.OnTimerFinished += OnTimerFinished;
    }

    private void OnTimerFinished()
    {
        UIManager.Instance.m_timer.OnTimerFinished -= OnTimerFinished;

        m_isPaused = true;

        StopLevel( EndType.TimeOut );
    }

    public void StartScene()
    {
        UIManager.Instance.m_microwave.StartWaving();

        m_isPaused = false;
    }

    public void StopLevel( EndType _type )
    {
        StopLevel( _type, UIManager.Instance.m_microwave.GetFoods() );
    }

    public void StopLevel( EndType _type, Food[] _food )
    {
        m_isPaused = true;

        m_currentIndex++;
        UIManager.Instance.StartCoroutine( UIManager.Instance.DoResultScreen( new PerformanceReport( _food, _type ), m_currentIndex < m_levels.Count ) );
    }
}

[System.Serializable]
public struct LevelInfo
{
    public string m_name;
    public string m_level;
    public Plate m_plate;
}
