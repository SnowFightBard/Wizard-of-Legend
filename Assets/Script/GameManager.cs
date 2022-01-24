using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    private GameObject scanObject;
    public bool isTalk;


    public void Talk(GameObject scanObj)
    {
        if (isTalk)
        {
            isTalk = false;
            talkPanel.SetActive(false);
        }
        else
        {
            if (GameObject.Find("Player").GetComponent<PlayerMove>().talkNpc == false) return;
            isTalk = true;
            talkPanel.SetActive(true);
            scanObject = scanObj;
            talkText.text = "�̰��� �̸��� " + scanObject.name + " �Դϴ�.";
        }

        
    }
}
