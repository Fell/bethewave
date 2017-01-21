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
        SceneManager.LoadScene( m_levels[ m_currentIndex ].m_level );
    }

    public void PauseGame()
    {

    }

    public void StopGame()
    {
        SceneManager.LoadScene( mainMenuScene );
    }

    public void StartScene()
    {
        UIManager.Instance.m_microwave.SetPlate( m_levels[ m_currentIndex ].m_plate );
    }
}

[System.Serializable]
public struct LevelInfo
{
    public string m_level;
    public Plate m_plate;
}
