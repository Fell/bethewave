using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPStartGame : MenuPoint {

    public TextMesh m_indexIndicator;

    int m_levelID = 0;

    public override void Start()
    {
        base.Start();
        m_indexIndicator.text = (m_levelID + 1) + "";
    }

    public override void MenuAction()
    {
        GameManager.Instance.StartGame(m_levelID);
    }

    public void setLevelID(int _id)
    {
        m_levelID = _id;
    }
}
