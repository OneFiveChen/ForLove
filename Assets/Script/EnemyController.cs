using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject[] Enemies;
    public GameObject EnemyPrefab;
    public Vector2 spawnValue;
    public float startWait;
    public float spawnWait;
    public float waveWait;
    public int EnemyCount;

    public CameraMove cameramove;
    public Camera camera2;
    
    bool tag2 = true;
    Vector3 stopPos;


    // Use this for initialization
    void Start () {
        
        //StartCoroutine(WaitDestory());
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(cameramove.canMove);
        //Debug.Log(tag2);

        
        if(tag2 && cameramove.canMove)
        {
            StartCoroutine(SpawnWaves());
            tag2 = false;
        }

    }


    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < EnemyCount; ++i)
            {
                GameObject Enemy = Enemies[Random.Range(0, Enemies.Length)];
                Enemy.transform.position = new Vector3(camera2.transform.position.x + Random.Range(-1f,1f),
                                          camera2.transform.position.y + 8 + Random.Range(-2f, 2f),
                                          camera2.transform.position.z + 10);
                
                Enemy = (GameObject)Instantiate(EnemyPrefab);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

        }
    }

    void CheckStop()
    {
        Vector3 distance = transform.position - stopPos;
        Vector2 distance2D = new Vector2(distance.x, distance.y);
        //  Debug.Log("Distance ="+distance2D.magnitude);
        if (distance2D.magnitude <= 5)
        {
            Destroy(this.gameObject);
        }
    }

}
