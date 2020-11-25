using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    PlayerMovement playerMove;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = GameObject.Find("/knight").GetComponent<PlayerMovement>();
    }

    public void HymnBuy()
    {
        if(playerMove.gold >= 500 && playerMove.songsUnlocked < playerMove.unlockedMusic.Length)
        {
            playerMove.gold -= 500;
            InventoryManagement.GoldAmt = playerMove.gold;
            playerMove.changeSong(playerMove.unlockNextSave());
            playerMove.checkIfDoneMusic();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
