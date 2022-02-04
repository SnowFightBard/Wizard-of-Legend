using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData; // 대화내용을 저장할 딕셔너리

    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        TalkAdd();  // 최초 실행시 대화내용 추가
    }
    


    /*
    0 : 새 A
    1 : 새 B
    2 : 나무 A
    */

    // 이곳에 대화 내용을 작성
    public void TalkAdd()
    {
        talkData.Add(0, new string[] { "안녕.>내가 누구냐고?>몰?루" });
        talkData.Add(1, new string[] {"뭘 봐.>내가 누구냐고?>나도 모르니까 저리가"});
        talkData.Add(2, new string[] { "난 나무야.>......>왜 그런눈으로 보는거야?>..........>제발 자르지 말아줘..." });

        // 대화내용은 '>' 를 기준으로 잘라서 여러개의 문자열로 나누어 줌
        for (int i = 0; i < talkData.Count; i++)
            talkData[i] = talkData[i][0].Split('>');

    }

    // 대화 내용을 반환해주는 함수
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
