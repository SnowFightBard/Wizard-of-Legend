using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject target; // ī�޶� ���� ���
    public float moveSpeed = 10.0f; // ī�޶� ���� �ӵ�
    private Vector3 targetPosition; // ����� ���� ��ġ
    
    void Start()
    {

    }
    
    void Update()
    {
        // ����� �ִ��� üũ
        if (target.gameObject != null)
        {
            // this�� ī�޶� �ǹ� (z���� ī�޶��� �״�� ����)
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);
            
            // MainCamera�� ��󿡰� moveSpeed �ӵ��� �̵�
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}