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
            ChatBallon(storyBlock, chatPlayerPrefab, "Frost");
        }
        if (storyBlock.pcText.Length > 0)
        {
            ChatBallon(storyBlock, chatPCPrefab, chatChar.ToString(), true);
        }

        BuildChoiseButton(c1, storyBlock.c1_optionText);
        BuildChoiseButton(c2, storyBlock.c2_optionText);
        BuildChoiseButton(c3, storyBlock.c3_optionText);
    }
    
    void BuildChoiseButton(GameObject button, string text)
    {
        if (text.Length > 0)
        {
            button.GetComponent<Button>().interactable = true;
            button.GetComponentInChildren<Text>().text = text;
        }
        else
        {
            button.GetComponent<Button>().interactable = false;
            button.GetComponentInChildren<Text>().text = "";
        }
    }

    void PreviewText(string text)
    {
        // panel -> contactPanel -> contactPanel(1) -> gridImage -> Lucy, Jacob...
        var panel = chatMenu.transform.GetChild(0);
        var contactPanel = panel.transform.GetChild(0).GetChild(0);
        foreach (Transform contact in contactPanel.GetChild(0))
        {
            if (contact.name == thisCharacter.ToString())
            {
                foreach (Transform child in contact)
                {
                    if(child.name == "TextPreview")
                    {
                        var previewText = text.Length >= 36 ? text.Substring(0, 36) + "..." : text;
                        child.GetComponent<Text>().text = previewText;
                    }
                    else if (child.name == "BlueCircle" && chatMenu.selectedChat != thisCharacter)
                    {
                        child.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    void ChatBallon(StoryBlock storyBlock, GameObject prefab, string speakerName, bool isPC = false)
    {
        var chat = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        chat.transform.parent = chatGrid.transform;

        var chatLine = speakerName.ToString() + ": " + storyBlock.pcText;
        chat.transform.GetComponent<Text>().text = chatLine + '\n' + '\n';
        chat.transform.GetChild(1).gameObject.transform.GetComponent<Text>().text = chatLine;

        if (isPC)
        {
            PreviewText(storyBlock.pcText);
        }

        StartCoroutine(DelayedRebuild(chat));
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
