using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPStartGame : MenuPoint {

    int m_levelID = 0; 

    public override void MenuAction()
    {
        Debug.Log(m_levelID);
        //GameManager.Instance.StartGame();
    }

    public void setLevelID(int _id)
    {
        m_levelID = _id;
    }
}
