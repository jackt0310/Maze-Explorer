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

    public int maxNoteAmt;
    public int noteCount;
    public int maxNoteCount;
    public int maxBozuAmt;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = player.GetComponent<PlayerMovement>();
        //maxNoteCount = playerMove.unlockedMusic.Length;
        noteCount = 1;
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
        GameObject[] notesArray = GameObject.FindGameObjectsWithTag("note");

        int noteAmt = notesArray.Length - 1;
        if (!playerMove.unlockedAll && noteCount < maxNoteCount && noteAmt < maxNoteAmt)
        {
            float spawnX = Random.Range(minX, maxX);
            float spawnZ = Random.Range(minZ, maxZ);
            float spawnY = yCoord;
            Instantiate(note, new Vector3(spawnX, spawnY, spawnZ), Quaternion.identity);
            noteCount++;
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
        GameObject[] bozusArray = GameObject.FindGameObjectsWithTag("bozu");

        int bozuAmt = bozusArray.Length - 1;

        if(bozuAmt < maxBozuAmt)
        {
            float spawnX = Random.Range(minX, maxX);
            float spawnZ = Random.Range(minZ, maxZ);
            float spawnY = yCoord;
            Vector3 spawnLoc = new Vector3(spawnX, spawnY, spawnZ);
            if (player != null && Vector3.Distance(spawnLoc, player.transform.position) > 15f)
            {
                Instantiate(bozu, spawnLoc, Quaternion.identity);
            }
            else
            {
                if (player != null)
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
