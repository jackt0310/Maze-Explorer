using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public GameObject player;

    public GameObject gold50;
    public GameObject hymn;
    public GameObject gold100;
    public GameObject gold500;
    public GameObject arrowUp;
    public GameObject grenadeUp;
    public GameObject healthUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("/knight");
        gold50 = Resources.Load<GameObject>("Gold50");
        gold100 = Resources.Load<GameObject>("Gold100");
        gold500 = Resources.Load<GameObject>("Gold500");
        hymn = Resources.Load<GameObject>("note");
        healthUpgrade = Resources.Load<GameObject>("Grail");
        arrowUp = Resources.Load<GameObject>("ArrowUp");
        grenadeUp = Resources.Load<GameObject>("GrenadeUp");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 8f)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                float random = Random.Range(0f, 100f);

                if (random < 30f)
                {
                    GameObject goldSpawn = Instantiate(gold50, transform.position + new Vector3(0f, 1.5f, 0f), transform.rotation);
                    if (goldSpawn != null)
                    {
                        goldSpawn.transform.Rotate(-90f, 0f, 0f);
                    }
                }
                else if (random < 60f)
                {
                    Instantiate(hymn, transform.position + new Vector3(0f, 1.5f, 0f), transform.rotation);
                }
                else if (random < 80f)
                {
                    GameObject goldSpawn = Instantiate(gold100, transform.position + new Vector3(0f, 1.5f, 0f), transform.rotation);
                    if (goldSpawn != null)
                    {
                        goldSpawn.transform.Rotate(-90f, 0f, 0f);
                    }
                }
                else if (random < 87f)
                {
                    GameObject goldSpawn = Instantiate(gold500, transform.position + new Vector3(0f, 1.5f, 0f), transform.rotation);
                    if (goldSpawn != null)
                    {
                        goldSpawn.transform.Rotate(-90f, 0f, 0f);
                    }
                }
                else if (random < 92f)
                {
                    GameObject arrowSpawn = Instantiate(arrowUp, transform.position + new Vector3(0f, 5f, 0f), transform.rotation);
                    if (arrowSpawn != null)
                    {
                        arrowSpawn.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                    }
                }
                else if (random < 97f)
                {
                    Instantiate(grenadeUp, transform.position + new Vector3(0f, 1.5f, 0f), transform.rotation);
                    
                }
                else 
                {
                    Instantiate(healthUpgrade, transform.position + new Vector3(0f, 1.5f, 0f), transform.rotation);
                }
                Destroy(gameObject);
            }
        }
    }
}
