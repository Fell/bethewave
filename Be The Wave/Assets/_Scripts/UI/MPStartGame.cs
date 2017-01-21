using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPStartGame : MenuPoint {

    public override void MenuAction()
    {
        GameManager.Instance.StartGame();
    }
}
