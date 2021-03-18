using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    public Story storyLucy;
    public Story storyJacob;
    
    public Constants.Character thisCharacter;

    public GameManager gameManager;
    public ChatMenuManager chatMenu = null;

    // Start is called before the first frame update
    void Start()
    {

        switch (thisCharacter)
        {
            case Constants.Character.Lucy:

                storyLucy = new Story(); //mudar para ficar com onde parou
                storyLucy.BuildLucyStory();
                FillStoryAndButtons(storyLucy.root, Constants.Character.Lucy);
                break;
            case Constants.Character.Jacob:

                storyJacob = new Story(); //mudar para ficar com onde parou
                storyJacob.BuildJacobStory();
                FillStoryAndButtons(storyJacob.root, Constants.Character.Jacob);
                break;
        }
    }

    void Update()
    {

    }

    void CheckTriggers(Constants.StoryTrigger? receivedTrigger)
    {
        if (receivedTrigger == gameManager.waitingTrigger && receivedTrigger != null)
        {
            gameManager.currentTrigger = receivedTrigger;
            //FillStoryAndButtons(gameManager.waitingStoryBlock, (Constants.Character)gameManager.waitingCharacter);
            //gameManager.waitingStoryBlock = null;
            //gameManager.waitingTrigger = null;
            //gameManager.waitingCharacter = null;
            //gameManager.hasWaitingTrigger = false;
        }
    }

    void FillStoryAndButtons(StoryBlock storyBlock, Constants.Character chatChar)
    {
        if (storyBlock.storyTriggerRequested == null)
        {
            FillStoryAndButtonsWOTrigger(storyBlock, chatChar);
        }
        else if(storyBlock.storyTriggerRequested == gameManager.currentTrigger)
        {
            gameManager.hasWaitingTrigger = false;
            gameManager.waitingTrigger = null;
            FillStoryAndButtonsWOTrigger(storyBlock, chatChar);
        }else if (storyBlock.storyTriggerRequested != gameManager.currentTrigger)
        {

            gameManager.hasWaitingTrigger = true;
            gameManager.waitingTrigger = storyBlock.storyTriggerRequested;
            gameManager.waitingCharacter = chatChar;
            gameManager.waitingStoryBlock = storyBlock;

            StartCoroutine(WaitForTrigger(storyBlock));
        }
    }

    IEnumerator WaitForTrigger(StoryBlock storyBlock)
    {
        while (storyBlock.storyTriggerRequested != gameManager.currentTrigger)
        {
            yield return new WaitForSeconds(0.5f);
        }
        FillStoryAndButtons(gameManager.waitingStoryBlock, (Constants.Character)gameManager.waitingCharacter);
        gameManager.waitingStoryBlock = null;
        gameManager.waitingTrigger = null;
        gameManager.waitingCharacter = null;
        gameManager.hasWaitingTrigger = false;
    }

    void FillStoryAndButtonsWOTrigger(StoryBlock storyBlock, Constants.Character chatChar)
    {
        foreach (Transform child in this.transform.GetChild(0).transform)
        {
            chatMenu.HighlightChatButton(chatChar);
            if (child.name == "Text")
            {
                if (storyBlock.userSelectedText.Length > 0)
                {
                    child.gameObject.transform.GetComponent<Text>().text += "Frost: " + storyBlock.userSelectedText + '\n';
                }
                child.gameObject.transform.GetComponent<Text>().text += chatChar.ToString() + ": " + storyBlock.lucyText + '\n';
            }
            else
            {
                if (child.name == "C1")
                {
                    child.gameObject.GetComponentInChildren<Text>().text = storyBlock.c1_optionText;
                }
                else if (child.name == "C2")
                {
                    child.gameObject.GetComponentInChildren<Text>().text = storyBlock.c2_optionText;
                }
                else if (child.name == "C3")
                {
                    child.gameObject.GetComponentInChildren<Text>().text = storyBlock.c3_optionText;
                }
            }
        }
    }

    public string CharacterName(Constants.Character chatChar)
    {
        switch (chatChar)
        {
            case Constants.Character.Lucy:
                return "Lucy";
            case Constants.Character.Jacob:
                return "Jacob";
            default:
                return "Lucy";
        }
    }

    public Story SwitchCharacters(Constants.Character chatChar)
    {
        switch (chatChar)
        {
            case Constants.Character.Lucy:
                return storyLucy;
            case Constants.Character.Jacob:
                return storyJacob;
            default:
                return storyLucy;
        }
    }

    public void MoveC1(Constants.Character chatChar)
    {
        Story story = SwitchCharacters(chatChar);

        story.currentNode = story.currentNode.c1;
        CheckTriggers(story.currentNode.storyTriggerActivated);
        FillStoryAndButtons(story.currentNode, chatChar);
    }
    public void MoveC2(Constants.Character chatChar)
    {
        Story story = SwitchCharacters(chatChar);

        story.currentNode = story.currentNode.c2;
        CheckTriggers(story.currentNode.storyTriggerActivated);
        FillStoryAndButtons(story.currentNode, chatChar);
    }
    public void MoveC3(Constants.Character chatChar)
    {
        Story story = SwitchCharacters(chatChar);

        story.currentNode = story.currentNode.c3;
        CheckTriggers(story.currentNode.storyTriggerActivated);
        FillStoryAndButtons(story.currentNode, chatChar);
    }
}
