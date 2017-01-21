﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{

    #region Fields

    public List<Food> m_foods = new List<Food>();

    public float m_rotSpeed = 10;

    public float m_distance = 10;

    public float m_foodScale = 1.0f;

    public float m_triggerAngle = 45;

    public float m_time = 300;

    public float m_heightOffset = 1f;

    private float angle;

    private Food[] m_children;

    #endregion


    #region Methods

    // Use this for initialization
    void Start()
    {
        if ( m_foods.Count <= 0 )
            return;

        angle = 360 / m_foods.Count;

        m_children = new Food[ m_foods.Count ];

        for ( int i = 0; i < m_foods.Count; i++ )
        {
            Vector3 dir = new Vector3( Mathf.Cos( 2 * Mathf.PI * i / m_foods.Count ), -Mathf.Sin( 2 * Mathf.PI * i / m_foods.Count ) );
            dir = dir * m_distance;
            m_children[ i ] = Instantiate( m_foods[ i ].gameObject, this.transform.position + dir + Vector3.up * m_heightOffset, Quaternion.identity, this.transform ).GetComponent<Food>();
            m_children[ i ].transform.localScale = new Vector3( m_foodScale, m_foodScale, m_foodScale );
            UIManager.Instance.m_powerMeter.addMarker( m_children[ i ], m_children[ i ].m_reqStrengthLv );
        }

        UIManager.Instance.m_timer.Time = m_time;
        UIManager.Instance.m_timer.Ticking = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ( GameManager.Instance.m_isPaused )
            return;

        this.transform.Rotate( Vector3.up, m_rotSpeed * Time.deltaTime );


        for ( int i = 0; i < m_foods.Count; i++ )
        {
            float lowAngle = 360 + i * angle - m_triggerAngle;

            float highAngle = 360 + i * angle + m_triggerAngle;

            float newPlateRot = transform.rotation.eulerAngles.y + 360;

            if ( newPlateRot >= lowAngle && newPlateRot <= highAngle )
            {
                Debug.Log( "ok" + i );
            }
        }

        CalcHeating();
    }

    private void CalcHeating()
    {
        var _minDot = Mathf.Cos( Mathf.Rad2Deg * m_triggerAngle );

        for ( int i = 0; i < m_children.Length; i++ )
        {
            Vector3 _dir = m_children[ i ].transform.position - transform.position;
            _dir -= transform.up * Vector3.Dot( _dir, transform.up );
            _dir = _dir.normalized;
            var _dot = Vector3.Dot( _dir, transform.parent.right );

            // change later!!!
            if ( _dot > _minDot )
            {
                var _devi = UIManager.Instance.m_powerMeter.getDeviation( m_children[ i ] );

                var _heatMultiplier = 1.0f;
                var _burnMultiplier = 0.0f;
                // maybe change
                if ( _devi < -10 )
                {
                    _heatMultiplier = Mathf.Lerp( 1, 0, -( ( _devi + 10 ) / Mathf.Max( 1, ( m_children[ i ].m_reqStrengthLv - 10 ) ) ) );
                }
                if ( _devi > 10 )
                {
                    // 100 - 10
                    _burnMultiplier = Mathf.Lerp( 1, 0, ( 90.0f - m_children[ i ].m_reqStrengthLv - _devi ) / ( 90 - m_children[ i ].m_reqStrengthLv ) );
                }

                m_children[ i ].m_actBurnPec += (int)( _burnMultiplier * Time.deltaTime * m_children[ i ].m_burnPecRaise );
                m_children[ i ].m_actDegreePec += (int)( _heatMultiplier * Time.deltaTime * m_children[ i ].m_degreePecRaise );
            }

        }
    }

    #endregion
}
