using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonScript : MonoBehaviour
{
    public bool pursuit = false;
    public GameObject player;
    public float moveSpeed = 7f;
    private Animator animator;
    bool hasHowled = false;
    public AudioSource demonHowl;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pursuit)
        {
            if (player != null)
            {
                animator.SetBool("Running", true);
                transform.LookAt(player.transform);
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            } else
            {
                animator.SetBool("Running", false);
                if(!hasHowled)
                {
                    animator.SetTrigger("Howl");
                    Invoke("HowlSound", 1f);
                    
                    hasHowled = true;
                }
                
            }
        } else
        {
            animator.SetBool("Running", false);
        }
    }

    void HowlSound()
    {
        demonHowl.Play();
    }
}
