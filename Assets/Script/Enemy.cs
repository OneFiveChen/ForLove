using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    bool hasCollider;
    public float xSpeed = 0.01f;
    public float ySpeed = 0.2f;
    public float zSpeed = 0.01f;
    //Camera camera;
    // Use this for initialization
    void Start () {
        Camera camera = Camera.main;

	}
	
	// Update is called once per frame
	void Update () {
        /* Ray ray = new Ray(transform.position, -transform.forward);

         RaycastHit hit;
         if(Physics.Raycast(ray,out hit,Mathf.Infinity))
         {
             //如果射线与平面碰撞，打印碰撞物体信息  
             Debug.Log("碰撞对象： " + hit.collider.name);
             // 在场景视图中绘制射线  
             Debug.DrawLine(ray.origin, hit.point, Color.red);

         }*/

        /*Ray2D ray2D = new Ray2D(transform.position, transform.forward);
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, -Vector2.up);
        if(Physics2D.Raycast(transform.position,Vector2.down))
        {
            hasCollider = true;
            //Destroy(this.gameObject);
            //如果射线与平面碰撞，打印碰撞物体信息  
            Debug.Log("碰撞对象： " +hit2D.collider.name);
            // 在场景视图中绘制射线  
            Debug.DrawLine(ray2D.origin, hit2D.point, Color.red);


        }*/


        transform.Translate(this.transform.position.x * Time.deltaTime * xSpeed, -ySpeed * Time.deltaTime,this.transform.position.z * Time.deltaTime * zSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }

}
