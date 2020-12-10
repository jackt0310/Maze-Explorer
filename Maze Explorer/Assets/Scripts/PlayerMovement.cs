using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public AudioSource music_22;
    public AudioSource music_23;
    public AudioSource music_24;
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
    public int songsUnlocked;

    public bool rolling = false;
    public Animator bowAnimate;

    public SkinnedMeshRenderer bowMesh;
    public SkinnedMeshRenderer arrowMesh;
    public bool canFire = true;
    public bool drawn = false;

    public GameObject arrow;
    public GameObject grenade;

    public bool moveAttack = false;

    public float rollSpeed = 25f;
    public float moveAttackSpeed = 20f;

    public Text HymnsCollectedText;
    public Text KeyCollectedText;
    public Text levelText;

    public GameControl control;
    public bool gamePaused = false;
    public GameObject pauseMenu;
    public GameObject playerUI;

    public int arrowAmt;
    public int grenadeAmt;

    public Text arrowText;
    public Text grenadeText;

    public float health;
    public float maxHealth = 100f;
    public Text healthText;
    public bool dying = false;

    public int gold = 0;
    public Text goldText;

    public int maxArrows;
    public int maxGrenades;
    public bool walkSoundPause = false;

    public float arrowCooldown = 0f;
    public float grenadeCooldown = 0f;

    public float maxArrowCooldown = .8f;
    public float maxGrenadeCooldown = .8f;

    void Start()
    {
        pauseMenu.SetActive(false);
        //demon = GameObject.Find("/demon").GetComponent<DemonScript>();
        GameObject.Find("/Main Camera").GetComponent<FollowPlayer>().player = gameObject;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        door = GameObject.Find("/Door");
        animator = GetComponent<Animator>();
        walk.Play();
        walk.Pause();
        if(InventoryManagement.MaxHealth > 0)
        {
            maxHealth = InventoryManagement.MaxHealth;
        }
        health = maxHealth;
        gold = InventoryManagement.GoldAmt;

        maxArrows = InventoryManagement.MaxArrows;
        maxGrenades = InventoryManagement.MaxGrenades;

        if(InventoryManagement.UnlockedMusic == null)
        {
            int musicAmt = 25;
            bool[] unlockedMusic = new bool[musicAmt];
            unlockedMusic[0] = true;

            for (int i = 1; i < musicAmt; i++)
            {
                unlockedMusic[i] = false;
            }
        } else
        {
            unlockedMusic = (bool[]) InventoryManagement.UnlockedMusic.Clone();
        }
        
        songsUnlocked = InventoryManagement.SongsUnlocked;

        arrowAmt = InventoryManagement.ArrowAmt;
        grenadeAmt = InventoryManagement.GrenadeAmt;


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
            music_21,
            music_22,
            music_23,
            music_24
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
            "Ievan Polkka",
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
            "Striken By Thunder - AC/DC",
            "It Shall Be None But I - NSYNC",
            "The King's Regulators - Warren G & Nate Dogg",
            "Under Thy Spell - The Notorious B.I.G."
        };
        
        musicText.text = "Now playing: " + musicTitle[currentSong];
        switchTrackTime = musicAudio[currentSong].clip.length;
        musicTextDisableTime = 7f;
        
        musicInfoText.enabled = false;
        
        HymnsCollectedText.text = "Hymns: " + songsUnlocked + "/" + unlockedMusic.Length;
        
        KeyCollectedText.text = "Key: 0/1";
        levelText.text = "Level " + InventoryManagement.CurrentLevel;
        goldText = GameObject.Find("/Canvas/PlayerUI/GoldText").GetComponent<Text>();
    }
    
    public void Resume()
    {
        if(walkSoundPause)
        {
            walk.UnPause();
        }
        walkSoundPause = false;
        pauseMenu.SetActive(false);
        playerUI.SetActive(true);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void Pause()
    {
        if(walk.isPlaying)
        {
            walk.Pause();
            walkSoundPause = true;
        }
        pauseMenu.SetActive(true);
        //playerUI.SetActive(false);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void changeSong(int song)
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
            if (song >= unlockedMusic.Length)
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
        if (musicTextDisableTime <= 0f)
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
        if (!attacking || moveAttack)
        {
            float moveSpeed = 0;
            float forwardAmt = 1.0f;
            float moveVertical = 0.0f;
            float moveHorizontal = 0.0f;
            if (!rolling && !moveAttack)
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
            } else if (rolling)
            {
                moveSpeed = rollSpeed;
                moveVertical = 1.0f;

            } else
            {
                moveSpeed = moveAttackSpeed;
                moveVertical = 1.0f;
            }


            if (Input.GetKey(KeyCode.E) && !drawn && !rolling)
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

            if (rolling || moveAttack)
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


                /* https://wiki.unity3d.com/index.php/RigidbodyFPSWalker */
                // Calculate how fast we should be moving
                var targetVelocity = transform.forward * moveSpeed;

                // Apply a force that attempts to reach our target velocity
                var velocity = rb.velocity;
                var velocityChange = (targetVelocity - velocity);
                var maxVelocityChange = 100000f;
                var maxYVelocity = 25f;
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                if (velocity.y > maxYVelocity)
                {
                    velocityChange.y = (maxYVelocity - velocity.y);
                    velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);
                }
                else
                {
                    velocityChange.y = 0;
                }
                rb.AddForce(velocityChange, ForceMode.VelocityChange);
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
            //rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
        }

        canFire = true;

    }
    
    void BowGone()
    {
        bowMesh.enabled = false;
        canFire = true;
    }

    void FireArrow()
    {
        arrowMesh.enabled = false;
        Invoke("BowGone", .5f);
        GameObject projectile = Instantiate(arrow, transform.position + transform.forward * 5f, transform.rotation * Quaternion.Euler(0, 90, 0));
        projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 5000f);
    }

    void RollForce()
    {
        //rb.AddForce(transform.forward * 1000f);
    }
    void StopRolling()
    {
        rolling = false;
    }

    void StopMoveAttack()
    {
        moveAttack = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(arrowCooldown > 0f)
        {
            arrowCooldown -= Time.deltaTime;
        }
        if(grenadeCooldown > 0f)
        {
            grenadeCooldown -= Time.deltaTime;

        }
        if(health <= 0 && !dying)
        {
            dying = true;
            Die();
        }

        string remainingArrows = "/??";
        string remainingGrenades = "/??";

        switch (InventoryManagement.Difficulty) {
            case "Easy":
                remainingArrows = "/999";
                remainingGrenades = "/999";
                break;
            case "Medium":
                remainingArrows = "/" + maxArrows * 2;
                remainingGrenades = "/" + maxGrenades * 2;
                break;
            case "Hard":
                remainingArrows = "/" + maxArrows;
                remainingGrenades = "/" + maxGrenades;
                break;
            default:
                break;
        }


        arrowText.text = "Arrows: " + arrowAmt + remainingArrows;
        grenadeText.text = "Grenades: " + grenadeAmt + remainingGrenades;
        goldText.text = "Gold: " + gold;
        if (health < 0)
        {
            health = 0;
        }
        healthText.text = "Health: " + health + "/" + maxHealth;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gamePaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }

        if(!gamePaused)
        {
            if (Input.GetKeyDown(KeyCode.F) && grenadeAmt > 0 && grenadeCooldown <= 0f && !rolling)
            {
                GameObject projectile = Instantiate(grenade, transform.position + new Vector3(0f, 3f, 0f) + transform.forward * 5f, transform.rotation);
                projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 500f);
                if (!InventoryManagement.Difficulty.Equals("Easy"))
                {
                    grenadeAmt--;
                }
                grenadeCooldown = maxGrenadeCooldown;
            }
            if (Input.GetKey(KeyCode.Mouse0) && !rolling)
            {
                if (!attacking)
                {
                    animator.SetTrigger("Attack");
                    swordHit.enabled = true;
                    swordNoise.Play();

                    attacking = true;
                    if (animator.GetBool("isWalking"))
                    {
                        sword.GetComponent<Renderer>().enabled = true;
                        sword.GetComponent<Animator>().SetTrigger("MoveAttack");
                        moveAttack = true;
                        Invoke("StopMoveAttack", 0.85f);
                        Invoke("SwordDone", 0.85f);
                    }
                    else
                    {
                        sword.GetComponent<Renderer>().enabled = true;
                        sword.GetComponent<Animator>().SetTrigger("Attack");
                        Invoke("SwordDone", 0.6f);
                    }
                }

            }


            if (Input.GetKeyDown(KeyCode.Mouse1) && canFire && arrowAmt > 0 && arrowCooldown <= 0f && !rolling)
            {
                canFire = false;
                bowAnimate.SetTrigger("Draw");
                bowMesh.enabled = true;
                arrowMesh.enabled = true;
                drawn = true;
            }

            if (Input.GetKeyUp(KeyCode.Mouse1) && drawn)
            {
                bowAnimate.SetTrigger("Fire");
                Invoke("FireArrow", .2f);
                drawn = false;
                if(!InventoryManagement.Difficulty.Equals("Easy"))
                {
                    arrowAmt--;
                }
                arrowCooldown = maxArrowCooldown;
            }

            if (musicTextDisableTime > 0f)
            {
                musicTextDisableTime -= Time.deltaTime;
            }
            else
            {
                MusicTextDone();
            }

            if (switchTrackTime > 0f)
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
            if (hasKey)
            {
                Destroy(door);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                changeSong(-1);
            }
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
            KeyCollectedText.text = "Key: 1/1";
            fanfare.Play();
        }
        if (collision.collider.tag == "bozu")
        {
            health -= 50f;
            if(health > 0)
            {
                Destroy(collision.collider.gameObject);
            }
        }
        if (collision.collider.tag == "note")
        {
            Destroy(collision.collider.gameObject);
            control.noteAmt--;
            changeSong(unlockNext());
            checkIfDoneMusic();
        }

        if (collision.collider.tag == "arrows")
        {
            Destroy(collision.collider.gameObject);

            switch (InventoryManagement.Difficulty) {
                case "Medium":
                    arrowAmt += 15;
                    if(arrowAmt > maxArrows * 2)
                    {
                        arrowAmt = maxArrows * 2;
                    }
                    break;
                case "Hard":
                    arrowAmt += 15;
                    if (arrowAmt > maxArrows)
                    {
                        arrowAmt = maxArrows;
                    }
                    break;
                default:
                    break;
            }

            
        }

        if(collision.collider.tag == "grenades")
        {
            Destroy(collision.collider.gameObject);

            switch (InventoryManagement.Difficulty)
            {
                case "Medium":
                    grenadeAmt += 3;
                    if (grenadeAmt > maxGrenades * 2)
                    {
                        grenadeAmt = maxGrenades * 2;
                    }
                    break;
                case "Hard":
                    grenadeAmt += 3;
                    if (grenadeAmt > maxGrenades)
                    {
                        grenadeAmt = maxGrenades;
                    }
                    break;
                default:
                    break;
            }
        }

        if (collision.collider.tag == "grail")
        {
            Destroy(collision.collider.gameObject);
            maxHealth += 50;
            health = maxHealth;
        }

        if (collision.collider.tag == "food")
        {
            Destroy(collision.collider.gameObject);
            health = maxHealth;
        }

        if(collision.collider.tag == "gold")
        {
            
            gold += collision.collider.gameObject.GetComponent<GoldScript>().value;
            Destroy(collision.collider.gameObject);
        }

        if(collision.collider.tag == "arrowUp")
        {
            Destroy(collision.collider.gameObject);
            maxArrows += 15;

        }

        if(collision.collider.tag == "grenadeUp")
        {
            Destroy(collision.collider.gameObject);
            maxGrenades += 3;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "demonZone")
        {
            //demon.pursuit = true;
        } else if(other.tag == "end")
        {
            if(InventoryManagement.CurrentLevel < 4)
            {
                InventoryManagement.CurrentLevel = InventoryManagement.CurrentLevel + 1;
                InventoryManagement.UnlockedMusic = unlockedMusic;
                InventoryManagement.SongsUnlocked = songsUnlocked;
                InventoryManagement.ArrowAmt = arrowAmt;
                InventoryManagement.GrenadeAmt = grenadeAmt;
                InventoryManagement.MaxHealth = maxHealth;
                InventoryManagement.MaxArrows = maxArrows;
                InventoryManagement.MaxGrenades = maxGrenades;
                gold += 500;
                InventoryManagement.GoldAmt = gold;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            } else
            {
                gold += 1000;
                InventoryManagement.GoldAmt = gold;
                InventoryManagement.UnlockedMusic = unlockedMusic;
                InventoryManagement.SongsUnlocked = songsUnlocked;
                InventoryManagement.CurrentLevel = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        } else if(other.tag == "enemyAttack")
        {
            health -= 50f;
            Debug.Log("Ouch");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "demonZone")
        {
            //demon.pursuit = false;
        }
    }

    public void checkIfDoneMusic()
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
            HymnsCollectedText.text = "Hymns: " + songsUnlocked + "/" + unlockedMusic.Length;
            return song;
        }
        song = 0;
        while (song < unlockedMusic.Length)
        {
            if (!unlockedMusic[song])
            {
                unlockedMusic[song] = true;
                songsUnlocked++;
                HymnsCollectedText.text = "Hymns: " + songsUnlocked + "/" + unlockedMusic.Length;
                return song;
            }
            song++;
        }
        return -1;
    }


    public int unlockNextSave()
    {
        int song = Random.Range(1, unlockedMusic.Length);
        if (!unlockedMusic[song])
        {
            unlockedMusic[song] = true;
            songsUnlocked++;
            HymnsCollectedText.text = "Hymns: " + songsUnlocked + "/" + unlockedMusic.Length;
            InventoryManagement.UnlockedMusic = unlockedMusic;
            InventoryManagement.SongsUnlocked = songsUnlocked;
            return song;
        }
        song = 0;
        while (song < unlockedMusic.Length)
        {
            if (!unlockedMusic[song])
            {
                unlockedMusic[song] = true;
                songsUnlocked++;
                HymnsCollectedText.text = "Hymns: " + songsUnlocked + "/" + unlockedMusic.Length;
                InventoryManagement.UnlockedMusic = unlockedMusic;
                InventoryManagement.SongsUnlocked = songsUnlocked;
                return song;
            }
            song++;
        }
        return -1;
    }
}
