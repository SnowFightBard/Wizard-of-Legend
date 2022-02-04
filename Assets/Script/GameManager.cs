using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{ 
    // UI 조정 및 여러 오브젝트가 참조해야하는 데이터를 담은 스크립트 //

    public List<SkillSpawn> data;  // 스킬 데이터 (정보,리소스 포함)
    public GameObject talkPanel;    // 대화창
    public GameObject inventory;    // 인벤토리 창
    public Text talkText;           // 대화 텍스트
    private GameObject scanObject;  // 접촉한 오브젝트
    public bool isTalk;             // 대화중인지 체크하는 변수
    public int talkIndex;           // 해당 NPC의 대화텍스트 INDEX
    TalkManager tm;


    private void Start()
    {
        tm = this.GetComponent<TalkManager>();

        // ScriptableObject 파일에서 가져온 스킬들을 index값을 기준으로 오름차순정렬
        data.Sort(delegate (SkillSpawn a, SkillSpawn b)
        {
            if (a.index > b.index) return 1;
            else if (a.index < b.index) return -1;
            return 0;
        }

        );
    }


    // 플레이어가 오브젝트 앞에서 F(액션버튼)를 눌렀을 때
    public void Action(GameObject scanObj)
    {
        isTalk = true;    // 대화중이라고 체크
        talkPanel.SetActive(true);      // 대화상자 활성화
        scanObject = scanObj;       // 대화중인 NPC를 게임오브젝트로 저장
        ObjData objData = scanObject.GetComponent<ObjData>();       // NPC의 컴포넌트에 접근하여 id와 isNpc정보를 가져옴
        Talk(objData.id, objData.isNpc);    // 대화함수 실행
        talkPanel.SetActive(isTalk);    // 대화중인지 체크하여 대화상자를 켜거나 끔

    }

    // 대화 함수
    void Talk(int id, bool isNpc)
    {
        string talkData = tm.GetTalk(id, talkIndex);    // NPC와의 대화내용을 id 와 index로 접근하여 가져옴

        if (talkData == null)   // 가져올 대화내용이 더 없으면 대화를 종료
        {
            isTalk = false;
            talkIndex = 0;
            return;
        }

        if (isNpc)  // 대상이 Npc가 맞다면 대화상자에 대화내용 출력
        {
            talkText.text = talkData;
        }
        //else
        //{
        //    talkText.text = talkData;
        //}
        
        talkIndex++;    // 대화내용 다음으로 넘김
    }
}
