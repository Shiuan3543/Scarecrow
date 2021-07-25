using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string GoToBattle;

    public void StartButton()
    {
        SceneManager.LoadScene(GoToBattle);
    }

    public void HowToPlayButton()
    {
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void CloseRule()
    {
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
    }
}
