using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {
    
    public GameObject Panel;
    ButtonController buttonController;

    void Start()
    {
        buttonController = Panel.GetComponent<ButtonController>();
    }

    public void StartGame()
    {
        GameObject controller = GameObject.Find("SpermCollection");
        controller.GetComponent<SpermController>().StartButtonFlag = true;
        buttonController.PressKeySetActive();
       // PressButton.SetActive(true);
        Destroy(this.gameObject);
    }

    public void RestarGame()
    {
        SceneManager.LoadScene("Main");
    }


}
