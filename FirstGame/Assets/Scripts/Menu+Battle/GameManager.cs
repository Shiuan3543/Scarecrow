using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public bool StartAt; //T=red F=blue
    bool ischange = false;

    public player myPY;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            StartAt = false;
        }
        else if (this != instance)
        {
            //if (SceneManager.GetActiveScene().name == "Menu")
            //{
            //    Destroy(GameObject.Find("GameManagerInMenu"));
            //    instance = this;
            //    DontDestroyOnLoad(this);
            //    StartAt = true;
            //    Debug.Log("換新");
            //}
            //else
            //{
                Destroy(gameObject);
            //}
        }
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            ischange = false;
        }

        if(SceneManager.GetActiveScene().name == "Gaming")
        {
            if (!ischange)
            {
                myPY = GameObject.Find("Player").transform.GetComponent<player>();
                myPY.playerChange = StartAt;
                if (StartAt)
                    myPY.TimetoChange = Time.time;
                ischange = true;
            }
        }
    }
}
