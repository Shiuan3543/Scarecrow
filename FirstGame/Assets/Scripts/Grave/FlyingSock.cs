using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSock : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D cc;
    Animator am;

    AudioSource myAS;
    public AudioClip[] myAC;

    public float speed = 5;
    public float rotateSpeed = 200;

    public bool LockOn = false;
    public bool Stillinside = false;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        am = GetComponent<Animator>();
        myAS = GetComponent<AudioSource>();
        myAS.clip = myAC[0];
        myAS.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (LockOn)
        {
            rb.velocity = new Vector2(0, 0);
            cc.radius = 5;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Invoke("sockDestroy", 1);
        }
        else
        {
            Vector2 direction = (Vector2)player.playerinstane.transform.position - rb.position;
            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = -rotateAmount * rotateSpeed;
            rb.velocity = transform.up * speed;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            LockOn = true;
            Stillinside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
            Stillinside = false;

    }

    void sockDestroy()
    {
        if (Stillinside)
            player.playerinstane.dead();

        myAS.clip = myAC[1];
        myAS.Play();
        am.SetBool("boom", true);

        Destroy(gameObject, 0.5f);

        CancelInvoke("sockDestroy");
    }
}
