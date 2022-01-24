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
    GameManager Manager;

    public Animator player_ani;
    public Animator skill_ani;
    int index;
    public bool isSkill = false;

    private void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();
        player_ani = GameObject.Find("Player").GetComponent<Animator>();
        skill_ani = this.GetComponent<Animator>();
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // ScriptableObject 파일에서 가져온 스킬들을 index값을 기준으로 오름차순정렬
        data.Sort(delegate (SkillSpawn a, SkillSpawn b)
        {
            if (a.index > b.index) return 1;
            else if (a.index < b.index) return -1;
            return 0;
        }

        );

    }

    private void Update()
    {
        if (pm.isDash == false && Manager.isTalk == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                index = 1;
                if (!isSkill)
                {
                    if(data[index].isRot)
                        this.GetComponent<Skill_LookAt>().Skill_Look(data[index]);
                    StartCoroutine(Skill());
                }
            }
        }
    }


    IEnumerator Skill()
    {
        isSkill = true;

        skill_ani.Play(data[index].name);
        Debug.Log("Player는 " + data[index].skillName + "을(를) 사용하였다!");

        pm.ChangeAnimationState("Right_Attack");

        //if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y > 0 && Camera.main.ScreenToWorldPoint(Input.mousePosition).x > 0)
        //{
        //    pm.ChangeAnimationState("Right_Attack");
        //}
        //else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < 0 && Camera.main.ScreenToWorldPoint(Input.mousePosition).x < 0)

        yield return new WaitForSeconds(data[index].activeTime);

        skill_ani.Play("Idle");

        isSkill = false;

    }


}