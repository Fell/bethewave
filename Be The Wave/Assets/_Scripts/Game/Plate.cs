using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{

    #region Fields

    public List<Food> m_foods = new List<Food>();

    public float rotSpeed = 10;

    public float distance = 10;

    #endregion


    #region Methods

    // Use this for initialization
    void Start()
    {
        if ( m_foods.Count <= 0 )
            return;

        float angle = 360 / m_foods.Count;

        for ( int i = 0; i < m_foods.Count; i++ )
        {
            Vector3 dir = new Vector3( -Mathf.Sin( 2 * Mathf.PI * i / m_foods.Count ), 0, Mathf.Cos( 2 * Mathf.PI * i / m_foods.Count ) );
            dir = dir * distance;
            Instantiate( m_foods[ i ].gameObject, this.transform.position + dir, Quaternion.identity, this.transform );
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate( Vector3.up, rotSpeed * Time.deltaTime );
    }

    #endregion
}
