using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{

    // 스킬의 피격처리 스크립트
    
    PlayerMove pm;
    GameManager Manager;

    public Animator player_ani;
    public Animator skill_ani;


    private void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();
        player_ani = GameObject.Find("Player").GetComponent<Animator>();
        skill_ani = this.GetComponent<Animator>();
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // 스킬의 방향을 보정함
        if (Manager.data[pm.index].isRot)
            this.GetComponent<Skill_LookAt>().Skill_Look(Manager.data[pm.index]);

        // 스킬 발동
        StartCoroutine(Skill());

    }

    private void Update()
    {
        
    }

    // 스킬 충돌처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("데미지를 이펴따 Trigger : " + collision.name);
    }


    // 스킬 사용 코루틴
    IEnumerator Skill()
    {
        pm.isSkill = true;

        skill_ani.Play(Manager.data[pm.index].name);
        Debug.Log("Player는 " + Manager.data[pm.index].skillName + "을(를) 사용하였다!");

        pm.ChangeAnimationState("Right_Attack");    // 스킬을 사용할때 플레이어의 애니메이션 교체

        //if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y > 0 && Camera.main.ScreenToWorldPoint(Input.mousePosition).x > 0)
        //{
        //    pm.ChangeAnimationState("Right_Attack");
        //}
        //else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < 0 && Camera.main.ScreenToWorldPoint(Input.mousePosition).x < 0)

        yield return new WaitForSeconds(Manager.data[pm.index].activeTime);    // 스킬 지속시간만큼 지연됨
        
        Destroy(this.gameObject);

        pm.isSkill = false;

    }


}