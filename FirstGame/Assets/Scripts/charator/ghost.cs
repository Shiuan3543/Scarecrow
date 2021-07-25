using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    float radious;
    float targetGhostPos;
    bool facingRight = true;
    bool colorChange = false;
    Animator myAnim;

    float updateTime;
    float xPosition;
    float yPosition;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        radious = Vector2.Distance(target.position, transform.position);
        xPosition = Random.Range(-3, 4);
        yPosition = Random.Range(-2, 5);
        updateTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerChange();
        FacingChange();
        TimeCheck();
    }

    void PlayerChange()
    {
        if (player.playerinstane.playerChange != colorChange)
        {
            colorChange = player.playerinstane.playerChange;
            myAnim.SetBool("playerChange", colorChange);
            if (player.playerinstane.facingRight)
            {
                transform.position = target.transform.position + new Vector3(3.5f, 0.5f, 0);
            }
            else
            {
                transform.position = target.transform.position + new Vector3(-3.5f, 0.5f, 0);
            }

        }
        else
        {
            GhostMove();
        }
    }

    void GhostMove()
    {

        if (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("ghost_invisiable"))
        {
            //followTarget
            targetGhostPos = Vector2.Distance(target.position, transform.position);
            if (targetGhostPos > radious + 1f)
            {
                transform.position = Vector3.Lerp(transform.position, target.position, smoothing * Time.deltaTime);
                myAnim.SetBool("startmove", true);
            }
            else if (targetGhostPos > radious+0.25f)
            {
                myAnim.SetBool("startmove", false);
                transform.position = Vector3.Lerp(transform.position, target.position, smoothing * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, target.position + new Vector3(xPosition, yPosition, 0), smoothing * Time.deltaTime);
            }
        }

    }

    void TimeCheck()
    {
        if (Time.time - updateTime > 5)
        {
            updateTime = Time.time;
            xPosition = Random.Range(-2, 2);
            yPosition = Random.Range(-1, 4);
        }
        else
        {
            xPosition += Random.Range(-0.25f, 0.25f);
            yPosition += Random.Range(-0.25f, 0.25f);
            if (xPosition > 2.5f)
            {
                xPosition = 2;
            }
            else if (xPosition < -3.5f)
            {
                xPosition = -3;
            }
            else if (xPosition < 1 && xPosition > -1)
            {
                xPosition = 1.5f;
            }

            if (yPosition > 3.5f)
            {
                yPosition = 3f;
            }
            else if (yPosition < -2)
            {
                yPosition = -1.5f;
            }else if (yPosition < 1 && yPosition > -1)
            {
                yPosition = 1.5f;
            }
        }
    }

    void FacingChange()
    {
        //facingRight
        float xDis = target.position.x - transform.position.x;
        if ((xDis < 0 && facingRight == true) || (xDis > 0 && facingRight == false))
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            facingRight = !facingRight;
        }
    }
}
