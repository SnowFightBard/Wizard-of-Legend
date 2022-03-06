using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    [SerializeField] GameObject[] InvenSkill_Image; // 인벤토리창에 보이는 장착중인 스킬 이미지
    [SerializeField] GameObject[] EquipSkill_Image; // 장착중인 스킬 이미지
    [SerializeField] GameObject Info; // 스킬 정보


    public int selectItem; // 좌클릭중인 아이템
    bool isChange = false; // 스킬 교체중인지 확인하는 변수

    public Skill[] equipSkill = new Skill[4];   // 장착중인 스킬 오브젝트
    int a = -1, b = -1; // 선택한 스킬 Index

    int index;

    private void Update()
    {
        // 스페이스바 입력시 교체 진행
        if (Input.GetKeyDown(KeyCode.Space) && GameObject.Find("Player").GetComponent<PlayerMove>().isInventory)
        {
            if (!isChange)
                StartCoroutine(Change());
        }

    }

    public void PrintInfo()
    {
        if (equipSkill[selectItem] != null)
        {
            Info.SetActive(true);
            Info.GetComponent<Text>().text = equipSkill[selectItem].info;
        }
        else
            Info.SetActive(false);
    }

    // 스킬 장착
    public void Equip(Skill skill)
    {

        for(int i=0; i<4; i++)
        {
            if (equipSkill[i] == null)
            {
                index = i;
                break;
            }
        }

        InvenSkill_Image[index].GetComponent<Image>().color = new Color(255, 255, 255, 255);
        InvenSkill_Image[index].GetComponent<Image>().sprite = skill.image;
        EquipSkill_Image[index].GetComponent<Image>().color = new Color(255, 255, 255, 255);
        EquipSkill_Image[index].GetComponent<Image>().sprite = skill.image;

        equipSkill[index] = skill;

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
