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
    public List<Item> itemDB = new List<Item>();    // ��ų �� �������� ���� Item������ ����Ʈ

    public GameObject fieldItemPrefab;
    public Vector3[] pos;

    private void Start()
    {
        for(int i=0; i<5; i++)
        {
            GameObject go =  Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem(itemDB[Random.Range(0, 3)]);
        }
    }

}