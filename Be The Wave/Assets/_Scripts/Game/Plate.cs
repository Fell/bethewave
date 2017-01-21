using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{

    #region Fields

    public List<Food> m_foods = new List<Food>();

    public float m_rotSpeed = 10;

    public float m_distance = 10;

    public float m_triggerAngle = 45;

    public float m_time = 300;

    public float m_heightOffset = 1f;

    private float angle;

    #endregion


    #region Methods

    // Use this for initialization
    void Start()
    {
        if ( m_foods.Count <= 0 )
            return;

        angle = 360 / m_foods.Count;

        for ( int i = 0; i < m_foods.Count; i++ )
        {
            Vector3 dir = new Vector3( Mathf.Cos( 2 * Mathf.PI * i / m_foods.Count ), -Mathf.Sin( 2 * Mathf.PI * i / m_foods.Count ) );
            dir = dir * m_distance;
            Instantiate( m_foods[ i ].gameObject, this.transform.position + dir + Vector3.up * m_heightOffset, Quaternion.identity, this.transform );
        }

        UIManager.Instance.m_timer.Time = m_time;
        UIManager.Instance.m_timer.Ticking = true;
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    #endregion
}
