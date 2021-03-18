using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StoryBlock
{
    public Constants.StoryTrigger? storyTriggerActivated;
    public Constants.StoryTrigger? storyTriggerRequested;

    public string userSelectedText;
    public string lucyText;

    public string c1_optionText;
    public StoryBlock c1;

    public string c2_optionText;
    public StoryBlock c2;

    public string c3_optionText;
    public StoryBlock c3;
};

public class Story
{
    enum Position{
        C1,
        C2,
        C3
    };

    public StoryBlock root;
    public StoryBlock currentNode;
    
    public void BuildLucyStory()
    {
        if (root == null)
        {

            // INITIAL
            root = Insert(null,
                    "", "Hi Frost",
                    "Hello", "", "",
                    Position.C1, null, null);
            currentNode = root;

            StoryBlock node = Insert(root,
                    "Hello, Lucy", "Are you okay after what happened?",
                    "I'm very sad...", " Yes, life goes on", "I'm almost there",
                    Position.C1, null, null);
            //1.C1
            var aux = Insert(node,
                    "You know, Lucy, it's very hard to forget what happened!", "For sure Frost, I imagine how you feel",
                    "Are you sure, Lucy?", "blabla", "blabla",
                    Position.C1, null, null);
            //1.C2
            aux = Insert(node,
                    "Yes, sometimes you have to move on!", "I knew you would get over it fast!",
                    "It seems so easy to you", "blabla", "blabla",
                    Position.C2, null, null);
            //1.C3
            aux = Insert(node,
                    "I'm getting over it, Lucy, little by little", "How can I help you with this?",
                    "Couldn't you have asked that question before?", "blabla", "blabla",
                    Position.C3, Constants.StoryTrigger.ST0, null);
        }

    }

    public void BuildJacobStory()
    {
        if (root == null)
        {

            // INITIAL
            root = Insert(null,
                    "", "HEY FROST. WHATS UP?",
                    "Hey Jacob", "", "",
                    Position.C1, null, Constants.StoryTrigger.ST0);
            currentNode = root;

            StoryBlock node = Insert(root,
                    "Hello, Jacob. I was talking with Lucy just now", "Heh. She is ignoring me since that day",
                    "What happened?", "That day?", "I kinda understand her.",
                    Position.C1, null, null);
            //1.C1
            var aux = Insert(node,
                    "What happened?", "You forgot?",
                    "Maybe...?", "blabla", "blabla",
                    Position.C1, null, null);
            //1.C2
            aux = Insert(node,
                    "That day at the beach?", "Yeah! Don't you tell me you already forgot.",
                    "No, just checking", "blabla", "blabla",
                    Position.C2, null, null);
            //1.C3
            aux = Insert(node,
                    "I kinda understand her.", "I should have imagined you would take her side.",
                    "And can you even blame me?", "blabla", "blabla",
                    Position.C3, null, null);
        }

    }

    StoryBlock Insert(StoryBlock root,
        string userSelectedText, string lucyReplyText,
        string c1_optionText, string c2_optionText, string c3_optionText,
        Position position,
        Constants.StoryTrigger? storyTriggerActivated, Constants.StoryTrigger? storyTriggerRequested)
    {        
        StoryBlock node = new StoryBlock();
        node.userSelectedText = userSelectedText;
        node.lucyText = lucyReplyText;

        node.c1_optionText = c1_optionText;
        node.c2_optionText = c2_optionText;
        node.c3_optionText = c3_optionText;

        node.storyTriggerActivated = storyTriggerActivated;
        node.storyTriggerRequested = storyTriggerRequested;

        if (root == null)
        {
            return node;
        }

        switch (position)
        {
            case Position.C1:
                root.c1 = node;
                break;
            case Position.C2:
                root.c2 = node;
                break;
            case Position.C3:
                root.c3 = node;
                break;
        }
        return node;
    }    
}
