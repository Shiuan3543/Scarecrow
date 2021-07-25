using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedWin : MonoBehaviour
{
    public Animator WinTextAM;
    public Animator BackToMenuButton;
    public Text RedWinText;
    public Color red;

    BoxCollider2D myBC;
    Animator RedOpen;

    // Start is called before the first frame update
    void Start()
    {
        myBC = GetComponent<BoxCollider2D>();
        RedOpen = GetComponent<Animator>();
        WinTextAM.SetBool("GameSet", false);
        BackToMenuButton.SetBool("GameSet", false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            if (RedOpen.GetBool("RedDoorOpen") && (player.playerinstane.playerChange == true))
            {
                WinTextAM.SetBool("GameSet", true);
                BackToMenuButton.SetBool("GameSet", true);
                RedWinText.color = red;
                RedWinText.text = "Red Win!";
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            if (RedOpen.GetBool("RedDoorOpen") && (player.playerinstane.playerChange == true))
            {
                WinTextAM.SetBool("GameSet", true);
                BackToMenuButton.SetBool("GameSet", true);
                RedWinText.color = red;
                RedWinText.text = "Red Win!";
            }
        }
    }

}
