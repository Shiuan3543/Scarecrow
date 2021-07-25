using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public static CameraFollower camerainstane;
    public Transform target;
    public float smoothing;
    public float worldStartx;
    public float worldEndx;
    Vector3 offset;

    //trap control
    public GameObject[] trap;

    // Start is called before the first frame update
    void Start()
    {
        camerainstane = this;
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cameraFollow();
    }

    void cameraFollow()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        if (transform.position.x < worldStartx)
        {
            if (transform.position.y < 0)
            {
                transform.position = new Vector3(worldStartx, 0, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(worldStartx, transform.position.y, transform.position.z);
            }
        }
        else if (transform.position.x > worldEndx)
        {
            if (transform.position.y < 0)
            {
                transform.position = new Vector3(worldEndx, 0, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(worldEndx, transform.position.y, transform.position.z);
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

}
