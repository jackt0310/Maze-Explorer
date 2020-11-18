using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    public GameObject player;
    Rigidbody rb;
    public float moveSpeed = 5f;
    public AudioSource ghostDeath;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("/knight");
        //rb = GetComponent<Rigidbody>();
        ghostDeath = GameObject.Find("Main Camera/GhostDeath").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            transform.LookAt(player.transform);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }

        

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "attack")
        {
            ghostDeath.Play();
            Destroy(gameObject);
        }
    }
}
