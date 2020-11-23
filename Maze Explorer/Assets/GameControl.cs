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
    public GameObject arrows;
    public GameObject grenades;
    public GameObject grail;

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

    public float bozuSpeed = 6f;
    public float bozuSpeedMult = 1.2f;
    public float bozuSpawnMult = 1.2f;
    public bool title = false;


    // Start is called before the first frame update
    void Start()
    {
        if(!title)
        {
            switch(InventoryManagement.Difficulty)
            {
                case "Easy":
                    bozuSpeed = 4f;
                    bozuSpeedMult = 1.05f;
                    bozuSpawnMult = 1.05f;
                    break;
                case "Medium":
                    bozuSpeedMult = 1.1f;
                    bozuSpawnMult = 1.1f;
                    break;
                case "Hard":
                    bozuSpeedMult = 1.2f;
                    bozuSpawnMult = 1.2f;
                    break;
                case "Default":
                    bozuSpeed = 4f;
                    bozuSpeedMult = 1.05f;
                    bozuSpawnMult = 1.05f;
                    break;
            }
            bozuSpeed += bozuSpeed * ((bozuSpeedMult - 1) * (InventoryManagement.CurrentLevel - 1));
            spawnRateBozu -= spawnRateBozu * ((bozuSpawnMult - 1) * (InventoryManagement.CurrentLevel - 1));
            initSpawnBozu += (int) Mathf.Round(spawnRateBozu * ((bozuSpawnMult - 1) * (InventoryManagement.CurrentLevel - 1)));
            maxBozuAmt += (int) Mathf.Round(spawnRateBozu * ((bozuSpawnMult - 1) * (InventoryManagement.CurrentLevel - 1)));
        
        }
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
        SpawnArrows();
        SpawnGrenades();
        SpawnGrail();
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

            Ray ray = new Ray(new Vector3(spawnX, 100f, spawnZ), Vector3.down);
            RaycastHit hit;

            bool wallCollide = false;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag.Equals("wall"))
                {
                    wallCollide = true;
                    SpawnNote();
                }
            }
            
            if(!wallCollide)
            {
                float spawnY = yCoord;
                Instantiate(note, new Vector3(spawnX, spawnY, spawnZ), Quaternion.identity);
                noteCount++;
                noteAmt++;
            }
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


            Ray ray = new Ray(new Vector3(spawnX, 100f, spawnZ), Vector3.down);
            RaycastHit hit;

            bool wallCollide = false;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag.Equals("wall"))
                {
                    wallCollide = true;
                    SpawnBozu();
                }
            }

            Vector3 spawnLoc = new Vector3(spawnX, spawnY, spawnZ);
            if (!wallCollide && (player == null || Vector3.Distance(spawnLoc, player.transform.position) > 40f))
            {
                GameObject current = Instantiate(bozu, spawnLoc, Quaternion.identity);
                GhostScript ghost = current.GetComponent<GhostScript>();
                ghost.moveSpeed = bozuSpeed;

                bozuAmt++;
                if(bozuWander)
                {
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

    void SpawnArrows()
    {
        float spawnX = Random.Range(minX, maxX);
        float spawnZ = Random.Range(minZ, maxZ);
        float spawnY = yCoord;

        Ray ray = new Ray(new Vector3(spawnX, 100f, spawnZ), Vector3.down);
        RaycastHit hit;
        bool wallCollide = false;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag.Equals("wall"))
            {
                wallCollide = true;
                SpawnArrows();
            }
        }

        if (!wallCollide)
        {
            GameObject arrowSpawn = Instantiate(arrows, new Vector3(spawnX, spawnY + 5f, spawnZ), Quaternion.identity);
            arrowSpawn.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
    }


    void SpawnGrail()
    {
        float spawnX = Random.Range(minX, maxX);
        float spawnZ = Random.Range(minZ, maxZ);
        float spawnY = yCoord;

        Ray ray = new Ray(new Vector3(spawnX, 100f, spawnZ), Vector3.down);
        RaycastHit hit;
        bool wallCollide = false;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag.Equals("wall"))
            {
                wallCollide = true;
                SpawnGrail();
            }
        }

        if (!wallCollide)
        {
            Instantiate(grail, new Vector3(spawnX, spawnY + 3f, spawnZ), Quaternion.identity);
        }
    }

    void SpawnGrenades()
    {
        float spawnX = Random.Range(minX, maxX);
        float spawnZ = Random.Range(minZ, maxZ);
        float spawnY = yCoord;

        Ray ray = new Ray(new Vector3(spawnX, 100f, spawnZ), Vector3.down);
        RaycastHit hit;
        bool wallCollide = false;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag.Equals("wall"))
            {
                wallCollide = true;
                SpawnGrenades();
            }
        }

        if (!wallCollide)
        {
            Instantiate(grenades, new Vector3(spawnX, spawnY + 3f, spawnZ), Quaternion.identity);
        }
    }

    void SpawnKey()
    {
        float spawnX = Random.Range(minX, maxX);
        float spawnZ = Random.Range(minZ, maxZ);
        float spawnY = yCoord;

        Ray ray = new Ray(new Vector3(spawnX, 100f, spawnZ), Vector3.down);
        RaycastHit hit;
        bool wallCollide = false;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag.Equals("wall"))
            {
                wallCollide = true;
                SpawnKey();
            }
        }

        if (!wallCollide)
        {
            Instantiate(key, new Vector3(spawnX, spawnY, spawnZ), Quaternion.identity);
        }
    }
    
}
