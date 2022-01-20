using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    private Dictionary<int,List<Sprite>> dic;

    [SerializeField]
    List<SkillSpawn> data;  // 스킬 데이터 (정보,리소스 포함)

    PlayerMove pm;

    public Animator player_ani;
    public Animator skill_ani;
    int index;
    public bool isSkill = false;

    private void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();
        player_ani = GameObject.Find("Player").GetComponent<Animator>();
        skill_ani = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if (pm.isDash == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                index = 0;
                if (!isSkill)
                    StartCoroutine(Skill());
            }
        }
    }


    IEnumerator Skill()
    {
        isSkill = true;

        skill_ani.Play(data[index].name);
        pm.ChangeAnimationState("Right_Attack");

        yield return new WaitForSeconds(data[index].activetime);

        skill_ani.Play("Idle");

        isSkill = false;

    }


}