using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    [SerializeField] GameObject[] skillSlot;
    Skill[] equipSkill = new Skill[4];
    int index = 0;

    void Awake()
    {

    }

    public void Equip(Skill skill)
    {
        skillSlot[index].GetComponent<Image>().color = new Color(255, 255, 255, 255);
        skillSlot[index].GetComponent<Image>().sprite = skill.image;

        equipSkill[index++] = skill;

        if (index == 4)
            index = 0;
    }



}
