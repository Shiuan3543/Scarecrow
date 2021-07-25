using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser_PictureAnim : MonoBehaviour
{
    Animator myAnim;
    laser myLS;
    bool isCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        myLS = GetComponentInChildren<laser>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myLS.checkshoot==true)
        {
            myAnim.SetBool("TrapOpen", true);
            isCheck = false;
        }
        else
        {
            myAnim.SetBool("TrapOpen", false);
            if (!isCheck)
            {
                isCheck = true;
                Invoke("TurnActive", 3);
            }
        }
    }

    void TurnActive()
    {
        myLS.isActive = false;
    }
}
