using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleSceneManager : MonoBehaviour
{
    public Slider MySlider;
    public Animator TTS;
    public Text Start321;
    public Text Time15;
    public string GoToGaming;
    public AudioClip[] myAC;

    public Animator background;

    AudioSource myAS;
    public GameManager gameManager;

    bool ClickTheRuleButton = false;
    bool TimesUp = false;
    bool MakeASound = false;    //只發出一次聲音
    float threeTimeCheck; 
    float tenTimeCheck = 10;

    //結算
    float talktime = 0; 
    //special
    public Animator SliderA;
    bool Cheating = false;
    int countSentence = 0;

    void Awake()
    {
        myAS = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManagerInMenu").transform.GetComponent<GameManager>();
        MySlider.value = 20;
    }

    void FixedUpdate()
    {
        //把規則按掉
        if (ClickTheRuleButton)
        {
            //結算
            if (TimesUp)
            {
                //計算時間
                talktime += Time.deltaTime;
                if (!Cheating)
                {
                    EndScene();
                    if(talktime >= 3)
                        ChangeScene();

                }
                else
                {
                    Special();
                    if(countSentence >= 3)
                    {
                        gameManager.StartAt = !gameManager.StartAt;
                        ChangeScene();
                    }
                }
            }
            //數3秒
            else if (Time.time - threeTimeCheck <= 0)
                TimeToStart();
            //數10秒
            else
            {
                FinalTime();
                background.SetBool("black", true);
            }
        }

        //測試用
        if (Input.GetKey(KeyCode.P))
        {
            tenTimeCheck = 0;
        }

    }

    public void closeRule()
    {
        ClickTheRuleButton = true;
        threeTimeCheck = Time.time + 3.5f;

        //關掉規則
        gameObject.transform.GetChild(3).gameObject.SetActive(false);
    }

    //數三秒
    void TimeToStart()
    {
        //開啟3秒倒數
        gameObject.transform.GetChild(2).gameObject.SetActive(true);

        if (threeTimeCheck - Time.time >= 2.5f)
        {
            Start321.text = "3";
        }
        else if (threeTimeCheck - Time.time >= 1.5f)
        {
            if (threeTimeCheck - Time.time >= 2.4f)            
                MakeASound = false;
            
            Start321.text = "2";
        }
        else if (threeTimeCheck - Time.time >= 0.5f)
        {
            if (threeTimeCheck - Time.time >= 1.4f)
                MakeASound = false;

            Start321.text = "1";
        }
        else
        {
            if (threeTimeCheck - Time.time >= 0.4f)
                MakeASound = false;

            TTS.SetBool("AfterThreeSec", true);
            Start321.text = "START";
        }

        //倒數聲
        if (!MakeASound)
        {
            if (threeTimeCheck - Time.time <= 0.5f)
                soundManager(0);
            else
                soundManager(1);

            MakeASound = true;
        }
    }

    //10秒倒數
    void FinalTime()
    {
        int IntNum;
        int Decimal;

        //calculate time
        Start321.gameObject.SetActive(false);
        tenTimeCheck -= Time.deltaTime;
        IntNum = (int)tenTimeCheck;
        Decimal = (int)(tenTimeCheck * 100) - IntNum * 100;

        if (tenTimeCheck > 6)
        {
            if (Decimal < 10)
                Time15.text = IntNum + ":0" + Decimal;
            else
                Time15.text = IntNum + ":" + Decimal;
        }
        else if (tenTimeCheck > 0)
        {
            Time15.color = Color.red;

            if (Decimal < 10)
                Time15.text = IntNum + ":0" + Decimal;
            else
                Time15.text = IntNum + ":" + Decimal;
        }
        else if (tenTimeCheck <= 0)
        {
            Time15.text = "0:00";
            TimesUp = true;
        }

        //battle
        if (Input.GetKeyDown("left") || Input.GetKeyDown("right"))
        {
            MySlider.value -= 1;
            //soundManager(3);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            MySlider.value += 1;
            //soundManager(3);
        }

        //之後結束音效
        MakeASound = false;

    }

    //結束確認
    void EndScene()
    {
        //背景
        background.SetBool("black", false);

        Start321.gameObject.SetActive(true);
        TTS.SetBool("ReadyToChange", true);

        //修改
        if (MySlider.value > 20)
        {
            gameManager.StartAt = true; //Red
            Start321.text = "Red First";

            if (Input.GetKeyDown(KeyCode.L))//Red->Blue
            {
                Cheating = true;
            }
        }
        else if (MySlider.value <= 20)
        {
            gameManager.StartAt = false; //Blue 
            Start321.text = "Blue First";

            if (Input.GetKeyDown(KeyCode.R))//Blue->Red
            {
                Cheating = true;
            }
        }

        if (!MakeASound)
        {
            soundManager(2);
            MakeASound = true;
        }
    }

    void Special()
    {
        //傾斜
        if (gameManager.StartAt) //Red->Blue
        {
            SliderA.SetBool("blue", true);
            if(MySlider.value > 0)
            {
                MySlider.value -= 1f;
            }
        }
        else                     //Blue->Red
        {
            SliderA.SetBool("red", true);
            if (MySlider.value < 39)
            {
                MySlider.value += 1f;
            }
        }


        if (talktime >= 3f)
        {
            talktime = 0;
            countSentence++;
        }
        //對白
        switch (countSentence)
        {
            case 0:
                Start321.fontSize = 100;
                Start321.text = "...";
                break;
            case 1:
                Start321.text = "ㄜ 看來是我看錯了呢";
                talktime += Time.deltaTime;
                break;
            case 2:
                Start321.fontSize = 200;
                if (MySlider.value > 20)
                {
                    Start321.text = "Red First";
                }
                else
                {
                    Start321.text = "Blue First";
                }

                if (!MakeASound)
                {
                    soundManager(2);
                    MakeASound = true;
                }
                break;
            case 3:
                Start321.text = "哈哈";
                talktime += Time.deltaTime*2;
                break;
            default:
                break;
        }
    }           

    //換場景
    void ChangeScene()
    {
        SceneManager.LoadScene(GoToGaming);
    }

    //音效
    void soundManager(int number)
    {
        myAS.Stop();
        myAS.clip = myAC[number];
        myAS.Play();
        
    }
}
