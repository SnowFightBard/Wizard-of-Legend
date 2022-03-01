using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelect : MonoBehaviour
{
    [SerializeField] GameObject[] Buttons;
    public void Select()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (Buttons[i].name == this.name)
                GameObject.Find("SkillSlot").GetComponent<SkillSlot>().selectItem = i;
            Buttons[i].GetComponent<Outline>().effectColor = Color.black;
        }

        this.GetComponent<Outline>().effectColor = Color.red;
        
    }
}
