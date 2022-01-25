using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{ 
    // UI 조정 및 여러 오브젝트가 참조해야하는 데이터를 담은 스크립트

    public List<SkillSpawn> data;  // 스킬 데이터 (정보,리소스 포함)
    public GameObject talkPanel;
    public Text talkText;
    private GameObject scanObject;
    public bool isTalk;


    private void Start()
    {
        // ScriptableObject 파일에서 가져온 스킬들을 index값을 기준으로 오름차순정렬
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
            talkText.text = "이것의 이름은 " + scanObject.name + " 입니다.";
        }

        
    }
}
