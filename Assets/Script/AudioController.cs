using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public static bool IsClone = false;
    public AudioSource BGM;
    public GameObject Touch;
    public AudioSource Die;
    public AudioSource Success;
    public AudioSource ElasticAudio;
    public AudioSource BoomAudio;
    bool lerpBgm = false;
    bool elasticCan = false;
    //  GameObject _BGM;

    private void Start()
    {
        //if (!IsClone)
        //{
        //    _BGM = (GameObject)Instantiate(BGM);
        //    DontDestroyOnLoad(_BGM);
        //    IsClone = true;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(lerpBgm)
        {
            BGM.volume = Mathf.Lerp(BGM.volume, 0f,Time.deltaTime);
            if(BGM.volume <=0.05)
            {
                lerpBgm = false;
                BGM.Stop();
            }
        }
    }

    public void PlayTouchAudio()
    {
        //Touch.audio
        GameObject touchOne = (GameObject)Instantiate(Touch);
        Destroy(touchOne, 0.2f);
    }

    public void StopBGM()
    {
        //BGM.Stop();
        lerpBgm = true;
    }

    public void PlayDie()
    {
        Die.Play();
    }

    public void PlaySuccess()
    {
        Success.Play();
    }

    public void ElasticGO()
    {
      //  Debug.Log("Play!!!");
        if (!elasticCan)
        {
            elasticCan = true;
         //   Debug.Log("Play!!!!");
            ElasticAudio.Play();
        }
    }

    public void ElasticStop()
    {
        ElasticAudio.Stop();
    }

    public void PlayBoom()
    {
        BoomAudio.Play();
    }

}
