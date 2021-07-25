using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMaker_UP : MonoBehaviour
{
    public GameObject box;
    public Transform muzzle;
    float reload;
    int boxcount = 0;
    float cooldown;

    AudioSource myAS;

    void Awake()
    {
        myAS = GetComponent<AudioSource>();
        reload = -3;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.playerinstane.playerChange)
        {
            if (Input.GetKeyDown("up"))
                trapStart();
        }
        else if (!player.playerinstane.playerChange)
        {
            if (Input.GetKeyDown(KeyCode.W))
                trapStart();
        }

        if (Time.time - cooldown > 3)
        {
            boxcount = 0;
        }
    }

    void trapStart()
    {
        if (Time.time - reload > 3)
        {
            if (boxcount < 20)
            {
                Instantiate(box, muzzle.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                boxcount++;
                cooldown = Time.time;
                myAS.Play();

                if (boxcount == 20)
                {
                    reload = Time.time;
                    boxcount = 0;
                }
            } 
            
        }
        
    }

}
