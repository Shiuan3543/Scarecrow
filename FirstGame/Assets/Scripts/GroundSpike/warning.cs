using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warning : MonoBehaviour
{
    public TrapActiveController myTAC;
    public bool isActive = false;

    Animator myAnim;
    public GameObject spikeTexture;
    public GameObject warningLight;
    float sixSec;//reload trap

    // Start is called before the first frame update
    void Awake()
    {
        sixSec = -6;
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time - sixSec > 6)
        {
            if (player.playerinstane.playerChange)
            {
                if (Input.GetKeyDown("down"))
                {
                    isActive = true;
                    sixSec = Time.time;
                    warningLight.SetActive(true);
                    myAnim.SetBool("invisiable", false);
                    Invoke("closeAnim", 1);
                }
            }
            else if (!player.playerinstane.playerChange)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    isActive = true;
                    sixSec = Time.time;
                    warningLight.SetActive(true);
                    myAnim.SetBool("invisiable", false);
                    Invoke("closeAnim", 1);
                }
            }
        }
        myTAC.isActive = isActive;

    }

    void closeAnim()
    {
        spikeTexture.SetActive(true);
        warningLight.SetActive(false);
        myAnim.SetBool("invisiable", true);
    }

    public void changeActive()
    {
        Invoke("a", 1f);
    }

    private void a()
    {
        isActive = false;
    }
}
