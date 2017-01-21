using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

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

    // Use this for initialization
    void Start () {
        Time = 300; // 5:00
	}
	
	// Update is called once per frame
	void Update () { 
        if (m_ticking && (int)Time > 0)
        {
            // Update Time
            m_time -= UnityEngine.Time.deltaTime;

            // Update Text
            TextMesh theMesh = gameObject.GetComponent<TextMesh>();
            theMesh.text = getTimeString();
        }
    }

    private string getTimeString() {
        int minutes = (int)(Time / 60);
        int seconds = (int)(Time % 60);

        if (seconds % 2 == 0)
            return minutes.ToString("D2") + ":" + seconds.ToString("D2");
        else
            return minutes.ToString("D2") + " " + seconds.ToString("D2");
    }
}
