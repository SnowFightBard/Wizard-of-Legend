using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "ScriptableObject/Skill")]
public class Skill : ScriptableObject
{
    public string skillname;
    public float range;
    public float cooltime;
    public Sprite icon;

}
