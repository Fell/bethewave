using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    Quaternion m_lerpStart;
    Quaternion m_lerpEnd;
    float m_lerpT = 1.0f;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		if(m_lerpT < 1) {
            m_lerpT += Time.deltaTime * (1 - m_lerpT);
            transform.localRotation = Quaternion.Slerp(m_lerpStart, m_lerpEnd, m_lerpT);
        }

        if (Input.GetKeyDown(KeyCode.Q)) Open();
        if (Input.GetKeyDown(KeyCode.E)) Close();
    }

    public void Open() {
        m_lerpStart = transform.localRotation;
        m_lerpEnd = Quaternion.Euler(0, 90, 0);
        m_lerpT = 0;
    }

    public void Close() {
        m_lerpStart = transform.localRotation;
        m_lerpEnd = Quaternion.Euler(0, 0, 0);
        m_lerpT = 0;
    }
}
