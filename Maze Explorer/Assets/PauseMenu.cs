using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public PlayerMovement playerMove;
    public GameObject pauseMenu;
    public GameObject controlsInfo;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("/Canvas");
        pauseMenu = canvas.transform.Find("PauseMenu").gameObject;
        controlsInfo = canvas.transform.Find("Controls").gameObject;
        playerMove = GameObject.Find("/knight").GetComponent<PlayerMovement>();
        //pauseMenu = GameObject.Find("/Canvas/PauseMenu");
        //controlsInfo = GameObject.Find("/Canvas/Controls");
        controlsInfo.SetActive(false);
    }

    public void ResumeGame()
    {
        playerMove.Resume();
    }

    public void BackToTitle()
    {
        playerMove.Resume();
        InventoryManagement.CurrentLevel = 0;
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Controls()
    {
        pauseMenu.SetActive(false);
        controlsInfo.SetActive(true);
    }

    public void Back()
    {
        controlsInfo.SetActive(false);
        pauseMenu.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
