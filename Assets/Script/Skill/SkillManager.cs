using System.Collections;
using System    .Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public List<Skill> data;  // 스킬 데이터 (정보,리소스 포함)

    PlayerMove pm;
    [SerializeField] GameObject Skill;

    public int index;
    public bool isSkill = false;
    public Vector2 skill_Des_Pos;

    private void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();

        // ScriptableObject 파일에서 가져온 스킬들을 index값을 기준으로 오름차순정렬
        data.Sort(delegate (Skill a, Skill b)
        {
            if (a.index > b.index) return 1;
            else if (a.index < b.index) return -1;
            return 0;
        }

        );
    }

    private void Update()
    {

        if (!pm.isDash && !pm.isTalk && !pm.isInventory && !isSkill)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (GameObject.Find("SkillSlot").GetComponent<SkillSlot>().equipSkill[0] != null)
                {
                    index = GameObject.Find("SkillSlot").GetComponent<SkillSlot>().equipSkill[0].index;
                    StartCoroutine("SkillSpawn");
                }

            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (GameObject.Find("SkillSlot").GetComponent<SkillSlot>().equipSkill[1] != null)
                {
                    index = GameObject.Find("SkillSlot").GetComponent<SkillSlot>().equipSkill[1].index;
                    StartCoroutine("SkillSpawn");
                }
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                if (GameObject.Find("SkillSlot").GetComponent<SkillSlot>().equipSkill[2] != null)
                {
                    index = GameObject.Find("SkillSlot").GetComponent<SkillSlot>().equipSkill[2].index;
                    StartCoroutine("SkillSpawn");
                }
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (GameObject.Find("SkillSlot").GetComponent<SkillSlot>().equipSkill[3] != null)
                {
                    index = GameObject.Find("SkillSlot").GetComponent<SkillSlot>().equipSkill[3].index;
                    StartCoroutine("SkillSpawn");
                }
            }
        }

        Debug.Log(index);

    }

    // 스킬 오브젝트 생성 함수
    IEnumerator SkillSpawn()
    {
        isSkill = true;

        Vector2 Player_pos = GameObject.Find("Player").transform.position;
        float angle = GameObject.Find("Mouse_Rot").transform.rotation.eulerAngles.z + 90;
        angle = angle / 360 * 2 * Mathf.PI;
        //Debug.Log("마우스 각도 : " + angle);

        pm.SkillAnimation(angle);

        Vector2 skill_Pos = new Vector2(Player_pos.x + Mathf.Cos(angle) * data[index].range, Player_pos.y + Mathf.Sin(angle) * data[index].range);   // 스킬 생성위치   // 스킬 생성위치
        skill_Des_Pos = new Vector2(Player_pos.x + Mathf.Cos(angle) * 100, Player_pos.y + Mathf.Sin(angle) * 100);      // 스킬이 향하는 곳


        if (data[index].type == 1 || data[index].type == 2) // 단발성 근접 , 원거리 스킬
        {
            Instantiate(Skill, skill_Pos, Quaternion.identity);
            yield return new WaitForSeconds(data[index].activeTime);
        }
        else if (data[index].type == 3)     // 원거리 연사 스킬 
        {
            // 직선으로 5번 생성됨
            for (int i = 1; i <= 5; i++)
            {
                skill_Pos = new Vector2(Player_pos.x + Mathf.Cos(angle) * (data[index].range + (i * 0.3f)), Player_pos.y + Mathf.Sin(angle) * (data[index].range + (i * 0.3f)));
                Instantiate(Skill, skill_Pos, Quaternion.identity);
                yield return new WaitForSeconds(data[index].activeTime * 0.2f);
            }
        }
        else if (data[index].type == 4)     // 원거리 부채꼴 스킬
        {
            // 부채꼴 모양으로 3개 발사
            for (int i = 0; i < 3; i++)
            {
                skill_Pos = new Vector2(Player_pos.x + Mathf.Cos(angle - 45 + (i * 45)) * data[index].range, Player_pos.y + Mathf.Sin(angle - 45 + (i * 45)) * data[index].range);
                //skill_Des_Pos = new Vector2(Player_pos.x + Mathf.Cos(angle - 45 + (i * 45)) * 100, Player_pos.y + Mathf.Sin(angle - 45 + (i * 45)) * 100);
                Instantiate(Skill, skill_Pos, Quaternion.identity);
            }
            yield return new WaitForSeconds(data[index].activeTime);

        }

        isSkill = false;
    }



}