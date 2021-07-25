using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCheck : MonoBehaviour
{
    public int count = 0;//計算第幾個陷阱
    public bool APortal = true;//第一個傳送門

    // Update is called once per frame
    void Update()
    {
        if (player.playerinstane.transform.localPosition.y < -7)
            count = 0;

        //portal
        if (count == 0)
            APortal = true;
        else if (count == 7)
            APortal = false;

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("trap"))
        {
            for (int i = 0; i < CameraFollower.camerainstane.trap.Length; i++)
            {
                if (player.playerinstane.playerChange)
                {
                    if (count == i && Input.GetKey(KeyCode.D) || Input.GetKeyUp(KeyCode.D))
                    {
                        CameraFollower.camerainstane.trap[i].SetActive(false);
                        count++;
                        break;
                    }
                }
                else if (!player.playerinstane.playerChange)
                {
                    if (count == i && Input.GetKey("right") || Input.GetKeyUp("right"))
                    {
                        CameraFollower.camerainstane.trap[i].SetActive(false);
                        count++;
                        break;
                    }
                }

                //portal
                if (player.playerinstane.myBC.isTrigger == true || player.playerinstane.myCC.isTrigger == true)
                {
                    if (count == i && APortal)
                    {
                        CameraFollower.camerainstane.trap[i].SetActive(false);
                        count++;
                        break;
                    }
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("trap"))
        {
            for (int i = 0; i < CameraFollower.camerainstane.trap.Length; i++)
            {
                if (player.playerinstane.playerChange)
                {
                    if (count == i + 1 && Input.GetKey(KeyCode.A) || Input.GetKeyUp(KeyCode.A))
                    {
                        CameraFollower.camerainstane.trap[i].SetActive(true);
                        count--;

                        break;

                    }
                }
                else if (!player.playerinstane.playerChange)
                {
                    if (count == i + 1 && Input.GetKey("left") || Input.GetKeyUp("left"))
                    {
                        CameraFollower.camerainstane.trap[i].SetActive(true);
                        count--;

                        break;

                    }
                }

                //portal
                if (player.playerinstane.myBC.isTrigger == true || player.playerinstane.myCC.isTrigger == true)
                {
                    if (count == i && !APortal)
                    {
                        CameraFollower.camerainstane.trap[i].SetActive(true);
                        count--;
                        break;
                    }
                }
            }
        }
    }
}
