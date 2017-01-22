﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPoint : MonoBehaviour {

    public Texture m_texture;
    public Camera m_targetCam;
    public AudioClip m_clip;

	// Use this for initialization
	public void Start () {
        GetComponentInChildren<MeshRenderer>().material.mainTexture = m_texture;
        if (m_targetCam == null)
        {
            m_targetCam = GameObject.Find("MainCamera").GetComponent<Camera>();
        }
	}
	
	// Update is called once per frame
	public void Update () {
        //transform.rotation = Quaternion.LookRotation( -Camera.main.transform.forward, Vector3.up );
        transform.forward = m_targetCam.transform.forward;
	}

    public virtual void MenuAction()
    {
    }
}