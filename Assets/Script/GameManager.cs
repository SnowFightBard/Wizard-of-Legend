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
        // 마우스 커서의 좌표값을 mouse변수로 받음
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mouse);
    }
}
