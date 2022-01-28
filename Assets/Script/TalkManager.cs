using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        TalkAdd();
    }


    void Update()
    {
        
    }


    public void TalkAdd()
    {
        talkData.Add(1, new string[] { "�ȳ�.", "���� �����İ�?", "��?��" });
        talkData.Add(2, new string[] { "�� ��.", "���� �����İ�?", "���� �𸣴ϱ� ������" });

    }

    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }
}
