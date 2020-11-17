using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Components
    private Animator animator;
    Rigidbody rb;
    
    // Stats
    public float moveSpeed = 100f;
    public float walkSpeed = 10f;
    public float runSpeed = 20f;
    public float turnSpeed = 2f;
    public int jumps = 1;
    public float jumpForce = 10f;

    public GameObject door;
    public GameObject fallenKnight;
    public GameObject minimap;

    public bool hasKey = false;
    
    void Start()
    {
        GameObject.Find("/Main Camera").GetComponent<FollowPlayer>().player = gameObject;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        door = GameObject.Find("/Door");
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float moveSpeed = 0;
        float forwardAmt = 1.0f;
        float moveVertical = 0.0f;
        if (Input.GetKey(KeyCode.W))
        {
            moveSpeed = runSpeed;
            moveVertical = 1.0f;
        }

        if(Input.GetKey(KeyCode.S))
        {
            moveSpeed = runSpeed;
            moveVertical = -1.0f;
        }

        float moveHorizontal = 0.0f;
        if (Input.GetKey(KeyCode.D))
        {
            moveSpeed = runSpeed;
            moveHorizontal = 1.0f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveSpeed = runSpeed;
            moveHorizontal = -1.0f;
        }
        float cameraRotY = GameObject.Find("/Main Camera").transform.localRotation.eulerAngles.y;

        Vector3 movement = Quaternion.AngleAxis(cameraRotY, Vector3.up) * new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (moveSpeed > 0)
        {
            animator.SetBool("isWalking", true);
            rb.MoveRotation(Quaternion.LookRotation(movement));
        } else
        {
            animator.SetBool("isWalking", false);
        }

        //rb.AddForce(movement * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
        rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);

    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            rb.detectCollisions = false;
            
            minimap.SetActive(false);
            GameObject.Find("/Main Camera").GetComponent<FollowPlayer>().player = Instantiate(fallenKnight, transform.position, transform.rotation);
            Destroy(gameObject);

        }
        if(hasKey)
        {
            Destroy(door);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name == "mazekey")
        {
            Destroy(collision.collider.transform.parent.gameObject);
            hasKey = true;
        }
    }
}
