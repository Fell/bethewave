using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPMenuBack : MenuPoint {

    public override void MenuAction()
    {
        transform.parent.GetComponent<MainMenu>().CreateMenuPoints();
    }
}
