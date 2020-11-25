using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    public GameObject player;
    Rigidbody rb;
    public float moveSpeed = 5f;
    public AudioSource ghostDeath;
    public bool wander = false;

    public GameControl plane;
    public float yCoord = 0f;
    public Vector3 nextPoint;

    public bool moving = false;
    public GameControl control;
    public bool goStart = false;
    public Animator animator;

    public GameObject gold;


    // Start is called before the first frame update
    void Start()
    {
        gold = Resources.Load<GameObject>("Gold");
        control = GameObject.Find("/Plane").GetComponent<GameControl>();
        if (!wander)
        {
            player = GameObject.Find("/knight");
            ghostDeath = GameObject.Find("Main Camera/GhostDeath").GetComponent<AudioSource>();
        }
        else
        {
            plane = GameObject.Find("/Plane").GetComponent<GameControl>();
            if (goStart)
            {
                Go();
            }

            if(GetComponent<Animator>())
            {
                animator = GetComponent<Animator>();
            }
        }
    }
    public void Go()
    {
        if(!moving)
        {
            float spawnX = Random.Range(plane.minX, plane.maxX);
            float spawnZ = Random.Range(plane.minZ, plane.maxZ);
            nextPoint = new Vector3(spawnX, yCoord, spawnZ);
            
            moving = true;
        }
        
    }

    public void Pause()
    {
        Invoke("Go", Random.Range(3f, 6f));
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null && !wander)
        {
            transform.LookAt(player.transform);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }

        if(animator != null)
        {
            animator.SetBool("isWalking", moving);
        }

        if(wander)
        {
           
            if (Vector3.Distance(transform.position, nextPoint) < 5f)
            {
                moving = false;
                Pause();
            }
            if (moving)
            {
                transform.LookAt(nextPoint);
                transform.position = Vector3.MoveTowards(transform.position, nextPoint, moveSpeed * Time.deltaTime);
            }
        }

    }

    public void Die()
    {
        GameObject goldSpawn= Instantiate(gold, transform.position, transform.rotation);
        goldSpawn.transform.Rotate(-90f, 0f, 0f);
        control.bozuAmt--;
        ghostDeath.Play();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "attack")
        {
            Die();
        }
    }
}
