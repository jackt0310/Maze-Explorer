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

    public void ArrowUpgrade()
    {
        if(playerMove.gold >= 1000)
        {
            playerMove.gold -= 1000;
            InventoryManagement.GoldAmt = playerMove.gold;
            InventoryManagement.MaxArrows = InventoryManagement.MaxArrows + 15;
        }
    }

    public void GrenadeUpgrade()
    {
        if (playerMove.gold >= 1000)
        {
            playerMove.gold -= 1000;
            InventoryManagement.GoldAmt = playerMove.gold;
            InventoryManagement.MaxGrenades = InventoryManagement.MaxGrenades + 3;
        }
    }

    public void HealthUpgrade()
    {
        if (playerMove.gold >= 5000)
        {
            playerMove.gold -= 5000;
            InventoryManagement.GoldAmt = playerMove.gold;
            playerMove.maxHealth += 50;
            playerMove.health = playerMove.maxHealth;
            InventoryManagement.MaxHealth = playerMove.maxHealth;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
