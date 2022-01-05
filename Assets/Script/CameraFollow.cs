using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject target; // 카메라가 따라갈 대상
    public float moveSpeed = 10.0f; // 카메라가 따라갈 속도
    private Vector3 targetPosition; // 대상의 현재 위치
    
    void Start()
    {

    }
    
    void Update()
    {
        // 대상이 있는지 체크
        if (target.gameObject != null)
        {
            // this는 카메라를 의미 (z값은 카메라값을 그대로 유지)
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);
            
            // MainCamera가 대상에게 moveSpeed 속도로 이동
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}