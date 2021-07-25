using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActiveController : MonoBehaviour
{
    public bool isActive = false;
    public bool needclose = false;

    // Update is called once per frame
    void Update()
    {
        if(needclose && !isActive)
        {
            needclose = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }

    }
}
