﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microwave : MonoBehaviour
{
    public Transform m_plateSpawn;

    private Plate m_plate;

    public void SetPlate( Plate p_plate )
    {
        m_plate = Instantiate( p_plate, m_plateSpawn );
        m_plate.transform.localPosition = Vector2.zero;
    }

    public void StartWaving()
    {

    }

}
