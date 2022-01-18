using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "ScriptableObject/Skill")]
public class SkillManager : ScriptableObject
{
    public int index;
    public string skillname;
    public float range;
    public float cooltime;
    public Sprite icon;

}
