using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    private Dictionary<int,List<Sprite>> dic;

    [SerializeField]
    List<SkillSpawn> data;  // ��ų ������ (����,���ҽ� ����)

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