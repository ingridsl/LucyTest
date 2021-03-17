using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    Story storyLucy = null;
    Story storyJacob = null;


    public Constants.Character thisCharacter;

    // Start is called before the first frame update
    void Start()
    {
        storyLucy = new Story(); //mudar para ficar com onde parou
        storyLucy.BuildLucyStory();

        storyJacob = new Story(); //mudar para ficar com onde parou
        storyJacob.BuildJacobStory();

        switch(thisCharacter)
        {
            case Constants.Character.Lucy:
                FillStoryAndButtons(storyLucy.root, Constants.Character.Lucy);
                break;
            case Constants.Character.Jacob:
                FillStoryAndButtons(storyJacob.root, Constants.Character.Jacob);
                break;
        }
    }

    void FillStoryAndButtons(StoryBlock storyBlock, Constants.Character chatChar)
    {
        foreach (Transform child in this.transform)
        {
            if (child.name == "Text")
            {
                if (storyBlock.userSelectedText.Length > 0)
                {
                    child.gameObject.transform.GetComponent<Text>().text += "Frost: " + storyBlock.userSelectedText + '\n';
                }
                child.gameObject.transform.GetComponent<Text>().text += chatChar.ToString()  + ": " + storyBlock.lucyText + '\n';
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
        FillStoryAndButtons(story.currentNode, chatChar);
    }
    public void MoveC2(Constants.Character chatChar)
    {
        Story story = SwitchCharacters(chatChar);

        story.currentNode = story.currentNode.c2;
        FillStoryAndButtons(story.currentNode, chatChar);
    }
    public void MoveC3(Constants.Character chatChar)
    {
        Story story = SwitchCharacters(chatChar);

        story.currentNode = story.currentNode.c3;
        FillStoryAndButtons(story.currentNode, chatChar);
    }
}
