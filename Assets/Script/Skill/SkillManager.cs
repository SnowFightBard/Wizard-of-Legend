using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    private Dictionary<int,List<Sprite>> dic;

    [SerializeField]
    List<SkillSpawn> data;  // ��ų ������ (����,���ҽ� ����)

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

        // ScriptableObject ���Ͽ��� ������ ��ų���� index���� �������� ������������
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
        if (pm.isDash == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                index = 0;
                if (!isSkill)
                {
                    this.GetComponent<Skill_LookAt>().Skill_Look(data[index].rot, data[index].range);
                    StartCoroutine(Skill());
                }
            }
        }
    }


    IEnumerator Skill()
    {
        isSkill = true;

        skill_ani.Play(data[index].name);
        Debug.Log("Player�� " + data[index].skillname + "��(��) ����Ͽ���!");

        pm.ChangeAnimationState("Right_Attack");

        //if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y > 0 && Camera.main.ScreenToWorldPoint(Input.mousePosition).x > 0)
        //{
        //    pm.ChangeAnimationState("Right_Attack");
        //}
        //else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < 0 && Camera.main.ScreenToWorldPoint(Input.mousePosition).x < 0)

        yield return new WaitForSeconds(data[index].activetime);

        skill_ani.Play("Idle");

        isSkill = false;

    }


}