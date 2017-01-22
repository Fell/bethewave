using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPSelectLevel : MenuPoint {

    public override void MenuAction()
    {
        transform.parent.GetComponent<MainMenu>().createLevelPoints();
    }
}
