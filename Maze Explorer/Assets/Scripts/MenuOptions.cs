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
        InventoryManagement.GrenadeAmt = InventoryManagement.MaxGrenades;
        InventoryManagement.ArrowAmt = InventoryManagement.MaxArrows;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartMedium()
    {
        InventoryManagement.Difficulty = "Medium";
        InventoryManagement.GrenadeAmt = InventoryManagement.MaxGrenades * 2;
        InventoryManagement.ArrowAmt = InventoryManagement.MaxArrows * 2;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartEasy()
    {
        InventoryManagement.Difficulty = "Easy";
        InventoryManagement.GrenadeAmt = 999;
        InventoryManagement.ArrowAmt = 999;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartGame()
    {
        difficultyMenu.SetActive(true);
        mainMenu.SetActive(false);
        
    }

    public void ArenaMode()
    {
        InventoryManagement.Difficulty = "Medium";
        InventoryManagement.GrenadeAmt = 999;
        InventoryManagement.ArrowAmt = 999;
        SceneManager.LoadScene("Arena");
    }

    public void Tavern()
    {
        SceneManager.LoadScene("Tavern");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
