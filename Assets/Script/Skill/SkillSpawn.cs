using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "ScriptableObject/Skill")]
public class SkillSpawn : ScriptableObject
{
    public int index;   // ��ų ���� �ε�����ȣ
    public string skillname;    // ��ų �̸�
    public float range; // ��ų ��Ÿ�
    public float cooltime;  // ��ų ��Ÿ��
    public float activetime; // ���� �ð�
    public AnimationClip ani; // ��ų �ִϸ��̼�

}
