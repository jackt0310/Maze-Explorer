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
    public AudioSource music_6;
    public AudioSource music_7;
    public AudioSource music_8;
    public AudioSource music_9;
    public AudioSource music_10;
    public AudioSource music_11;
    public AudioSource music_12;
    public AudioSource music_13;
    public AudioSource music_14;
    public AudioSource music_15;
    public AudioSource music_16;
    public AudioSource music_17;
    public AudioSource music_18;
    public AudioSource music_19;
    public AudioSource music_20;
    public AudioSource music_21;
    public BoxCollider swordHit;

    public bool isDead = false;

    public bool[] unlockedMusic;
    public AudioSource[] musicAudio;
    public DemonScript demon;
    public string[] musicTitle;

    int currentSong = 0;
    public Text musicText;
    public Text musicInfoText;
    public Text hymnsUnlockedText;

    float musicTextDisableTime = 0f;
    float switchTrackTime = 100000f;

    public GameObject sword;
    public AudioSource swordNoise;
    public bool attacking = false;
    public bool unlockedAll = false;
    int songsUnlocked = 1;

    public bool rolling = false;

    void Start()
    {
        demon = GameObject.Find("/demon").GetComponent<DemonScript>();
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
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
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
            music_5,
            music_6,
            music_7,
            music_8,
            music_9,
            music_10,
            music_11,
            music_12,
            music_13,
            music_14,
            music_15,
            music_16,
            music_17,
            music_18,
            music_19,
            music_20,
            music_21
        };

        musicTitle = new string[]
        {
            "Dungeons and Dragons - Alexander Nakarada",
            "Thine ankles shan’t lie - Shakira",
            "Farewell farewell farewell - NSYNC",
            "Thine Watermelon Sugar - Harry Styles",
            "QUITE UNPLEASANT! - XXXTentacion",
            "Bringeth Holy Back - Justin Timberlake",
            "Buskin Boots - Foster the People",
            "Ievan Polka",
            "Thou Art Somebody Whom I Used to Know - Gotye",
            "Stench of Youthful Soul - Nirvana",
            "Tis No Sunshine - Bill Withers",
            "Wheat and Potatoes (WAP) - Cardi B feat Megan Thee Stallion",
            "Thine Affection Towards California - 2Pac feat Dr. Dre and Roger Troutman",
            "Absurd Occurrences - Kyle Dixon & Michael Stein",
            "Bury Thine Friend - Billie Eilish",
            "Bound II - Kanye West",
            "Where Art Thine Kingdom - DMX",
            "I Hath Five Upon Thee - Luniz",
            "No Peasants - TLC",
            "Because I Smoketh The Bud - Afroman",
            "Whatever Thou Sayeth I Am - Eminem",
            "Striken By Thunder - AC/DC"
        };
        musicText = GameObject.Find("/Canvas/MusicText").GetComponent<Text>();
        musicText.text = "Now playing: " + musicTitle[currentSong];
        switchTrackTime = musicAudio[currentSong].clip.length;
        musicTextDisableTime = 7f;

        musicInfoText = GameObject.Find("/Canvas/MusicInfoText").GetComponent<Text>();
        musicInfoText.enabled = false;

        hymnsUnlockedText = GameObject.Find("/Canvas/HymnsUnlockedText").GetComponent<Text>();
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
                        switchTrackTime = musicAudio[currentSong].clip.length;
                        musicText.text = "Now playing: " + musicTitle[currentSong];
                        musicText.enabled = true;
                        musicTextDisableTime = 7f;
                    }
                }
                nextSong++;
            }
        } else
        {
            if(song >= unlockedMusic.Length)
            {
                song = 0;
            }
            musicAudio[currentSong].Stop();
            currentSong = song;
            musicAudio[currentSong].Play();
            switchTrackTime = musicAudio[currentSong].clip.length;
            musicText.text = "Now playing: " + musicTitle[currentSong];
            musicText.enabled = true;
            musicInfoText.enabled = true;
            musicTextDisableTime = 7f;
        }
    }
    void PlayNextTrack()
    {
        changeSong(-1);
    }

    void MusicTextDone()
    {
        if(musicTextDisableTime <= 0f)
        {
            musicText.enabled = false;
            musicInfoText.enabled = false;
            hymnsUnlockedText.enabled = false;
        }
       
    }

    void SwordDone()
    {
        attacking = false;
        swordHit.enabled = false;
        sword.GetComponent<Renderer>().enabled = false;
    }
    private void FixedUpdate()
    {
        if(!attacking)
        {
            float moveSpeed = 0;
            float forwardAmt = 1.0f;
            float moveVertical = 0.0f;
            float moveHorizontal = 0.0f;
            if (!rolling)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    moveSpeed = runSpeed;
                    moveVertical = 1.0f;
                }

                if (Input.GetKey(KeyCode.S))
                {
                    moveSpeed = runSpeed;
                    moveVertical = -1.0f;
                }

               
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
            } else
            {
                moveSpeed = runSpeed * 2.5f;
                moveVertical = 1.0f;

            }
            
            
            if(Input.GetKey(KeyCode.E))
            {
                if (!rolling)
                {
                    Invoke("RollForce", .2f);
                    
                    animator.SetTrigger("Roll");
                    rolling = true;
                    Invoke("StopRolling", 1.2f);
                }
            }


            float cameraRotY = GameObject.Find("/Main Camera").transform.localRotation.eulerAngles.y;

            Vector3 movement = Quaternion.AngleAxis(cameraRotY, Vector3.up) * new Vector3(moveHorizontal, 0.0f, moveVertical);

            if(rolling)
            {
                movement = transform.forward;
            }
            if (moveSpeed > 0)
            {
                animator.SetBool("isWalking", true);
                if (!walk.isPlaying)
                {
                    walk.UnPause();
                }
                rb.MoveRotation(Quaternion.LookRotation(movement));
            }
            else
            {
                if (walk.isPlaying)
                {
                    walk.Pause();
                }
                animator.SetBool("isWalking", false);
            }

            //rb.AddForce(movement * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
            rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
        }
        

    }

    void RollForce()
    {
        //rb.AddForce(transform.forward * 1000f);
    }
    void StopRolling()
    {
        rolling = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(!attacking)
            {
                sword.GetComponent<Renderer>().enabled = true;
                sword.GetComponent<Animator>().SetTrigger("Attack");
                animator.SetTrigger("Attack");
                swordHit.enabled = true;
                swordNoise.Play();
                Invoke("SwordDone", 0.6f);
                attacking = true;
            }
            
        }

        if (musicTextDisableTime > 0f)
        {
            musicTextDisableTime -= Time.deltaTime;
        } else
        {
            MusicTextDone();
        }

        if (switchTrackTime> 0f)
        {
            switchTrackTime -= Time.deltaTime;
        }
        else
        {
            PlayNextTrack();
        }

        if (Input.GetKeyDown(KeyCode.G))
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
            checkIfDoneMusic();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "demonZone")
        {
            demon.pursuit = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "demonZone")
        {
            demon.pursuit = false;
        }
    }

    void checkIfDoneMusic()
    {
        if(songsUnlocked >= unlockedMusic.Length)
        {
            hymnsUnlockedText.enabled = true;
            var clones = GameObject.FindGameObjectsWithTag("note");
            foreach (var clone in clones)
            {
                Destroy(clone);
            }
            unlockedAll = true;
        }
        
    }

    int unlockNext()
    {
        int song = Random.Range(1, unlockedMusic.Length);
        if (!unlockedMusic[song])
        {
            unlockedMusic[song] = true;
            songsUnlocked++;
            return song;
        }
        song = 0;
        while (song < unlockedMusic.Length)
        {
            if (!unlockedMusic[song])
            {
                unlockedMusic[song] = true;
                songsUnlocked++;
                return song;
            }
            song++;
        }
        return -1;
    }
}
