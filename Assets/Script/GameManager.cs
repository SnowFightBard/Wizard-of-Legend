using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    private GameObject scanObject;
    public bool isAction;


    public void action(GameObject scanObj)
    {
        if (isAction)
        {
            isAction = false;
            talkPanel.SetActive(false);
        }
        else
        {
            isAction = true;
            talkPanel.SetActive(true);
            scanObject = scanObj;
            talkText.text = "�̰��� �̸��� " + scanObject.name + " �Դϴ�.";
        }

        
    }
}
