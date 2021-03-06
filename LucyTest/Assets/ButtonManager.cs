using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public StoryManager storyManager = null;
    public ChatMenuManager chatMenu = null;
    public GameManager gameManager = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenContactsPage()
    {
        chatMenu.transform.GetChild(0).gameObject.SetActive(true);
        storyManager.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OpenChat()
    {
        var chats = GameObject.FindGameObjectsWithTag("Chat");
        foreach (var chat in chats)
        {
            if (chat.name.ToUpper() == this.name.ToUpper())
            {
                //chatMenu.WhiteChatButton(this.name);
                //open personal chat page
                var chatChar = chatMenu.GetChatChar(chat.name);
                chatMenu.selectedChat = chatChar;
                chat.transform.GetChild(0).gameObject.SetActive(true);

                //non read message blue circle in contact list
                var blueCircle = chatMenu.GetBlueCircle(chat.name);
                if (blueCircle!= null && blueCircle.activeSelf)
                {
                    gameManager.RemoveNonReadMessage();
                }
                blueCircle.gameObject.SetActive(false);

                //non read message red circle in personal chat page
                var storyManager = chat.GetComponent<StoryManager>();
                var redCircle = storyManager.GetRedCircle();
                gameManager.SetNonReadMessage(redCircle);
            }
            else
            {
                chat.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        chatMenu.transform.GetChild(0).gameObject.SetActive(false);
    }


    Constants.Character FindCharacter(Transform pressedButton)
    {
        if (pressedButton.parent.transform.parent.transform.parent.name == "LUCY")
        {
            return Constants.Character.Lucy;
        }
        else if (pressedButton.parent.transform.parent.transform.parent.name == "JACOB")
        {
            return Constants.Character.Jacob;
        }
        return Constants.Character.Lucy;
    }

    public void ChooseOption()
    {
        Transform thisTransform = this.gameObject.transform;
        if (thisTransform.name == "C1")
        {
            var currentCharacter = FindCharacter(thisTransform);
            storyManager.MoveC1(currentCharacter);
        }
        else if(thisTransform.name == "C2")
        {
            var currentCharacter = FindCharacter(thisTransform);
            storyManager.MoveC2(currentCharacter);
        }
        else if (thisTransform.name == "C3")
        {
            var currentCharacter = FindCharacter(thisTransform);
            storyManager.MoveC3(currentCharacter);
        }
    }
}
