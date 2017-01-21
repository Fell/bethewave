using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microwave : MonoBehaviour
{
    public Transform m_plateSpawn;

    public void SetPlate( Plate p_plate )
    {
        var _plate = Instantiate( p_plate, m_plateSpawn );
        _plate.transform.localPosition = Vector2.zero;
    }

}
