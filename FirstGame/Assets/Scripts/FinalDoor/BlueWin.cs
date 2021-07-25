using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BlueWin : MonoBehaviour
{
    public Animator WinTextAM;
    public Animator BackToMenuButton;
    public Text BlueWinText;
    public Color blue;
    public string GoToMenu;

    BoxCollider2D myBC;
    Animator BlueOpen;

    // Start is called before the first frame update
    void Start()
    {
        myBC = GetComponent<BoxCollider2D>();
        BlueOpen = GetComponent<Animator>();
        WinTextAM.SetBool("GameSet", false);
        BackToMenuButton.SetBool("GameSet", false);
    }

    private void Update()
    {
        //測試用
        if (Input.GetKey(KeyCode.P))
        {
            SceneManager.LoadScene(GoToMenu);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            if (BlueOpen.GetBool("BlueDoorOpen") && (player.playerinstane.playerChange == false))
            {
                WinTextAM.SetBool("GameSet", true);
                BackToMenuButton.SetBool("GameSet", true);
                BlueWinText.color = blue;
                BlueWinText.text = "Blue Win!";
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            if (BlueOpen.GetBool("BlueDoorOpen") && (player.playerinstane.playerChange == false))
            {
                WinTextAM.SetBool("GameSet", true);
                BackToMenuButton.SetBool("GameSet", true);
                BlueWinText.color = blue;
                BlueWinText.text = "Blue Win!";
            }
        }
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(GoToMenu);
    }
}
