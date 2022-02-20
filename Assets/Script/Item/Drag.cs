using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{

    private Vector3 m_Offset;
    private float m_ZCoord;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_ZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            m_Offset = gameObject.transform.position - GetMouseWorldPosition();
            Debug.Log("ÁÂÅ¬¸¯");
        }
    }


    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + m_Offset;
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = m_ZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

}