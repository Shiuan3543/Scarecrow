using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike : MonoBehaviour
{
    public warning myWarning;

    public float smoothing;
    public bool restart = true;
    float toRecover;

    Vector3 startposition;
    Vector3 endposition;

    // Start is called before the first frame update
    void Start()
    {
        startposition = transform.localPosition;
        toRecover = Time.time;
        restart = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (restart)
        {
            toRecover = Time.time;
            restart = false;
        }
        else
        {
            if (Time.time - toRecover < 2)
            {
                transform.localPosition = Vector3.Lerp(startposition, startposition + new Vector3(0f, 9f, 0f), smoothing * Time.deltaTime);
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 1f, 1f), smoothing * Time.deltaTime);

            }
            else if (Time.time - toRecover < 4)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, startposition, (smoothing - 5) * Time.deltaTime);
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 0.5f, 1f), (smoothing - 5) * Time.deltaTime);
            }
            else
                end();
        }
    }

    void end()
    {
        myWarning.changeActive();
        restart = true;
        transform.localPosition = startposition;
        transform.localScale = new Vector3(1, 0.3f, 1);
        gameObject.SetActive(false);
    }

}
