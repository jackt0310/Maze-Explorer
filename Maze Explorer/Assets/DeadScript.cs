using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadScript : MonoBehaviour
{
    public GameObject restart;
    // Start is called before the first frame update
    void Start()
    {
        restart = GameObject.Find("/Canvas/RestartText");
        Invoke("RestartText", 3f);
    }

    void RestartText()
    {
        restart.GetComponent<Text>().enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
