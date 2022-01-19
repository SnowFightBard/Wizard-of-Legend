using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "ScriptableObject/Skill")]
public class SkillSpawn : ScriptableObject
{
    public int index;   // 스킬 고유 인덱스번호
    public string skillname;    // 스킬 이름
    public float range; // 스킬 사거리
    public float cooltime;  // 스킬 쿨타임
    public float activetime; // 시전 시간
    public AnimationClip ani; // 스킬 애니메이션

}
