using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatMenuManager : MonoBehaviour
{
    public Constants.Character? selectedChat = null;

    public GameManager gameManager;
    //FFFFFF normal color
    //09FF00 highlight color

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HighlightChatButton(Constants.Character chatChar)
    {
        if (chatChar != selectedChat) {

            var backButtons = GameObject.FindGameObjectsWithTag("RedCircle");
            foreach (GameObject backButton in backButtons)
            {
                var redCircle = backButton.transform.GetChild(0).gameObject;
                if (redCircle.GetComponent<RedCircle>().thisCharacter == selectedChat)
                {
                    gameManager.AddNonReadMessage(redCircle);
                }
            }
        }
    }

    public Constants.Character GetChatChar(string name)
    {
        if (name == Constants.Character.Lucy.ToString().ToUpper())
        {
            return Constants.Character.Lucy;
        }
        else
        {
            return Constants.Character.Jacob;
        }
    }

    public void WhiteChatButton(string name)
    {
        var chatChar = GetChatChar(name);
        selectedChat = chatChar;

        //foreach (Transform child in this.transform.GetChild(0).transform)
        //{
        //    if (child.name.ToUpper() == chatChar.ToString().ToUpper())
        //    {
        //        child.GetComponent<Image>().color = new Color(255, 255, 255);
        //    }
        //}
    }

    public GameObject GetBlueCircle(string character)
    {
        var panel = this.transform.GetChild(0);
        var contactPanel = panel.transform.GetChild(0).GetChild(0);
        foreach (Transform contact in contactPanel.GetChild(0))
        {
            if (contact.name.ToUpper() == character.ToString().ToUpper())
            {
                foreach (Transform child in contact)
                {
                    if (child.name == "BlueCircle")
                    {
                        return child.gameObject;
                    }
                }
            }
        }
        return null;
    }
}
