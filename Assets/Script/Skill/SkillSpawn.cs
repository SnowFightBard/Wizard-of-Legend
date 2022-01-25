using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "ScriptableObject/Skill")]
public class SkillSpawn : ScriptableObject
{
    // ��ų ���� ������ �����س��� ��ũ��Ʈ

    public int index;   // ��ų ���� �ε�����ȣ
    public string skillName;    // ��ų �̸�
    public float range; // ��ų ��Ÿ�
    public float coolTime;  // ��ų ��Ÿ��
    public float activeTime; // ���� �ð�
    public int count;   // �ߵ� Ƚ��
    public bool isRot; // ��ų�� ���콺 ��ġ������ ȸ����ų�� �����ϴ� ����
    public int rot; // ��ų �ߵ��� ȸ����ų ���� (������ �ȸ´� �̹����� ����������)

}