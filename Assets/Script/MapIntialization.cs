using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIntialization : MonoBehaviour {
    public int StageOneCount;
    public GameObject Back1;
    public GameObject Back2;
    public GameObject PreDestinationPrefab;
    public GameObject DestinationPrefab;
    public GameObject[] AbstaclePrefab;
    public int AbstacleCount;
    GameObject[] abstacle;
    GameObject[] Level;
    GameObject preDestination;
    GameObject destination;
    Vector3 LevelPos;
	// Use this for initialization
	void Start () {
        LevelPos = new Vector3(0, 0, 1);
        Level = new GameObject[StageOneCount];
        abstacle = new GameObject[AbstacleCount];
        for (int i = 0; i < StageOneCount; i++)
        {
            if (Random.Range(0f, 1f) >= 0.5)
            {
                Level[i] = (GameObject)Instantiate(Back1);
                Level[i].transform.position = LevelPos;
                LevelPosUp();
            }
            else
            {
                Level[i] = (GameObject)Instantiate(Back2);
                Level[i].transform.position = LevelPos;
                LevelPosUp();
            }
        }
        preDestination = Instantiate(PreDestinationPrefab);
        preDestination.transform.position = LevelPos;
        LevelPosUp();

        destination = (GameObject)Instantiate(DestinationPrefab);
        destination.transform.position = LevelPos;
        LevelPosUp();
        Initialization();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LevelPosUp()
    {
        LevelPos = new Vector3(LevelPos.x,
                       LevelPos.y + 12.8f,
                       LevelPos.z);
    }

    void Initialization()
    {
        for (int i = 0; i < AbstacleCount; i++)
        {
            float tempFloat = Random.Range(0, 3f);
            if (tempFloat <= 1)
            {
                abstacle[i] = (GameObject)Instantiate(AbstaclePrefab[0]);
            }
            if(tempFloat>1&& tempFloat<=2)
            {
                abstacle[i] = (GameObject)Instantiate(AbstaclePrefab[1]);
            }
            if(tempFloat>2&&tempFloat<=3)
            {
                abstacle[i] = (GameObject)Instantiate(AbstaclePrefab[2]);
            }
            float tempX = Random.Range(-2f, 2f);
            float tempY = Random.Range(-6f, 6f);
           // transform.position = new Vector3(transform.position.x + tempX, transform.position.y + tempY, transform.position.z);
            Vector3 tempVec = Level[Random.Range(1, StageOneCount)].transform.position;
            abstacle[i].transform.position = new Vector3(tempVec.x + tempX, tempVec.y + tempY, 0);
        }
    }
}
