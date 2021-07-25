using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class socksMaker : MonoBehaviour
{
    public GameObject sock;
    public Transform muzzle;
    int sockscount = 0;
    float reload;
    float cooldown;

    void Awake()
    {
        reload = -5;
    }

    void FixedUpdate()
    {
        if (player.playerinstane.playerChange)
        {
            if (Input.GetKeyDown("left"))
                trapStart();
        }
        else if (!player.playerinstane.playerChange)
        {
            if (Input.GetKeyDown(KeyCode.A))
                trapStart();
        }

        if (Time.time - cooldown > 3.5f)
        {
            sockscount = 0;
        }
    }

    void trapStart()
    {
        if (Time.time - reload > 5)
        {
            if (sockscount < 3)
            {
                Instantiate(sock, muzzle.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                sockscount++;
                cooldown = Time.time;

                if (sockscount == 3)
                {
                    reload = Time.time;
                    sockscount = 0;
                }
            }

        }
    }
}
