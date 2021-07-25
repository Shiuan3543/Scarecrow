using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public static player playerinstane;
    public BoxCollider2D myBC;
    public CircleCollider2D myCC;
    public Rigidbody2D myRB;
    Animator myAnim;
    //錯誤
    //GameManager gameManager;

    //unbeatable 
    public Color normal;
    public Color transparent;
    public float unbeatableTime = 0;//無敵三秒
    SpriteRenderer mySR;  

    //move
    public float speed;
    public bool facingRight=true;
    bool isportal = false;

    //jump
    bool grounded = false;
    public LayerMask jumpable;
    public LayerMask wall;
    public Transform groundCheck;
    public float jumpHeight;

    //dead check
    public bool playerChange = false;//T=Red F=Blue
    public float TimetoChange = 0;

    //sound effects
    AudioSource myAS;
    public AudioClip[] SoundEffects;
    bool soundeffect = false; //有音效

    void Start()
    {
        playerinstane = this;
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myBC = GetComponent<BoxCollider2D>();
        myCC = GetComponent<CircleCollider2D>();
        //gameManager = FindObjectOfType<GameManager>();
        myAS = GetComponent<AudioSource>();
        mySR = GetComponent<SpriteRenderer>();

        //playerChange = !gameManager.StartAt;

    }

    void FixedUpdate()
    {
        portalTeleport();

        myAnim.SetBool("playerChange", playerChange);
        if (TimetoChange != 0)
        {
            //傳送中
            if (myBC.isTrigger == true || myCC.isTrigger == true)
            {
                myRB.velocity = new Vector2(0, 0);
                isportal = true;
            }
            //轉換中
            else
            {
                if (isportal)
                {
                    TimetoChange = 0;
                    isportal = false;
                    unbeatableTime = 3;
                }
                else
                {
                    myRB.velocity = new Vector2(0, myRB.velocity.y);

                    if (Time.time - TimetoChange > 2.5f)
                    {
                        TimetoChange = 0;
                        unbeatableTime = 3;
                    }
                }
            }
        }
        else
        {
            move();
            jump();
        }

        if (unbeatableTime <= 0)
        {
            mySR.color = normal;
            unbeatableTime = 0;
        }
        else
        {
            mySR.color = transparent;
            unbeatableTime -= Time.deltaTime;
        }
    }

    void move()
    {
        float run = 0;
        if (playerChange)
        {
            if (Input.GetKey(KeyCode.A))
            {
                run = -1;
            }
            else if (Input.GetKey(KeyCode.D)) {
                run = 1;
            }
        }
        else if (!playerChange)
        {
            if (Input.GetKey("left"))
            {
                run = -1;
            }
            else if (Input.GetKey("right"))
            {
                run = 1;
            }
        }

        myRB.velocity = new Vector2(run * speed, myRB.velocity.y);

        if (run != 0)
        {
            if ((run > 0 && !facingRight) || (run < 0 && facingRight))
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                facingRight = !facingRight;
            }
            myAnim.SetBool("run", true);
        }
        else
        {
            myAnim.SetBool("run", false);
        }

    }

    void jump()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, jumpable);
        if (playerChange)
        {
            if (grounded && Input.GetKeyDown(KeyCode.W))
            {
                grounded = false;
                myRB.AddForce(new Vector2(0, jumpHeight));
            }
        }
        else if (!playerChange)
        {
            if (grounded && Input.GetKeyDown("up"))
            {
                grounded = false;
                myRB.AddForce(new Vector2(0, jumpHeight));
            }
        }
        myAnim.SetFloat("VerticalSpeed",myRB.velocity.y);
        myAnim.SetBool("isGround", grounded);

        //jump and fall sound
        if (!grounded && !soundeffect)
        {
            soundmanager(0);
            soundeffect = true;
        }
        if (myAS.clip == SoundEffects[0] && grounded)
        {
            myAS.Stop();
            soundeffect = false;
        }
    }

    void portalTeleport()
    {
        if (myBC.isTrigger == true || myCC.isTrigger == true)
            TimetoChange = Time.time;
    }

    public void dead()
    {
        if (unbeatableTime <= 0)
        {
            if (TimetoChange == 0)
            {
                playerChange = !playerChange;
                TimetoChange = Time.time;
                soundmanager(1);
            }
        }
    }

    void soundmanager(int number)
    {
        myAS.clip = SoundEffects[number];
        myAS.Play();
    }
}
