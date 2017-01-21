using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerMeter : MonoBehaviour
{

    public float m_sinkRate = 40f;
    public float m_increaseStep = 5f;


    public float Value { get; private set; }


    private Dictionary<string, float> m_markers = new Dictionary<string, float>();

    private float m_lerpedValue = 0;
    private float m_lerpT = 1.0f;



    public Transform marker;



    // Use this for initialization
    void Start()
    {
        this.addMarker( "Karotte", 77 );
        this.addMarker( "Eis", 38 );
    }

    // Update is called once per frame
    void Update()
    {

        if ( Input.GetKeyDown(KeyCode.Space) )
        {
            Value += m_increaseStep;
            m_lerpT = 0;
        }
        else
            if ( Value > 0 ) Value -= m_sinkRate * ( Mathf.Max( Value, 1 ) / 100 ) * Time.deltaTime;

        if ( Value > 100 ) Value = 100;
        if ( Value < 0 ) Value = 0;

        // ---

        if ( m_lerpT < 1 ) m_lerpT += Time.deltaTime;
        m_lerpedValue = Mathf.Lerp( m_lerpedValue, Value, m_lerpT );

        GameObject indicator = transform.Find( "Indicator" ).gameObject;
        indicator.transform.localPosition = new Vector3( 0.5f, ( m_lerpedValue - 50 ) / 10, 0 );


    }

    void OnGUI()
    {
        /*
        Texture2D color = new Texture2D( 1, 1 );
        color.SetPixel( 0, 0, new Color( 1, 0, 0 ) );
        color.Apply();

        GUI.skin.box.normal.background = color;
        GUI.Box( new Rect( 100, 150, m_lerpedValue * 5, 10 ), GUIContent.none );

        foreach ( KeyValuePair<string, float> marker in m_markers )
        {
            GUI.Box( new Rect( 100 + marker.Value * 5, 135, 2, 10 ), GUIContent.none );
        }



        string label = "Power Meter: " + m_lerpedValue;

        foreach ( KeyValuePair<string, float> marker in m_markers )
        {
            label += "\n" + marker.Key + ": " + marker.Value;
        }

        GUI.Label( new Rect( 100, 100, 100, 200 ), label );
        */
    }

    public void addMarker( string name, float value )
    {
        m_markers.Add( name, value );

        //Transform newMarker = Instantiate(marker, new Vector3(-0.5f, (value - 50) / 10, 0) + transform.position, Quaternion.Euler(0,0,90), transform);
        Transform newMarker = Instantiate( marker, transform.position, transform.rotation, transform );
        newMarker.transform.localRotation = Quaternion.Euler( 0, 0, 90 );
        newMarker.transform.localPosition = new Vector3( -0.5f, ( value - 50 ) / 10, 0 );

        newMarker.Find( "Label" ).gameObject.GetComponent<TextMesh>().text = name;
    }

    public float getDeviation( string markerName )
    {
        return m_lerpedValue - m_markers[ markerName ];
    }
}
