using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "ScriptableObject/Skill")]
public class Skill : ScriptableObject
{
    // ��ų ���� ������ �����س��� ��ũ��Ʈ //

    public int index;   // ��ų ���� �ε�����ȣ
    public int type;    // 1 : �ܹ߼� �������� , 2 : ������ ���Ÿ� ��ų , 3 : ������ ���Ÿ� ���罺ų(5��) , 4 : ��ä�� �л��� ��ų (1��)
    public string skillName;    // ��ų �̸�
    public float damage;    // ������
    public float range; // ��ų ��Ÿ�
    public float coolTime;  // ��ų ��Ÿ��
    public float activeTime; // ���� �ð�
    public bool isRot; // ��ų�� ���콺 ��ġ������ ȸ����ų�� �����ϴ� ����
    [TextArea] public string info; // ��ų ����
    public Sprite image; // ��ų �̹���

    /*
     
     0 : ���� �ٶ�����
     1 : ���̽� �ε�
     2 : ������ ��

     */

}