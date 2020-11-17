using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public AudioSource fanfare;
    public AudioSource fall;
    public AudioSource pain;
    public AudioSource walk;
    public AudioSource music_0;
    public AudioSource music_1;
    public AudioSource music_2;
    public AudioSource music_3;
    public AudioSource music_4;
    public AudioSource music_5;

    public bool isDead = false;

    public bool[] unlockedMusic;
    public AudioSource[] musicAudio;

    public string[] musicTitle;

    int currentSong = 0;
    public Text musicText;
    public Text musicInfoText;

    void Start()
    {
        GameObject.Find("/Main Camera").GetComponent<FollowPlayer>().player = gameObject;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        door = GameObject.Find("/Door");
        animator = GetComponent<Animator>();
        walk.Play();
        walk.Pause();
        unlockedMusic = new bool[] {
            true,
            false,
            false,
            false,
            false,
            false
        };
        musicAudio = new AudioSource[] {
            music_0,
            music_1,
            music_2,
            music_3,
            music_4,
            music_5
        };

        musicTitle = new string[]
        {
            "Dungeons and Dragons - Alexander Nakarada",
            "Thine ankles shan’t lie - Shakira",
            "Farewell farewell farewell - NSYNC",
            "Thine Watermelon Sugar - Harry Styles",
            "QUITE UNPLEASANT! - XXXTentacion",
            "Bringeth Holy Back - Justin Timberlake"
        };
        musicText = GameObject.Find("/Canvas/MusicText").GetComponent<Text>();
        musicText.text = "Now playing: " + musicTitle[currentSong];
        Invoke("MusicTextDone", 3f);

        musicInfoText = GameObject.Find("/Canvas/MusicInfoText").GetComponent<Text>();
        musicInfoText.enabled = false;
    }

    void changeSong(int song)
    {
        if (song == -1)
        {
            int nextSong = currentSong + 1;
            bool done = false;
            while (!done)
            {
                if (nextSong >= unlockedMusic.Length)
                {
                    nextSong = 0;
                }
                if (unlockedMusic[nextSong])
                {
                    done = true;
                    if (nextSong != currentSong)
                    {
                        musicAudio[currentSong].Stop();
                        currentSong = nextSong;
                        musicAudio[currentSong].Play();
                        musicText.text = "Now playing: " + musicTitle[currentSong];
                        musicText.enabled = true;
                        Invoke("MusicTextDone", 3f);
                    }
                }
                nextSong++;
            }
        } else
        {
            musicAudio[currentSong].Stop();
            currentSong = song;
            musicAudio[currentSong].Play();
            musicText.text = "Now playing: " + musicTitle[currentSong];
            musicText.enabled = true;
            musicInfoText.enabled = true;
            Invoke("MusicTextDone", 3f);
        }
    }

    void MusicTextDone()
    {
        musicText.enabled = false;
        musicInfoText.enabled = false;
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
            if(!walk.isPlaying)
            {
                walk.UnPause();
            }
            rb.MoveRotation(Quaternion.LookRotation(movement));
        } else
        {
            if(walk.isPlaying)
            {
                walk.Pause();
            }
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
            Die();
        }
        if(hasKey)
        {
            Destroy(door);
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            changeSong(-1);
        }
    }

    void Die()
    {
        if (walk.isPlaying)
        {
            walk.Pause();
        }
        rb.detectCollisions = false;

        minimap.SetActive(false);
        fall.Play();
        pain.Play();
        GameObject.Find("/Main Camera").GetComponent<FollowPlayer>().player = Instantiate(fallenKnight, transform.position, transform.rotation);
        isDead = true;
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name == "mazekey")
        {
            Destroy(collision.collider.transform.parent.gameObject);
            hasKey = true;
            fanfare.Play();
        }
        if (collision.collider.tag == "bozu")
        {
            Die();
        }
        if (collision.collider.tag == "note")
        {
            Destroy(collision.collider.gameObject);
            changeSong(unlockNext());
        }
    }

    int unlockNext()
    {
        int song = Random.Range(1, unlockedMusic.Length);
        if (!unlockedMusic[song])
        {
            unlockedMusic[song] = true;
            return song;
        }
        song = 0;
        while (song < unlockedMusic.Length)
        {
            if (!unlockedMusic[song])
            {
                unlockedMusic[song] = true;
                return song;
            }
            song++;
        }
        return -1;
    }
}
