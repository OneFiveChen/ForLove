using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackColor : MonoBehaviour {
    Camera mainCamera;
    float timer;
    //public float TimeToChange;
    public Color[] ColorList;
    int currentColorNumber;
    //public float Duration;
	// Use this for initialization
	void Start () {
        mainCamera = GetComponent<Camera>();
       // currentColorNumber = Random.Range(0, ColorList.Length - 1);
        mainCamera.backgroundColor = ColorList[Random.Range(0, ColorList.Length - 1)];
	}
	
	// Update is called once per frame
	void Update () {
       // TimeMachine();

	}

    //void TimeMachine()
    //{
    //    timer++;
    //    if(timer>=TimeToChange)
    //    {
    //        timer = 0;
    //        if (currentColorNumber == ColorList.Length - 1)
    //        {
    //            currentColorNumber = 0;
    //        }
    //        else
    //        {
    //            currentColorNumber++;
    //        }
    //    }
    //    if((TimeToChange-timer)<300 && (TimeToChange - timer) > 0)
    //    {
    //        float lerp = Mathf.PingPong(Time.time, Duration) / Duration;
    //        if (currentColorNumber == (ColorList.Length - 1))
    //        {
    //            mainCamera.backgroundColor = Color.Lerp(ColorList[currentColorNumber], ColorList[0], lerp);
    //        }else{
    //            mainCamera.backgroundColor = Color.Lerp(ColorList[currentColorNumber], ColorList[currentColorNumber + 1], lerp);
    //        }
    //    }
    //}
}
