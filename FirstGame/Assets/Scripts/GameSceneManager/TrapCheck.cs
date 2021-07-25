using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCheck : MonoBehaviour
{
    TrapActiveController trapsTAC;

    public Collider2D[] trapOpen;
    public Collider2D[] trapExit;
    public int incount = 0;//計算進入第幾個陷阱
    public int outcount = 0;//計算離開第幾個陷阱
    public bool APortal = true;//第一個傳送門

    //背景音樂開啟
    public AudioSource BGM;


    // Update is called once per frame
    void Update()
    {
        trapOpen = Physics2D.OverlapBoxAll(transform.position, new Vector2(46, 26), 0);
        trapExit = Physics2D.OverlapBoxAll(transform.position, new Vector2(55, 35), 0);

        bool checkthesame = false;

        for (int i = 0; i < trapExit.Length; i++)
        {
            if (trapExit[i].gameObject.layer == LayerMask.NameToLayer("trap"))
            {
                trapsTAC = trapExit[i].gameObject.GetComponent<TrapActiveController>();
                for (int j = 0; j < trapOpen.Length; j++)
                {
                    if (trapExit[i] == trapOpen[j])
                    {
                        checkthesame = true;
                        break;
                    }
                }

                if (checkthesame)
                {
                    trapExit[i].gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    if (trapsTAC.isActive)
                        trapsTAC.needclose = true;
                    else
                        trapExit[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            checkthesame = false;
        }
    }
}
