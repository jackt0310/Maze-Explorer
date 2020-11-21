using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float yCoord;

    public GameObject note;
    public GameObject bozu;
    public GameObject key;
    public GameObject player;
    PlayerMovement playerMove;

    public float spawnRateNote;
    public float spawnRateBozu;
    public int initSpawnNote;
    public int initSpawnBozu;

    public int maxNoteAmt;
    public int noteCount;
    public int maxNoteCount;
    public int maxBozuAmt;
    public bool bozuWander = false;

    public int bozuAmt;
    public int noteAmt;

    // Start is called before the first frame update
    void Start()
    {
        noteAmt = 0;
        bozuAmt = 0;

        if(player != null)
        {
            playerMove = player.GetComponent<PlayerMovement>();
        }
        
        noteCount = 1;
        
        minX = GetComponent<Collider>().bounds.min.x;
        maxX = GetComponent<Collider>().bounds.max.x;
        minZ = GetComponent<Collider>().bounds.min.z;
        maxZ = GetComponent<Collider>().bounds.max.z;
        yCoord = GetComponent<Collider>().bounds.max.y + 2;
        
        initSpawn();
    }

    void initSpawn()
    {
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
        if (noteCount < maxNoteCount && noteAmt < maxNoteAmt)
        {
            float spawnX = Random.Range(minX, maxX);
            float spawnZ = Random.Range(minZ, maxZ);
            float spawnY = yCoord;
            Instantiate(note, new Vector3(spawnX, spawnY, spawnZ), Quaternion.identity);
            noteCount++;
            noteAmt++;
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

        if(bozuAmt < maxBozuAmt)
        {
            float spawnX = Random.Range(minX, maxX);
            float spawnZ = Random.Range(minZ, maxZ);
            float spawnY = yCoord;
            Vector3 spawnLoc = new Vector3(spawnX, spawnY, spawnZ);
            if (player == null || Vector3.Distance(spawnLoc, player.transform.position) > 40f)
            {
                GameObject current = Instantiate(bozu, spawnLoc, Quaternion.identity);
                bozuAmt++;
                if(bozuWander)
                {
                    GhostScript ghost = current.GetComponent<GhostScript>();
                    ghost.wander = true;
                    ghost.Pause();
                }
            }
            else
            {
               // if (player != null)
                {
                    SpawnBozu();
                }
            }
        }
    }

    void SpawnKey()
    {
        float spawnX = Random.Range(minX, maxX);
        float spawnZ = Random.Range(minZ, maxZ);
        float spawnY = yCoord;
        Instantiate(key, new Vector3(spawnX, spawnY, spawnZ), Quaternion.identity);
    }
    
}
