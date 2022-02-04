using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData; // ��ȭ������ ������ ��ųʸ�

    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        TalkAdd();  // ���� ����� ��ȭ���� �߰�
    }
    


    /*
    0 : �� A
    1 : �� B
    2 : ���� A
    */

    // �̰��� ��ȭ ������ �ۼ�
    public void TalkAdd()
    {
        talkData.Add(0, new string[] { "�ȳ�.>���� �����İ�?>��?��" });
        talkData.Add(1, new string[] {"�� ��.>���� �����İ�?>���� �𸣴ϱ� ������"});
        talkData.Add(2, new string[] { "�� ������.>......>�� �׷������� ���°ž�?>..........>���� �ڸ��� ������..." });

        // ��ȭ������ '>' �� �������� �߶� �������� ���ڿ��� ������ ��
        for (int i = 0; i < talkData.Count; i++)
            talkData[i] = talkData[i][0].Split('>');

    }

    // ��ȭ ������ ��ȯ���ִ� �Լ�
    public string GetTalk(int id, int talkIndex)
    {

        if (talkIndex == talkData[id].Length)
            return null;
        else
        {
            
            return talkData[id][talkIndex];
        }
    }
}
