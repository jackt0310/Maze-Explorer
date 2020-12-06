using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenScript : MonoBehaviour
{
    public DialogueScript dialogue;
    public GameObject player;
    public GameObject dots;
    public Sprite talkSprite;
    public string text;

    public GameObject shopMenu;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = GameObject.Find("/DialogueControl").GetComponent<DialogueScript>();
        player = GameObject.Find("/knight");
        dots.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 8f)
        {
            dots.SetActive(true);
            if (Input.GetKeyDown(KeyCode.C) && !dialogue.open && dialogue.timeSinceLast > .1f)
            {
                shopMenu.SetActive(true);
                dialogue.OpenDialogue(talkSprite, text, gameObject);
            }
        } else
        {
            dots.SetActive(false);
        }
    }

    public void Close()
    {
        shopMenu.SetActive(false);
    }
}
