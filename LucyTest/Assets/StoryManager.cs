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

    public GameObject chatGrid;
    public GameObject c1;
    public GameObject c2;
    public GameObject c3;

    public GameObject chatPCPrefab;
    public GameObject chatPlayerPrefab;

    public GameObject redCircle;

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
        if (receivedTrigger != null)
        {
            gameManager.currentTrigger = receivedTrigger;
        }
    }

    void FillStoryAndButtons(StoryBlock storyBlock, Constants.Character chatChar)
    {
        if (storyBlock.storyTriggerRequested == null)
        {
            FillStoryAndButtonsWOTrigger(storyBlock, chatChar);
        }
        else if (storyBlock.storyTriggerRequested == gameManager.currentTrigger)
        {
            FillStoryAndButtonsWOTrigger(storyBlock, chatChar);
        }
        else if (storyBlock.storyTriggerRequested != gameManager.currentTrigger)
        {
            StartCoroutine(WaitForTrigger(storyBlock, chatChar));
        }
    }

    IEnumerator WaitForTrigger(StoryBlock storyBlock, Constants.Character chatChar)
    {
        while (storyBlock.storyTriggerRequested != gameManager.currentTrigger)
        {
            yield return new WaitForSeconds(0.5f);
        }
        
        FillStoryAndButtons(storyBlock, chatChar);
    }


    void FillStoryAndButtonsWOTrigger(StoryBlock storyBlock, Constants.Character chatChar)
    {
        chatMenu.HighlightChatButton(chatChar);
        //gameManager.RemoveNonReadMessage(redCircle);

        if (storyBlock.userSelectedText.Length > 0)
        {

            var chatPlayer = Instantiate(chatPlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            chatPlayer.transform.parent = chatGrid.transform;

            var chatLine = "Frost: " + storyBlock.userSelectedText;
            chatPlayer.transform.GetComponent<Text>().text = chatLine + '\n';
            chatPlayer.transform.GetChild(1).gameObject.transform.GetComponent<Text>().text = chatLine;

            StartCoroutine(DelayedRebuild(chatPlayer));
        }
        if (storyBlock.pcText.Length > 0)
        {
            var chatPC = Instantiate(chatPCPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            chatPC.transform.parent = chatGrid.transform;

            var chatLine = chatChar.ToString() + ": " + storyBlock.pcText;
            chatPC.transform.GetComponent<Text>().text = chatLine + '\n';
            chatPC.transform.GetChild(1).gameObject.transform.GetComponent<Text>().text = chatLine;

            StartCoroutine(DelayedRebuild(chatPC));
        }
        c1.GetComponentInChildren<Text>().text = storyBlock.c1_optionText;
        c2.GetComponentInChildren<Text>().text = storyBlock.c2_optionText;
        c3.GetComponentInChildren<Text>().text = storyBlock.c3_optionText;

    }

    IEnumerator DelayedRebuild(GameObject chat)
    {
        yield return new WaitForSeconds(0.01f);
        chat.SetActive(false);
        yield return new WaitForSeconds(0.01f);
        chat.SetActive(true);
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
