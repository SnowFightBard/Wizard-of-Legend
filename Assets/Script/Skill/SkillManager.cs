using System.Collections;
using System    .Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{

    // 스킬의 피격처리 스크립트 //
    
    PlayerMove pm;
    GameManager Manager;

    // 플레이어와 스킬의 애니메이션을 제어하기위한 변수
    public Animator player_ani;
    public Animator skill_ani;

    Skill data;    // 이 스킬의 데이터
    Vector2 des_Pos;    // 이 스킬의 파괴지점

    private void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();
        player_ani = GameObject.Find("Player").GetComponent<Animator>();
        skill_ani = this.GetComponent<Animator>();
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        des_Pos = pm.skill_Des_Pos;
        data = Manager.data[pm.index];

        // 스킬의 방향을 보정함
        if (data.isRot)
            this.GetComponent<Skill_LookAt>().Skill_Look(data);

        // 스킬 발동
        StartCoroutine(Skill());

    }

    private void Update()
    {
        if(data.type == 2 || data.type == 4)
        {
            
            transform.position = Vector2.Lerp(transform.position, des_Pos, 0.0001f);
            
        }
    }

    // 스킬 충돌처리
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().hp -= data.damage;
            Debug.Log("데미지를 이펴따 Trigger : " + collision.name + "의 체력 - " + data.damage);
        }

        if (data.type == 2)
        {
            Destroy(this.gameObject);
        }

    }


    // 스킬 사용 코루틴
    IEnumerator Skill()
    {

        skill_ani.Play(data.name);
        Debug.Log("Player는 " + data.skillName + "을(를) 사용하였다!");


        // 타입2가 아닌 스킬들은 activeTime이 지나면 사라짐. (타입2는 충돌할때까지 날아감)
        if (data.type != 2)
        {
            yield return new WaitForSeconds(data.activeTime);    // 스킬 지속시간만큼 지연됨
            Destroy(this.gameObject);
        }
    }


}