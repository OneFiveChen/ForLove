using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {
    public GameObject PressKey;
    public GameObject StartKey;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void PressKeySetActive()
    {
        PressKey.SetActive(true);
    }
}
