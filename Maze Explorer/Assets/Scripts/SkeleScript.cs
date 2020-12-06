using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeleScript : MonoBehaviour
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
    public bool attacking = false;
    public GameObject sword;
    public Animator anim;
    public Collider swordBox;
    public bool waiting = false;

    public float maxHealth = 150f;
    public float health;
    public bool stun = false;
    public float stunTime = 5f;
    public Vector3 stunDir;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        anim = sword.GetComponent<Animator>();
        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
        }

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
        }

        rb = GetComponent<Rigidbody>();
    }
    public void Go()
    {
        if (!moving)
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

    void StartAttack()
    {
        if(attacking)
        {
            animator.SetTrigger("Attack");
            Invoke("StopAttack", 1.2f);
            anim.SetTrigger("Attack");
            Invoke("StartHitBox", .5f);
        }
        
    }
    void StartHitBox()
    {
        swordBox.enabled = true;
    }
    void StopAttack()
    {
        attacking = false;
        waiting = true;
        Invoke("StopWaiting", 1f);
        swordBox.enabled = false;
    }

    void StopWaiting()
    {
        waiting = false;
    }

    private void FixedUpdate()
    {
        if(moving)
        {
            //rb.MoveRotation(Quaternion.LookRotation(player.transform.position));
            transform.LookAt(player.transform.position);

            /* https://wiki.unity3d.com/index.php/RigidbodyFPSWalker */
            // Calculate how fast we should be moving
            var targetVelocity = transform.forward * moveSpeed;

            // Apply a force that attempts to reach our target velocity
            var velocity = rb.velocity;
            var velocityChange = (targetVelocity - velocity);
            var maxVelocityChange = 100000f;
            var maxYVelocity = 25f;
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            if (velocity.y > maxYVelocity)
            {
                velocityChange.y = (maxYVelocity - velocity.y);
                velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);
            }
            else
            {
                velocityChange.y = 0;
            }
            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(stun)
        {
            //rb.AddForce(stunDir * 1000f * Time.deltaTime);
            swordBox.enabled = false;
        } else if(attacking && !waiting)
        {
            
        } else
        {
            swordBox.enabled = false;
            if (player != null && Vector3.Distance(transform.position, player.transform.position) < 5f && !waiting)
            {
                moving = false;
                attacking = true;
                Invoke("StartAttack", .5f);
                
            }
            else if (player != null && !wander)
            {
                moving = true;
                //Vector3 movePoint = player.transform.position + player.transform.forward * 2f;
                //transform.LookAt(player.transform.position);
                //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                //moving = true;
            }
            else if (player == null)
            {
                moving = false;
            }
        }
        

        if (animator != null)
        {
            animator.SetBool("isWalking", moving);
        }

        if (wander)
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
        control.bozuAmt--;
        ghostDeath.Play();
        Destroy(gameObject);
    }

    void StopStun()
    {
        stun = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "attack" && !stun)
        {
            stunDir = other.transform.parent.transform.forward;
            rb.AddForce(stunDir * 1000f);
            StopAttack();
            health -= 50f;
            stun = true;
            Invoke("StopStun", 3f);
            
            moving = false;
            if(health <= 0f)
            {
                Die();
            }
        }
    }
}
