using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    float minX;
    float maxX;
    float minZ;
    float maxZ;
    float yCoord;

    public GameObject note;
    public GameObject bozu;
    public GameObject key;
    public GameObject player;
    PlayerMovement playerMove;

    public float spawnRateNote;
    public float spawnRateBozu;
    public int initSpawnNote;
    public int initSpawnBozu;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = player.GetComponent<PlayerMovement>();
        minX = GetComponent<Collider>().bounds.min.x;
        maxX = GetComponent<Collider>().bounds.max.x;
        minZ = GetComponent<Collider>().bounds.min.z;
        maxZ = GetComponent<Collider>().bounds.max.z;
        yCoord = GetComponent<Collider>().bounds.max.y + 2;

        SpawnNoteAmt(initSpawnNote);
        SpawnBozuAmt(initSpawnBozu);
        SpawnKey();
        InvokeRepeating("SpawnNote", spawnRateNote, spawnRateNote);
        InvokeRepeating("SpawnBozu", spawnRateBozu, spawnRateBozu);
    }

    void SpawnNoteAmt(int amt)
    {
        for(int i = 0; i < amt; i++)
        {
            SpawnNote();
        }
    }

    void SpawnNote()
    {
        if(!playerMove.unlockedAll)
        {
            float spawnX = Random.Range(minX, maxX);
            float spawnZ = Random.Range(minZ, maxZ);
            float spawnY = yCoord;
            Instantiate(note, new Vector3(spawnX, spawnY, spawnZ), Quaternion.identity);
        }
    }


    void SpawnBozuAmt(int amt)
    {
        for (int i = 0; i < amt; i++)
        {
            SpawnBozu();
        }
    }

    void SpawnBozu()
    {
        float spawnX = Random.Range(minX, maxX);
        float spawnZ = Random.Range(minZ, maxZ);
        float spawnY = yCoord;
        Vector3 spawnLoc = new Vector3(spawnX, spawnY, spawnZ);
        if(Vector3.Distance(spawnLoc, player.transform.position) > 15f)
        {
            Instantiate(bozu, spawnLoc, Quaternion.identity);
        } else
        {
            SpawnBozu();
        }
        
    }

    void SpawnKey()
    {
        float spawnX = Random.Range(minX, maxX);
        float spawnZ = Random.Range(minZ, maxZ);
        float spawnY = yCoord;
        Instantiate(key, new Vector3(spawnX, spawnY, spawnZ), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
