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
    public GameObject arrows;
    public GameObject grenades;
    public GameObject food;
    public PlayerMovement playerMove;

    // Start is called before the first frame update
    void Start()
    {
        gold = Resources.Load<GameObject>("Gold10");
        arrows = Resources.Load<GameObject>("ArrowCollect");
        grenades = Resources.Load<GameObject>("GrenadeCollect");
        food = Resources.Load<GameObject>("Food");
        control = GameObject.Find("/Plane").GetComponent<GameControl>();
        if (!wander)
        {
            player = GameObject.Find("/knight");
            playerMove = player.GetComponent<PlayerMovement>();
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
        

        float arrowNeed = 0f;

        if (playerMove.arrowAmt < playerMove.maxArrows)
        {
            arrowNeed = 8f;
        }

        float grenadeNeed = 0f;

        if(playerMove.grenadeAmt < playerMove.maxGrenades)
        {
            grenadeNeed = 8f;
        }

        float foodNeed = 0f;

        if(playerMove.health < playerMove.maxHealth)
        {
            foodNeed = 8f;
        }

        float random = Random.Range(0f, 100f);

        if (random > 0f && random < arrowNeed)
        {
            GameObject arrowSpawn = Instantiate(arrows, transform.position, transform.rotation);
            arrowSpawn.transform.Rotate(-90f, 0f, 0f);
        }
        else if (random > arrowNeed && random < arrowNeed + grenadeNeed)
        {
            Instantiate(grenades, transform.position, transform.rotation);
        }
        else if (random > arrowNeed + grenadeNeed && random < arrowNeed + grenadeNeed + foodNeed)
        {
            Instantiate(food, transform.position, transform.rotation);
        } else
        {
            GameObject goldSpawn = Instantiate(gold, transform.position, transform.rotation);
            goldSpawn.transform.Rotate(-90f, 0f, 0f);
        }

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
