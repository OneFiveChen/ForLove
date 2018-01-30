using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public float MoveSpeed;
    public float StopSmoothTime;
    public SpermController controller;
    public bool canMove = false;
    Vector3 stopPos;
    Vector3 velocity = Vector3.zero;
    bool smoothFlag =false;
    Vector3 smoothTarget;
    bool cameraJump = false;
    GameObject cameraTarget;
    float timer = 0f;

	// Use this for initialization
	void Start () {
        GameObject temp = GameObject.Find("Destination(Clone)");
        // Debug.Log(stopPos.transform.position);
        stopPos = new Vector3(temp.transform.position.x, temp.transform.position.y, -10);
        cameraTarget = GameObject.Find("CameraTargetPrefab(Clone)");
	}
	
	// Update is called once per frame
	void Update () {
        if(cameraJump)
        {
            timer++;
            StartCameraJump();
        }
        CheckStop();
        if (canMove)
        {
            transform.Translate(Vector3.up * MoveSpeed * Time.deltaTime);
        }
        CheckOver();
       // Debug.Log(canMove);
	}

    public void CameraGo()
    {
        cameraJump = true;
       // canMove = true;
    }

    void CheckStop()
    {
        Vector3 distance = transform.position - stopPos;
        Vector2 distance2D = new Vector2(distance.x, distance.y);
      //  Debug.Log("Distance ="+distance2D.magnitude);
        if(distance2D.magnitude<=5)
        {
            canMove = false;
            transform.position = Vector3.SmoothDamp(transform.position, stopPos, ref velocity, StopSmoothTime);
            Debug.Log("canmove++++++"+canMove);
        }
    }

    void CheckOver()
    {
        if(controller.overFlag && !smoothFlag)
        {
            canMove = false;
            smoothFlag = true;
            smoothTarget = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            if(smoothTarget.y>stopPos.y)
            {
                smoothTarget = stopPos;
            }
        }
        if (smoothFlag)
        {
            transform.position = Vector3.SmoothDamp(transform.position, smoothTarget, ref velocity, 1f);
          //  Debug.Log("test");
        }
    }

    void StartCameraJump()
    {
        //GameObject spermClone = GameObject.Find("CameraTargetPrefab(Clone)");
        if(cameraTarget.GetComponent<Rigidbody2D>().velocity.y<1.5f && timer>60)
        {
            cameraJump = false;
            canMove = true;
            Debug.Log("okokok");
            Destroy(cameraTarget);
        }
        if (cameraTarget.transform.position.y > this.gameObject.transform.position.y)
        {
            this.gameObject.transform.position = new Vector3(transform.position.x, cameraTarget.transform.position.y, transform.position.z);
            Debug.Log("xxxxxx");
        }
    }
}
