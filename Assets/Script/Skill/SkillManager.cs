using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{

    // ��ų�� �ǰ�ó�� ��ũ��Ʈ
    
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

        // ��ų�� ������ ������
        if (Manager.data[pm.index].isRot)
            this.GetComponent<Skill_LookAt>().Skill_Look(Manager.data[pm.index]);

        // ��ų �ߵ�
        StartCoroutine(Skill());

    }

    private void Update()
    {
        
    }

    // ��ų �浹ó��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("�������� �����");
    }


    // ��ų ��� �ڷ�ƾ
    IEnumerator Skill()
    {
        pm.isSkill = true;

        skill_ani.Play(Manager.data[pm.index].name);
        Debug.Log("Player�� " + Manager.data[pm.index].skillName + "��(��) ����Ͽ���!");

        pm.ChangeAnimationState("Right_Attack");    // ��ų�� ����Ҷ� �÷��̾��� �ִϸ��̼� ��ü

        //if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y > 0 && Camera.main.ScreenToWorldPoint(Input.mousePosition).x > 0)
        //{
        //    pm.ChangeAnimationState("Right_Attack");
        //}
        //else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < 0 && Camera.main.ScreenToWorldPoint(Input.mousePosition).x < 0)

        yield return new WaitForSeconds(Manager.data[pm.index].activeTime);    // ��ų ���ӽð���ŭ ������
        
        Destroy(this.gameObject);

        pm.isSkill = false;

    }


}