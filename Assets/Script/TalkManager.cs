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
        talkData.Add(1, new string[] { "안녕.", "내가 누구냐고?", "몰?루" });
        talkData.Add(2, new string[] { "뭘 봐.", "내가 누구냐고?", "나도 모르니까 저리가" });

    }

    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }
}
