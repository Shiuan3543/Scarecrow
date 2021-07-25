using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    public TrapActiveController myTAC;
    public bool isActive = false;

    public GameObject Rot;//軸心
    public GameObject playerGameObject;
    public GameObject line;
    public GameObject particle;
    public BoxCollider2D collider1;
    public LineRenderer lenth;
    public float speed;
    float angle;//跟玩家的角度
    float angle2;
    bool checkhigh;//檢查玩家高度是否超過機器高度
    public bool checkshoot=false;//射擊時無法轉動
    int direction;//方向(值=+1或-1)
    public bool hitplayer;
    float time;
    float time2;
    public float timeControlbegin; //光束開始射擊
    public float timeControlend; //光束結束射擊
    public float timeContnext; //冷卻時間

    bool sound = false;
    AudioSource myAS;

    void Awake()
    {
        myAS = GetComponent<AudioSource>();
        time2 = timeContnext + 1;
    }

    // Update is called once per frame
    void Update()
    {
        //判斷區
        if (player.playerinstane.playerChange)
        {
            if (Input.GetKeyDown("right") && time2 > timeContnext && !checkshoot)
            {
                checkshoot = true;
                isActive = true;
            }
        }
        else if(!player.playerinstane.playerChange)
        {
            if (Input.GetKeyDown(KeyCode.D) && time2 > timeContnext && !checkshoot)
            {
                checkshoot = true;
                isActive = true;
            }
        }



        //射擊控制
        if (checkshoot == true)
            time += Time.deltaTime;

        else
        {
            time = 0;
            time2 += Time.deltaTime;
        }

        if (time < timeControlbegin && time > 0)
            particle.SetActive(true);
        else
            particle.SetActive(false);
        if (time > timeControlbegin)
        {
            line.SetActive(true);

            //聲音部分
            if (!sound)
            {
                myAS.Play();
                sound = true;
            }
        }
        else
        {
            line.SetActive(false);
            sound = false;
        }
        if (time > timeControlend)
        {
            checkshoot = false;
            time2 = 0;
        }

        //砲台部分
        checkhigh = (Rot.transform.position.y < playerGameObject.transform.position.y) ? true : false;
        if (checkhigh)
        {
            angle = -Mathf.Rad2Deg * Mathf.Atan((Rot.transform.position.x - playerGameObject.transform.position.x) / (Rot.transform.position.y - playerGameObject.transform.position.y)) + 180;
            if (angle > 180)
                angle -= 360;
        }
        else if (!checkhigh)
        {
            angle = -Mathf.Rad2Deg * Mathf.Atan((Rot.transform.position.x - playerGameObject.transform.position.x) / (Rot.transform.position.y - playerGameObject.transform.position.y));
        }

        if (checkshoot == false)
            Rot.transform.Rotate(new Vector3(0, 0, speed * direction), Space.Self);//轉動   

        if (Rot.transform.position.x > transform.position.x)
            angle2 = gameObject.transform.rotation.eulerAngles.z - 360;
        else if (Rot.transform.position.x < transform.position.x)
            angle2 = gameObject.transform.rotation.eulerAngles.z;
        if (angle > angle2 && angle2 < 90 && Mathf.Abs(angle - angle2) > 3)
            direction = 1;
        else if (angle < angle2 && angle2 > -90 && Mathf.Abs(angle - angle2) > 3)
            direction = -1;
        else
            direction = 0;

        //光束部分

        lenth.SetPosition(0, new Vector3(0, 0, 0));

        RaycastHit2D hit = Physics2D.Raycast(line.transform.position, line.transform.position - Rot.transform.position, 40, -1);


        if (hit.collider)
        {
            lenth.SetPosition(1, new Vector3(0, -Vector2.Distance(transform.position, hit.point), 0));
            collider1.size = new Vector2(2 / 10, Vector2.Distance(transform.position, hit.point));
            collider1.offset = new Vector2(0, -Vector2.Distance(transform.position, hit.point) / 2);
            if (hit.collider.gameObject == playerGameObject && checkshoot)
            {
                hitplayer = true;
                if (time > timeControlbegin)
                {
                    player.playerinstane.dead();
                }
            }
            else
                hitplayer = false;
        }
        else
        {
            lenth.SetPosition(1, new Vector3(0, -40, 0));
            collider1.size = new Vector2(2 / 10, 20);
            collider1.offset = new Vector2(0, -5);
        }

        myTAC.isActive = isActive;
    }

}
