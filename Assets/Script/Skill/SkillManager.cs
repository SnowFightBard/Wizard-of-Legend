using System.Collections;
using System    .Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{

    // ��ų�� �ǰ�ó�� ��ũ��Ʈ //
    
    PlayerMove pm;
    GameManager Manager;

    // �÷��̾�� ��ų�� �ִϸ��̼��� �����ϱ����� ����
    public Animator player_ani;
    public Animator skill_ani;

    Skill data;    // �� ��ų�� ������
    Vector2 des_Pos;    // �� ��ų�� �ı�����

    private void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();
        player_ani = GameObject.Find("Player").GetComponent<Animator>();
        skill_ani = this.GetComponent<Animator>();
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        des_Pos = pm.skill_Des_Pos;
        data = Manager.data[pm.index];

        // ��ų�� ������ ������
        if (data.isRot)
            this.GetComponent<Skill_LookAt>().Skill_Look(data);

        // ��ų �ߵ�
        StartCoroutine(Skill());

    }

    private void Update()
    {
        if(data.type == 2 || data.type == 4)
        {
            
            transform.position = Vector2.Lerp(transform.position, des_Pos, 0.0001f);
            
        }
    }

    // ��ų �浹ó��
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().hp -= data.damage;
            Debug.Log("�������� ����� Trigger : " + collision.name + "�� ü�� - " + data.damage);
        }

        if (data.type == 2)
        {
            Destroy(this.gameObject);
        }

    }


    // ��ų ��� �ڷ�ƾ
    IEnumerator Skill()
    {

        skill_ani.Play(data.name);
        Debug.Log("Player�� " + data.skillName + "��(��) ����Ͽ���!");


        // Ÿ��2�� �ƴ� ��ų���� activeTime�� ������ �����. (Ÿ��2�� �浹�Ҷ����� ���ư�)
        if (data.type != 2)
        {
            yield return new WaitForSeconds(data.activeTime);    // ��ų ���ӽð���ŭ ������
            Destroy(this.gameObject);
        }
    }


}