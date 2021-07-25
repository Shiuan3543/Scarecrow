using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyBox : MonoBehaviour
{
    public float lifetime;

    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, lifetime);
    }

}
