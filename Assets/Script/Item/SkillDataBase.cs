using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataBase : MonoBehaviour
{
    public static SkillDataBase Instance;
    private void Awake()
    {
        Instance = this;
    }
    public List<Skill> itemDB = new List<Skill>();    // 스킬 및 아이템을 담을 Item형식의 리스트

    public GameObject fieldItemPrefab;
    public Vector3[] pos;

    private void Start()
    {
        for(int i=0; i<5; i++)
        {
            GameObject go = Instantiate(fieldItemPrefab, new Vector3(0.2f + (i * 0.5f), -2.2f, 0), Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem(itemDB[Random.Range(0, 3)]);
        }
    }

}