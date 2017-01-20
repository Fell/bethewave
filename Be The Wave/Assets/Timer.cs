using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        TextMesh theMesh = gameObject.GetComponent<TextMesh>();

        string newText = "00";

        int seconds = (int)Time.realtimeSinceStartup;

        if(seconds % 2 == 0)
            newText += ":";
        else
            newText += " ";

        newText += seconds.ToString("D2"); ;

        theMesh.text = newText;
	}
}
