using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    public GameObject canvas;
    public GameObject mainMenu;
    public GameObject difficultyMenu;

    void Start()
    {
        canvas = GameObject.Find("/Canvas");
        mainMenu = canvas.transform.Find("MainMenu").gameObject;
        difficultyMenu = canvas.transform.Find("DifficultyMenu").gameObject;
    }

    public void StartHard()
    {
        InventoryManagement.Difficulty = "Hard";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartMedium()
    {
        InventoryManagement.Difficulty = "Medium";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartEasy()
    {
        InventoryManagement.Difficulty = "Easy";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartGame()
    {
        difficultyMenu.SetActive(true);
        mainMenu.SetActive(false);
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
