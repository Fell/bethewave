using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPEndGame : MenuPoint {

    public override void MenuAction()
    {
        Debug.Log("End");
        Application.Quit();
    }
}
