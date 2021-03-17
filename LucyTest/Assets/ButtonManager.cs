using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public StoryManager storyManager = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenChat()
    {
        var chats = GameObject.FindGameObjectsWithTag("Chat");
        foreach (var chat in chats)
        {
            if (chat.name == this.name)
            {
                chat.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                chat.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }


    Constants.Character FindCharacter(Transform pressedButton)
    {
        if (pressedButton.parent.transform.parent.name == "LUCY")
        {
            return Constants.Character.Lucy;
        }
        else if (pressedButton.parent.transform.parent.name == "JACOB")
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
