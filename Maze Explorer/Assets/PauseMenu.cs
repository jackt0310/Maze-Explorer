using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public PlayerMovement playerMove;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = GameObject.Find("/knight").GetComponent<PlayerMovement>();
    }

    public void ResumeGame()
    {
        playerMove.Resume();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
