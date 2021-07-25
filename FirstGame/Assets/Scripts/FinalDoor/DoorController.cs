using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator BlueA;
    public Animator RedA;

    Animator myAM;
    AudioSource myAS;

    int DoorSwitch = 0;// 0=null, 1=blue, 2=red

    void Start()
    {
        myAM = GetComponent<Animator>();
        myAS = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            if (player.playerinstane.playerChange && DoorSwitch != 2)
            {
                myAM.SetBool("RedOn", true);
                myAM.SetBool("BlueOn", false);
                BlueA.SetBool("BlueDoorOpen", false);
                RedA.SetBool("RedDoorOpen", true);
                DoorSwitch = 2;

                //partical
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.transform.GetChild(1).gameObject.SetActive(true);

                myAS.Play();

            }
            else if (!player.playerinstane.playerChange && DoorSwitch != 1)
            {
                myAM.SetBool("BlueOn", true);
                myAM.SetBool("RedOn", false);
                BlueA.SetBool("BlueDoorOpen", true);
                RedA.SetBool("RedDoorOpen", false);
                DoorSwitch = 1;

                //partical
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.SetActive(false);

                myAS.Play();

            }
        }
    }
}
