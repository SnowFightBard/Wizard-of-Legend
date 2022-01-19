using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    private Dictionary<int,List<Sprite>> dic;

    [SerializeField]
    List<SkillSpawn> data;  // 스킬 데이터 (정보,리소스 포함)

    public Animator am;
    int index;
    bool isSkill = false;

    private void Start()
    {
        am = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            index = 0;
            StartCoroutine(Skill());
        }
    }


    IEnumerator Skill()
    {
        isSkill = true;
        am.Play(data[index].name);
        yield return new WaitForSeconds(data[index].activetime);
        am.Play("Idle");


        isSkill = false;

    }


}