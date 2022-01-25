using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{ 
    // UI ���� �� ���� ������Ʈ�� �����ؾ��ϴ� �����͸� ���� ��ũ��Ʈ

    public List<SkillSpawn> data;  // ��ų ������ (����,���ҽ� ����)
    public GameObject talkPanel;
    public Text talkText;
    private GameObject scanObject;
    public bool isTalk;


    private void Start()
    {
        // ScriptableObject ���Ͽ��� ������ ��ų���� index���� �������� ������������
        data.Sort(delegate (SkillSpawn a, SkillSpawn b)
        {
            if (a.index > b.index) return 1;
            else if (a.index < b.index) return -1;
            return 0;
        }

        );
    }


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
