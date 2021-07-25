using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{
    public GameObject particle;
    public GameObject portalLink;
    public LayerMask playerL;
    public Transform playerT;

    AudioSource myAS;

    float cooldown = 3;
    float timecheck = 0;
    bool changeP = true;
    bool touchPlayer = false;
    bool teleport = false;

    //使用中
    public int attractNUM;
    bool AttractMode = false;
    float ThreeSecondsCheck = 0;
    float vx;
    float vy;
    Vector3 savePortal;
    Vector3 savePortalLink;

    public left myleftA , myleftB;

    void Start()
    {
        myAS = GetComponent<AudioSource>();
        savePortal=transform.position;
        savePortalLink=portalLink.transform.position;
    }

    void FixedUpdate()
    {
        PositionCheck();
        Play();
        TimeCheck();
        Lunch();
    }

    void PositionCheck()
    {
        float Dx = transform.position.x - playerT.position.x;
        float Dy = transform.position.y - playerT.position.y;
        float distance = Mathf.Sqrt(Dx * Dx + Dy * Dy);

        vx = Dx / distance * attractNUM;
        vy = Dy / distance * attractNUM;
    }

    void Play()
    {
        Vector2 touchRange = transform.position + new Vector3(-0.2f, -0.3f);
        touchPlayer = Physics2D.OverlapCircle(touchRange, 0.7f, playerL);

        if (player.playerinstane.unbeatableTime <= 0 && cooldown >= 3)
        {
            if (player.playerinstane.playerChange)
            {
                if (Input.GetKey("left"))
                {
                    particle.SetActive(true);
                    AttractMode = true;
                }
            }
            else if (!player.playerinstane.playerChange)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    particle.SetActive(true);
                    AttractMode = true;
                }
            }
        }
        else
        {
            particle.SetActive(false);
            cooldown += Time.deltaTime;
        }

        if (AttractMode)
        {
            player.playerinstane.myRB.velocity += new Vector2(vx, vy);

            if(touchPlayer && !teleport)
            {
                timecheck = Time.time;
                teleport = true;
                myAS.Play();

                changeP = false;

                if (!myleftA.istouch)
                {
                    myleftA.portaling();
                    myleftB.portaling();
                }
            }

            if (ThreeSecondsCheck < 3)
            {
                ThreeSecondsCheck += Time.deltaTime;
            }
        }

        if (ThreeSecondsCheck >= 3)
        {
            if (teleport)
            {
                particle.SetActive(true);
            }
            else
            {
                particle.SetActive(false);
                ThreeSecondsCheck = 0;
                cooldown = 0;
            }
            player.playerinstane.myRB.velocity -= new Vector2(vx, vy);
            AttractMode = false;
        }
    }

    //time continue check
    void TimeCheck()
    {
        if (Time.time - timecheck > 5f && timecheck != 0)
        {
            teleport = false;
            timecheck = 0;
            if (myleftA.istouch)
            {
                myleftA.portaling();
                myleftB.portaling();
            }

        }
    } 

    void Lunch()
    { 
        if (teleport == true)
        {
            player.playerinstane.myBC.isTrigger = true;
            player.playerinstane.myCC.isTrigger = true;
            transform.position = Vector3.Lerp(transform.position, portalLink.transform.position, Time.deltaTime);
            player.playerinstane.transform.position = transform.position;
        }
        else
        {
            player.playerinstane.myBC.isTrigger = false;
            player.playerinstane.myCC.isTrigger = false;

            if (!changeP)
            {
                transform.position = savePortalLink;
                portalLink.transform.position = savePortal;
                Vector3 changevector = savePortal;
                savePortal = savePortalLink;
                savePortalLink = changevector;

                changeP = true;
            }

            //change place
            if (player.playerinstane.transform.position.y > (portalLink.transform.position.y + transform.position.y) / 2)
            {
                if (player.playerinstane.transform.position.y > transform.position.y && player.playerinstane.transform.position.y < portalLink.transform.position.y)
                {
                    transform.position = savePortalLink;
                    portalLink.transform.position = savePortal;
                    Vector3 changevector = savePortal;
                    savePortal = savePortalLink;
                    savePortalLink = changevector;
                }
            }
            else
            {
                if (player.playerinstane.transform.position.y < transform.position.y && player.playerinstane.transform.position.y > portalLink.transform.position.y)
                {
                    transform.position = savePortalLink;
                    portalLink.transform.position = savePortal;
                    Vector3 changevector = savePortal;
                    savePortal = savePortalLink;
                    savePortalLink = changevector;
                }
            }
        }
    }

}
