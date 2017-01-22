using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPBackToMain : ResultOption
{
    public override void InvokeAction()
    {
        UIManager.Instance.StartCoroutine( UIManager.Instance.CloseResultScreen( GameManager.Instance.StopGame ) );
    }
}
