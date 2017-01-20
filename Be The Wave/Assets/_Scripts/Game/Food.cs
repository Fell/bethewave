using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    #region Fields
    

    public int m_reqStrengthLv;
    
    public int m_actDegreePec;
    public int m_degreePecRaise;

    public int m_actBurnPec;
    public int m_burnPecRaise;

    int m_neededDegree = 100;

    #endregion

    #region Methods



    // Use this for initialization
    void Start()
    {
        m_actDegreePec = 0;
        m_actBurnPec = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = -Camera.main.transform.forward;
    }

    #endregion
}
