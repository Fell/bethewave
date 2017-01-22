﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    #region Fields

    public int m_reqStrengthLv;

    private float m_status;

    private float m_burnStatus;

    public float currentCookStatus
    {
        get { return m_status; }
        set
        {
            m_status = value;

            if ( m_status > m_neededDegree )
            {
                if ( !m_hasBeenCooked )
                {
                    m_hasBeenCooked = true;
                    if ( m_doneParticles )
                        Instantiate( m_doneParticles, transform );
                }
                else
                {
                    var _diff = m_status - m_neededDegree;
                    m_status = m_neededDegree;
                    currentBurnStatus += _diff;
                }
            }
        }
    }

    public float m_cookRate;

    public float currentBurnStatus
    {
        get { return m_burnStatus; }
        set
        {
            if ( m_burnProgressParticles )
            {
                float _emitRate = value - m_burnStatus;

                _emitRate /= 10 * Time.deltaTime;
                _emitRate = Mathf.Clamp( _emitRate, 0, 2.5f );

                if ( !m_progressInstance )
                {
                    m_progressInstance = Instantiate( m_burnProgressParticles, transform );
                    m_progressEmitStrength = m_progressInstance.emission.rateOverTimeMultiplier;
                }

                var _emitter = m_progressInstance.emission;
                _emitter.rateOverTimeMultiplier = m_progressEmitStrength * _emitRate;
            }

            m_burnStatus = value;
            if ( m_burnStatus > m_neededDegree )
            {
                if ( m_canExplode && !m_hasExploded )
                {
                    m_hasExploded = true;
                    if ( m_burnParticles )
                        Instantiate( m_burnParticles, transform );
                    StartCoroutine( ChangeMaterial() );
                    GameManager.Instance.StopLevel( EndType.Explosion );
                }
                else if ( !m_hasBeenBurnt )
                {
                    m_hasBeenBurnt = true;
                    if ( m_burnParticles )
                        Instantiate( m_burnParticles, transform );
                    StartCoroutine( ChangeMaterial() );
                }
                m_burnStatus = m_neededDegree;
            }
        }
    }

    public float m_burnRate;

    int m_neededDegree = 100;

    public Texture m_texture;

    public Texture m_burntTexture;

    public ParticleSystem m_burnParticles;

    public ParticleSystem m_doneParticles;

    public ParticleSystem m_burnProgressParticles;

    private ParticleSystem m_progressInstance;

    private float m_progressEmitStrength;

    public bool m_canExplode;

    public bool m_hasBeenCooked;

    public bool m_hasBeenBurnt;

    public bool m_hasExploded;

    #endregion

    #region Methods

    // Use this for initialization
    void Start()
    {
        currentCookStatus = 0;
        currentBurnStatus = 0;

        GetComponentInChildren<MeshRenderer>().material.mainTexture = m_texture;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.LookRotation( -Camera.main.transform.forward, Vector3.up );
        transform.forward = Camera.main.transform.forward;

        // Update Text
        TextMesh theMesh = transform.Find( "StatusText" ).gameObject.GetComponent<TextMesh>();
        theMesh.text = (int)currentCookStatus + "%";
        theMesh.color = Color.Lerp( Color.white, Color.red, currentBurnStatus / m_neededDegree );
    }

    private IEnumerator ChangeMaterial()
    {
        yield return new WaitForSeconds( 0.5f );
        GetComponent<MeshRenderer>().material.mainTexture = m_burntTexture;
    }

    #endregion
}
