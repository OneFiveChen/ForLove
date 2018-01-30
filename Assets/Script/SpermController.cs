using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpermController : MonoBehaviour {
    
    public float ForceRate;
    public float InvertRate;
    public float MaxDistance;
    public float MaxStartPower;
    public float MinStartPower;
    public int AccumulatingSpeed;
    public CameraMove cameraMove;
    public int SpermCount;
    public Transform StartPos;
    public GameObject SpermPrefab;
    public bool StartButtonFlag = false;
    public GameObject RestartButton;
    public GameObject CameraTargetPrefab;
    public GameObject ClickEffectPrefab;
    public GameObject GameOverSprite;
    public GameObject Congradulations;
    public GameObject ResultScore;
    public GameObject PressKey;
    public AudioController audioControl;
    public Text Score;
    GameObject[] sperm;
    GameObject cameraTarget;
    bool canStart = false;
    float StartPower;
    int timer;
    int remainCount;

    bool remainNextFrameToStart = false;

    public bool overFlag = false;

    public Transform shrinkPoint;
    public float speed;

	// Use this for initialization
	void Start () {
        //sperm = this.GetComponentsInChildren<Transform>();
        sperm = new GameObject[SpermCount];
        for (int i = 0; i < SpermCount; i++)
        {
            sperm[i] = (GameObject)Instantiate(SpermPrefab);
            sperm[i].transform.position = new Vector3(StartPos.position.x + Random.Range(-0.7f, 0.7f),
                                                      StartPos.position.y + Random.Range(-0.7f, 0.7f),
                                                      StartPos.position.z);
        }
        //sperm[SpermCount] = (GameObject)Instantiate(CameraTargetPrefab);
        //sperm[SpermCount].transform.position = new Vector3(StartPos.position.x,
        //StartPos.position.y + 0.6f,
        //StartPos.position.z);
        cameraTarget = (GameObject)Instantiate(CameraTargetPrefab);
        cameraTarget.transform.position = new Vector3(StartPos.position.x,
                                                     StartPos.position.y + 0.6f,
                                                     StartPos.position.z);

    }


    // Update is called once per frame
    void Update () {
        InstantiateClickEffect();
        if(!StartButtonFlag)
        {
            return;
        }
        if(!canStart && remainNextFrameToStart)
        {
            MouseStart();
            TouchStart();
        }
        if (canStart)
        {
            TouchFun();
            MouseFun();
        }
        if(timer<3)
        {
            timer++;
        }else{
            timer = 0;
            CheckGameOver();
        }
        if(StartButtonFlag)
        {
            remainNextFrameToStart = true;
        }
       // InstantiateClickEffect();
	}

    void TouchFun()
    {
        if (Input.touchCount == 0) return;
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //RaycastHit hitt = new RaycastHit();
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Physics.Raycast(ray, out hitt);
            Vector3 tempMouse = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 temp = new Vector2(tempMouse.x, tempMouse.y);
            GiveForce(temp);

        //    InstantiateClickEffect(tempMouse);
        }
        //if (Input.GetTouch(1).phase == TouchPhase.Began)
        //{
        //    //RaycastHit hitt = new RaycastHit();
        //    //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    //Physics.Raycast(ray, out hitt);
        //    Vector3 tempMouse = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position);
        //    Vector2 temp = new Vector2(tempMouse.x, tempMouse.y);
        //    GiveForce(temp);

        ////   InstantiateClickEffect(tempMouse);
        //}
    }

    void MouseFun()
    {
        if(Input.GetMouseButton(0) && Input.GetMouseButtonDown(0))
        {
            //RaycastHit hitt = new RaycastHit();
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Physics.Raycast(ray, out hitt);
            Vector3 tempMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 temp = new Vector2(tempMouse.x, tempMouse.y);
            GiveForce(temp);

           // InstantiateClickEffect(tempMouse);
        }
    }

    void MouseStart()
    {
        if(Input.GetMouseButton(0))
        {
            StartPower += AccumulatingSpeed;
            Shrink();
            audioControl.ElasticGO();
//            Debug.Log(StartPower);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Start");
            if(StartPower>=MaxStartPower)
            {
                StartPower = MaxStartPower;
            }
            if(StartPower<=MinStartPower)
            {
                StartPower = MinStartPower;
            }
            for (int i = 0; i < sperm.Length; i++)
            {
                Vector2 temp = Vector2.up * StartPower;
               // Debug.Log(temp);
                sperm[i].GetComponent<Rigidbody2D>().AddForce(temp);
                //Debug.Log(sperm[i].GetComponent<Rigidbody2D>().velocity);
            }
            Vector2 forceVec = Vector2.up * StartPower;
            cameraTarget.GetComponent<Rigidbody2D>().AddForce(forceVec);
            canStart = true;
            cameraMove.CameraGo();
            audioControl.ElasticStop();
            audioControl.PlayBoom();
            PressKey.SetActive(false);
        }
    }

    void TouchStart()
    {
        if (Input.touchCount > 0)
        {
            if (!canStart)
            {
                StartPower += AccumulatingSpeed;
                audioControl.ElasticGO();
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (StartPower >= MaxStartPower)
                {
                    StartPower = MaxStartPower;
                }
                if (StartPower <= MinStartPower)
                {
                    StartPower = MinStartPower;
                }
                for (int i = 0; i < sperm.Length; i++)
                {
                    Vector2 temp = Vector2.up * StartPower;
                  //  Debug.Log(temp);
                    sperm[i].GetComponent<Rigidbody2D>().AddForce(temp);        //后续需要加入方向扰动
                                                                                //Debug.Log(sperm[i].GetComponent<Rigidbody2D>().velocity);
                }
                Vector2 forceVec = Vector2.up * StartPower;
                cameraTarget.GetComponent<Rigidbody2D>().AddForce(forceVec);
                canStart = true;
                cameraMove.CameraGo();
                audioControl.ElasticStop();
                audioControl.PlayBoom();
                PressKey.SetActive(false);
            }
        }
    }

    void GiveForce(Vector2 vec)
    {
        for (int i = 0; i < sperm.Length; i++)
        {
            if (sperm[i] != null)
            {
                Vector2 forceVec = new Vector2(sperm[i].transform.position.x - vec.x,
                                               sperm[i].transform.position.y - vec.y);
                if (forceVec.magnitude <= MaxDistance)
                {
                    Vector2 normalForceVec = forceVec.normalized;
                    sperm[i].GetComponent<Rigidbody2D>().AddForce(normalForceVec * ForceRate * Invert(forceVec.magnitude));
                 //   Debug.Log(normalForceVec * ForceRate * Invert(forceVec.magnitude));
                }
            }
        }
    }

    float Invert(float x)
    { 
        return -x + InvertRate;
    }

    void CheckGameOver()
    {
        for (int i = 0; i < sperm.Length; i++)
        {
           // Debug.Log("For");
            if (sperm[i] != null && !sperm[i].GetComponent<SpermIndividual>().GetDestination)
            {
                Debug.Log("return");
                return;
            }

            if(i == sperm.Length-1 && !overFlag)
            {
                GameOver();
                overFlag = true;

            }
        }
    }

    void GameOver()
    {
        Debug.Log("GameOver");
        for (int i = 0; i < sperm.Length; i++)
        {
            if(sperm[i] != null)
            {
                remainCount++;

            }
        }
        if(remainCount == 0)
        {
           // Debug.Log("Lose");
            GameOverSprite.SetActive(true);
            audioControl.PlayDie();
        }
        if(remainCount > 0)
        {
            Congradulations.SetActive(true);
            ResultScore.SetActive(true);
            Score.text = remainCount.ToString();
            audioControl.PlaySuccess();
        }
        audioControl.StopBGM();
        RestartButton.SetActive(true);
    }

    void Shrink()
    {
        foreach(GameObject spermsingle in sperm)
        {
            float step = speed * Time.deltaTime;
            spermsingle.transform.localPosition = Vector3.MoveTowards(spermsingle.transform.localPosition, shrinkPoint.localPosition, step);
        }
    }

    void InstantiateClickEffect()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 tempPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject temp = (GameObject)Instantiate(ClickEffectPrefab);
            temp.transform.position = new Vector3(tempPos.x,tempPos.y,2);
            Destroy(temp,2f);
            audioControl.PlayTouchAudio();
         //   Debug.Log("Effect");
        }
        if(Input.touchCount>0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 tempPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                GameObject temp = (GameObject)Instantiate(ClickEffectPrefab);
                temp.transform.position = new Vector3(tempPos.x, tempPos.y, 2);
                Destroy(temp, 0.5f);
                audioControl.PlayTouchAudio();
            }
            //if(Input.GetTouch(1).phase == TouchPhase.Began)
            //{
            //    Vector3 tempPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position);
            //    GameObject temp = (GameObject)Instantiate(ClickEffectPrefab);
            //    temp.transform.position = new Vector3(tempPos.x, tempPos.y, 2);
            //    Destroy(temp, 0.5f);
            //}
        }
    }
}
