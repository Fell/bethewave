using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class MPNextLevel : ResultOption
{
    public override void InvokeAction()
    {
        UIManager.Instance.StartCoroutine( UIManager.Instance.CloseResultScreen( GameManager.Instance.OpenNextScene ) );
    }
}
