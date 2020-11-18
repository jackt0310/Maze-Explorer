using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    public GameObject player;
    Rigidbody rb;
    public float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("/knight");
        //rb = GetComponent<Rigidbody>();
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
            Destroy(gameObject);
        }
    }
}
