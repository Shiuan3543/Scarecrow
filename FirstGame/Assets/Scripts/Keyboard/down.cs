using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class down : MonoBehaviour
{
    //顏色和圖片
    SpriteRenderer myRenderer;
    public Color white;
    public Color gray;
    public Sprite spriteA;
    public Sprite spriteB;

    //用時間更改顏色
    public int count;//能使用的次數
    public float TrapReloadingTime;//loading時間
    int iscount = 0;//已使用次數
    float TimeCheck;//記錄開始loading的當下時間

    public GameObject thisTrap;

    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        TimeCheck = -TrapReloadingTime;
    }

    // Update is called once per frame
    // Update is called once per frame
    void FixedUpdate()
    {
        //更改圖片
        if (player.playerinstane.playerChange)
        {
            myRenderer.sprite = spriteB;

            //變色or++
            if (thisTrap.activeSelf == true && Input.GetKeyDown("down"))
            {
                if (iscount == count && myRenderer.color != gray)
                {
                    iscount = 0;
                    TimeCheck = Time.time;
                    myRenderer.color = gray;
                }
                else if (myRenderer.color != gray)
                {
                    iscount++;
                }
            }
        }
        else if (!player.playerinstane.playerChange)
        {
            myRenderer.sprite = spriteA;

            //變色or++
            if (thisTrap.activeSelf == true && Input.GetKeyDown(KeyCode.S))
            {
                if (iscount == count && myRenderer.color != gray)
                {
                    iscount = 0;
                    TimeCheck = Time.time;
                    myRenderer.color = gray;
                }
                else if (myRenderer.color != gray)
                {
                    iscount++;
                }
            }
        }

        //確定時間
        if (Time.time - TimeCheck >= TrapReloadingTime)
        {
            myRenderer.color = white;
        }
    }
}
