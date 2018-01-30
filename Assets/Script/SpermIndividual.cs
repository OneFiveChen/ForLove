using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpermIndividual : MonoBehaviour {
    public float EndofMass;
    public float StartofMass;
    public bool GetDestination = false;
    Camera mainCamera;
	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;
       // GetComponent<Rigidbody2D>().mass = EndofMass;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
            Debug.Log("Die");
        }
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy((this.gameObject));
                    Debug.Log("Die");
        }
    }


    private void OnBecameInvisible()
    {
        if (mainCamera != null)
        {
            float distance = this.transform.position.y - mainCamera.gameObject.transform.position.y;
//            Debug.Log(distance);
            if (distance < 0)
            {
                Destroy(this.gameObject);
            }
            //Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DestinationCore")
        {
            GetComponent<Rigidbody2D>().mass = EndofMass;
            Debug.Log("enter success!!!");
        }
        if (collision.gameObject.name == "InCore")
        {
            GetComponent<Rigidbody2D>().mass = EndofMass*3;
            GetDestination = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DestinationCore")
        {
            GetComponent<Rigidbody2D>().mass = StartofMass;
            Debug.Log("Success");
        }
        if(collision.gameObject.name =="InCore")
        {
            GetComponent<Rigidbody2D>().drag = 1000;
        }
    }
}
