using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 1.0f; // 플레이어 이동 속도
    float Vertical; // 위쪽, 아래쪽 방향값을 받는 변수
    float Horizontal; // 왼쪽, 오른쪽 방향값을 받는 변수

    public Animator animator; // Animator 속성 변수 생성

    public SpriteRenderer rend; // SpriteRenderer 속성 변수 생성

    private void Start()
    {
        animator = GetComponent<Animator>(); // animator 변수를 Player의 Animator 속성으로 초기화
    }

    private void FixedUpdate()
    {
        Move(); // 플레이어 이동

    }

    void Move()
    {


        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        

        if (Horizontal != 0)
        {
            animator.SetBool("walk_h", true);
            animator.SetBool("walk_down", false);
            animator.SetBool("walk_up", false);

            if (Horizontal > 0)
            {
                rend.flipX = false; // Player의 Sprite를 좌우반전시키는 함수 , true일 때 반전
            }
            else
            {
                rend.flipX = true;
            }
        }
        else
        {
            animator.SetBool("walk_h", false);
        }

        if (Vertical < 0)
        {
            animator.SetBool("walk_down", true);
            animator.SetBool("walk_up", false);
        }
        else if (Vertical > 0)
        {
            animator.SetBool("walk_up", true);
            animator.SetBool("walk_down", false);
        }
        else
        {
            animator.SetBool("walk_up", false);
            animator.SetBool("walk_down", false);
        }

        Vector3 dir = (Vertical * Vector3.up) + (Horizontal * Vector3.right); // transform.Translate() 변수의 자료형을 맞추기 위해 생성한 새로운 Vector3 변수 생성
        this.transform.Translate(dir * moveSpeed * Time.deltaTime); // Player 오브젝트 이동 함수
    }
}