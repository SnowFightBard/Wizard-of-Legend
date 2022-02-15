using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{ 
    // UI ���� �� ���� ������Ʈ�� �����ؾ��ϴ� �����͸� ���� ��ũ��Ʈ //

    public List<Skill> data;  // ��ų ������ (����,���ҽ� ����)
    public GameObject talkPanel;    // ��ȭâ
    public GameObject inventory;    // �κ��丮 â
    public Text talkText;           // ��ȭ �ؽ�Ʈ
    private GameObject scanObject;  // ������ ������Ʈ
    public bool isTalk;             // ��ȭ������ üũ�ϴ� ����
    public int talkIndex;           // �ش� NPC�� ��ȭ�ؽ�Ʈ INDEX
    TalkManager tm;


    private void Start()
    {
        tm = this.GetComponent<TalkManager>();

        // ScriptableObject ���Ͽ��� ������ ��ų���� index���� �������� ������������
        data.Sort(delegate (Skill a, Skill b)
        {
            if (a.index > b.index) return 1;
            else if (a.index < b.index) return -1;
            return 0;
        }

        );
    }


    // �÷��̾ ������Ʈ �տ��� F(�׼ǹ�ư)�� ������ ��
    public void Action(GameObject scanObj)
    {
        isTalk = true;    // ��ȭ���̶�� üũ
        talkPanel.SetActive(true);      // ��ȭ���� Ȱ��ȭ
        scanObject = scanObj;       // ��ȭ���� NPC�� ���ӿ�����Ʈ�� ����
        ObjData objData = scanObject.GetComponent<ObjData>();       // NPC�� ������Ʈ�� �����Ͽ� id�� isNpc������ ������
        Talk(objData.id, objData.isNpc);    // ��ȭ�Լ� ����
        talkPanel.SetActive(isTalk);    // ��ȭ������ üũ�Ͽ� ��ȭ���ڸ� �Ѱų� ��

    }

    // ��ȭ �Լ�
    void Talk(int id, bool isNpc)
    {
        string talkData = tm.GetTalk(id, talkIndex);    // NPC���� ��ȭ������ id �� index�� �����Ͽ� ������

        if (talkData == null)   // ������ ��ȭ������ �� ������ ��ȭ�� ����
        {
            isTalk = false;
            talkIndex = 0;
            return;
        }

        if (isNpc)  // ����� Npc�� �´ٸ� ��ȭ���ڿ� ��ȭ���� ���
        {
            talkText.text = talkData;
        }
        //else
        //{
        //    talkText.text = talkData;
        //}
        
        talkIndex++;    // ��ȭ���� �������� �ѱ�
    }
}
