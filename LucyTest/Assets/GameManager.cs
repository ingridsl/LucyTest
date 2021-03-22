using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Constants.StoryTrigger? currentTrigger = null;

    public int nonRedMessages = 0;

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
        nonRedMessages++;
        if (redCircle.activeSelf)
        {
            redCircle.transform.GetChild(0).GetComponent<Text>().text = nonRedMessages.ToString();
        }
        else
        {
            redCircle.SetActive(true);
            redCircle.transform.GetChild(0).GetComponent<Text>().text = nonRedMessages.ToString();
        }
    }

    public void RemoveNonReadMessage(GameObject redCircle)
    {
        nonRedMessages--;
        if (nonRedMessages != 0)
        {
            redCircle.transform.GetChild(0).GetComponent<Text>().text = nonRedMessages.ToString();
        }
        else
        {
            redCircle.SetActive(false);
            redCircle.transform.GetChild(0).GetComponent<Text>().text = nonRedMessages.ToString();
        }
    }

    public void SetNonReadMessage(GameObject redCircle)
    {
        if (nonRedMessages != 0)
        {
            redCircle.SetActive(true);
        }
        else
        {
            redCircle.SetActive(false);
        }
    }
}
