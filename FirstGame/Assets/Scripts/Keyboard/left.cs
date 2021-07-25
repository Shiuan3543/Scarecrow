using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class left : MonoBehaviour
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

    //新增閒置後自動回補
    float cooldown;
    public float Howlong;

    //玩家碰到傳送門的時間
    public bool istouch = false;
    float saveLoadingTime;


    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        TimeCheck = -TrapReloadingTime;
        saveLoadingTime = TrapReloadingTime;
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
            if (thisTrap.activeSelf == true && Input.GetKeyDown("left"))
            {
                if (iscount == count && myRenderer.color != gray)
                {
                    TrapReloadingTime = saveLoadingTime;
                    iscount = 0;
                    TimeCheck = Time.time;
                    myRenderer.color = gray;
                }
                else if (myRenderer.color != gray)
                {
                    iscount++;
                    cooldown = Time.time;
                }
            }
        }
        else if (!player.playerinstane.playerChange)
        {
            myRenderer.sprite = spriteA;

            //變色or++
            if (thisTrap.activeSelf == true && Input.GetKeyDown(KeyCode.A))
            {
                if (iscount == count && myRenderer.color != gray)
                {
                    TrapReloadingTime = saveLoadingTime;
                    iscount = 0;
                    TimeCheck = Time.time;
                    myRenderer.color = gray;
                }
                else if (myRenderer.color != gray)
                {
                    iscount++;
                    cooldown = Time.time;
                }
            }
        }

        //確定時間
        if (Time.time - TimeCheck >= TrapReloadingTime)
        {
            myRenderer.color = white;
        }

        if (Time.time - cooldown > Howlong)
        {
            iscount = 0;
        }
    }

    public void portaling()
    {
        if (istouch)
        {
            istouch = false;
            TimeCheck = Time.time;
            TrapReloadingTime = 3f;
        }
        else
        {
            istouch = true;
            TimeCheck = Time.time;
        }
    }
}
