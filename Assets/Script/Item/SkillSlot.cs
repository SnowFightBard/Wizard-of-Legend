using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    [SerializeField] GameObject[] InvenSkill_Image; // �κ��丮â�� ���̴� �������� ��ų �̹���
    [SerializeField] GameObject[] EquipSkill_Image; // �������� ��ų �̹���

    public int selectItem;
    bool isChange = false;

    public Skill[] equipSkill = new Skill[4];
    int a = -1, b = -1;

    int index = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameObject.Find("Player").GetComponent<PlayerMove>().isInventory)
        {
            if (!isChange)
                StartCoroutine(Change());
        }

        Debug.Log("a : " + a + " b : " + b);
        Debug.Log(isChange);
    }

    public void Equip(Skill skill)
    {
        InvenSkill_Image[index].GetComponent<Image>().color = new Color(255, 255, 255, 255);
        InvenSkill_Image[index].GetComponent<Image>().sprite = skill.image;
        EquipSkill_Image[index].GetComponent<Image>().color = new Color(255, 255, 255, 255);
        EquipSkill_Image[index].GetComponent<Image>().sprite = skill.image;

        equipSkill[index++] = skill;

        if (index == 4)
            index = 0;

    }


    IEnumerator Change()
    {
        isChange = true;

        if (a == -1)
        {
            a = selectItem;
        }
        else
        {
            b = selectItem;

            if (equipSkill[a] == null && equipSkill[b] != null)
            {
                equipSkill[a] = equipSkill[b];
                equipSkill[b] = null;
            }
            else if (equipSkill[a] != null && equipSkill[b] == null)
            {
                equipSkill[b] = equipSkill[a];
                equipSkill[a] = null;
            }
            else
            {
                Skill temp = equipSkill[a];
                equipSkill[a] = equipSkill[b];
                equipSkill[b] = temp;
            }

            a = -1;
            b = -1;

            Refresh();
        }
        
        isChange = false;
        Debug.Log("������?");

        yield return new WaitForSeconds(0.5f);
    }


    private void Refresh()
    {
        for(int i=0; i<4; i++)
        {
            if (equipSkill[i] != null)
            {
                InvenSkill_Image[i].GetComponent<Image>().sprite = equipSkill[i].image;
                EquipSkill_Image[i].GetComponent<Image>().sprite = equipSkill[i].image;
                InvenSkill_Image[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                EquipSkill_Image[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            }
            else
            {
                InvenSkill_Image[i].GetComponent<Image>().sprite = null;
                EquipSkill_Image[i].GetComponent<Image>().sprite = null;
                InvenSkill_Image[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
                EquipSkill_Image[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
            }
        }
    }


}
