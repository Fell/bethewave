using System.Collections;
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
        SoundManager sound = this.transform.Find("Audio Source").gameObject.GetComponent<SoundManager>();
        StartCoroutine(sound.PlayStartup());
    }

<<<<<<< HEAD
    public Plate GetPlate()
    {
        return m_plate;
    }
=======
    public Food[] GetFoods()
    {
        return m_plate.GetFoods();
    } 
>>>>>>> 63e32c558220656caf104505340c52db52a1a572

}
