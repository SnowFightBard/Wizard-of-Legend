using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        // ���콺 Ŀ���� ��ǥ���� mouse������ ����
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mouse);
    }
}
