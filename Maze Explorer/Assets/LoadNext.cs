using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadNext : MonoBehaviour
{
    AsyncOperation loadingOperation;
    Text loadingText;
    float count = 1f;
    // Start is called before the first frame update
    void Start()
    {
        loadingText = GameObject.Find("/Canvas/LoadingProgressText").GetComponent<Text>();
        Invoke("Next", 0.01f);
    }

    void Next()
    {
        loadingOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    // Update is called once per frame
    void Update()
    {
        if(loadingOperation != null)
        {
            loadingText.text = "Loading: " + loadingOperation.progress + "%";
        } else
        {
            loadingText.text = "" + count;
            count++;
        }
    }
}
