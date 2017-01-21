using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }

    public Timer m_timer;

    public PowerMeter m_powerMeter;

    public Microwave m_microwave;


    private void Awake()
    {
        if ( Instance && Instance != this )
        {
            Destroy( gameObject );
            return;
        }

        Instance = this;

        StartCoroutine( DoSceneIntro() );
    }


    private IEnumerator DoSceneIntro()
    {
        yield return null;

        GameManager.Instance.StartScene();
    }


}
