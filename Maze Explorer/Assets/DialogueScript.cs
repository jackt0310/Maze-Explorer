using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    public GameObject diaPanel;
    public Image diaPortrait;
    public Text diaText;
    public bool open;
    public bool canMoveOn = false;

    // Start is called before the first frame update
    void Start()
    {
        diaPanel = GameObject.Find("/Canvas/DialoguePanel");
        diaPortrait = GameObject.Find("/Canvas/DialoguePanel/DiaBox/DiaPortrait").GetComponent<Image>();
        diaText = GameObject.Find("/Canvas/DialoguePanel/DiaBox/DiaText").GetComponent<Text>();
        diaPanel.SetActive(false);
    }


    public void OpenDialogue(Sprite picture, string text)
    {
        diaPanel.SetActive(true);
        diaPortrait.sprite = picture;
        diaText.text = text;
        open = true;
        Invoke("Continue", .1f);
    }

    public void Continue()
    {
        canMoveOn = true;
    }

    public void CloseDialogue()
    {
        diaPanel.SetActive(false);
        open = false;
        canMoveOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && open && canMoveOn)
        {
            CloseDialogue();
        }
    }
}
