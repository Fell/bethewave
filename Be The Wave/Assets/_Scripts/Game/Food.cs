using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    #region Fields


    public int m_reqStrengthLv;

    public float m_actDegreePec;
    public float m_degreePecRaise;

    public float m_actBurnPec;
    public float m_burnPecRaise;

    int m_neededDegree = 100;

    public Texture m_texture;

    #endregion

    #region Methods



    // Use this for initialization
    void Start()
    {
        m_actDegreePec = 0;
        m_actBurnPec = 0;

        GetComponentInChildren<MeshRenderer>().material.mainTexture = m_texture;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.LookRotation( -Camera.main.transform.forward, Vector3.up );
        transform.forward = Camera.main.transform.forward;

        // Update Text
        TextMesh theMesh = transform.Find( "StatusText" ).gameObject.GetComponent<TextMesh>();
        theMesh.text = (int)m_actDegreePec + "%";
    }

    #endregion
}
