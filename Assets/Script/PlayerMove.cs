using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    GameObject Text_Button;

    GameObject Button;

    public Animator animator; // Animator 속성 변수 생성
    public SpriteRenderer rend; // SpriteRenderer 속성 변수 생성
    private Vector2 vector;
    private Rigidbody2D rigidBody;
    
    float baseSpeed = 1.5f;     // 기본 이동 속도
    public float Speed = 1.5f;      // 현재 플레이어 이동 속도
    public float dashSpeed;         // 대쉬 속도
    private float dashTime = 0.3f;        // 대쉬 유지시간
    private bool isDash;        // 대쉬중인지 체크하는 변수
    float Vertical;      // 위쪽, 아래쪽 입력을 받는 변수
    float Horizontal;       // 왼쪽, 오른쪽 입력을 받는 변수


    bool Talk = false;  // 대화 가능한지 체크하는 변수
    bool isTalk = false;    // 대화중인지 체크하는 변수

    private void Start()
    {
        animator = GetComponent<Animator>(); // animator 변수를 Player의 Animator 속성으로 초기화
        rigidBody = GetComponent<Rigidbody2D>();
    }
    
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isDash)
                StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        Move(); // 플레이어 이동
    }


    void Move()
    {
        vector.y = Input.GetAxis("Vertical");
        vector.x = Input.GetAxis("Horizontal");

        // 가로 이동 애니메이션 동작
        if (vector.x != 0)
        {
            animator.SetBool("walk_h", true);
            animator.SetBool("walk_down", false);
            animator.SetBool("walk_up", false);

            if (vector.x > 0)
            {
                rend.flipX = false; // Player의 Sprite를 좌우반전시키는 함수 , true일 때 반전
            }
            else
            {
                rend.flipX = true;
            }
        }
        else if (vector.y < 0)  // 세로 방향 애니메이션 동작
        {
            animator.SetBool("walk_down", true);
            animator.SetBool("walk_up", false);
            animator.SetBool("walk_h", false);
        }
        else if (vector.y > 0)
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

        // 플레이어 이동처리
        rigidBody.velocity = vector.normalized * Speed;
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

    // 대쉬 기능
    IEnumerator Dash()
    {
        isDash = true;
        animator.SetBool("right_dash", true);
        Speed = dashSpeed;

        yield return new WaitForSeconds(dashTime);

        Speed = baseSpeed;
        isDash = false;
        animator.SetBool("right_dash", false);
    }
}