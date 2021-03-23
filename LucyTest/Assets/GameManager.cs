using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Constants.StoryTrigger? currentTrigger = null;

    [System.NonSerialized] public int nonReadMessages = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddNonReadMessage(GameObject redCircle)
    {
        nonReadMessages++;
        if (redCircle.activeSelf)
        {
            if (nonReadMessages > 0 ) {
                redCircle.transform.GetChild(0).GetComponent<Text>().text = nonReadMessages.ToString();
            }
            else
            {
                redCircle.SetActive(false);
            }
        }
        else
        {
            redCircle.SetActive(true);
            redCircle.transform.GetChild(0).GetComponent<Text>().text = nonReadMessages.ToString();
        }
    }

    public void RemoveNonReadMessage()
    {
        nonReadMessages = nonReadMessages == 0 ? nonReadMessages : nonReadMessages - 1;
    }

    public void SetNonReadMessage(GameObject redCircle)
    {
        if (nonReadMessages > 0)
        {
            redCircle.SetActive(true);
            redCircle.transform.GetChild(0).GetComponent<Text>().text = nonReadMessages.ToString();
        }
        else
        {
            redCircle.SetActive(false);
        }
    }
}
