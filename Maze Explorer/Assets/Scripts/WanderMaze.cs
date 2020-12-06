using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderMaze : MonoBehaviour
{
    public float moveSpeed = 10f;
    public GameControl plane;
    public float yCoord = 0f;
    public Vector3 nextPoint;

    public bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        plane = GameObject.Find("/Plane").GetComponent<GameControl>();
        Go();
    }

    void Go()
    {
        float spawnX = Random.Range(plane.minX, plane.maxX);
        float spawnZ = Random.Range(plane.minZ, plane.maxZ);
        nextPoint = new Vector3(spawnX, yCoord, spawnZ);

        moving = true;
    }

    void Pause()
    {
        Go();
        //Invoke("Go", Random.Range(4f, 6f));
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, nextPoint) < 5f)
        {
            moving = false;
            Pause();
        }
        if(moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPoint, moveSpeed * Time.deltaTime);
        }
    }
}
