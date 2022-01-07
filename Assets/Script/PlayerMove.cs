using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 1.5f; // 플레이어 이동 속도
    float Vertical; // 위쪽, 아래쪽 입력을 받는 변수
    float Horizontal; // 왼쪽, 오른쪽 입력을 받는 변수
    [SerializeField]
    GameObject Text_Button;

    GameObject Button;

    public Animator animator; // Animator 속성 변수 생성

    public SpriteRenderer rend; // SpriteRenderer 속성 변수 생성

    bool Talk = false;  // 대화 가능한지 체크하는 변수
    bool isTalk = false;    // 대화중인지 체크하는 변수

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
        
        // 가로 이동 애니메이션 동작
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
        else if (Vertical < 0)  // 세로 방향 애니메이션 동작
        {
            animator.SetBool("walk_down", true);
            animator.SetBool("walk_up", false);
            animator.SetBool("walk_h", false);
        }
        else if (Vertical > 0)
        {
            animator.SetBool("walk_up", true);
            animator.SetBool("walk_down", false);
            animator.SetBool("walk_h", false);
        }
        else // 이동중이 아닐때 기본자세 애니메이션 동작
        {
            animator.SetBool("walk_up", false);
            animator.SetBool("walk_down", false);
            animator.SetBool("walk_h", false);
        }

        Vector3 dir = (Vertical * Vector3.up) + (Horizontal * Vector3.right);
        this.transform.Translate(dir * moveSpeed * Time.deltaTime); // Player 오브젝트 이동 함수
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 부딪힌 오브젝트가 NPC일 경우 NPC머리위에 대화버튼 활성화
        if (collision.gameObject.tag == "Npc")
        {
            Vector3 pos = collision.gameObject.transform.position;
            pos = new Vector3(pos.x, pos.y + 0.2f, pos.z);
            Button = Instantiate(Text_Button, pos, Quaternion.identity);
        }
        Debug.Log(collision.collider.name);
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        // NPC와 멀어지면 대화버튼 제거
        Destroy(Button);
    }
}