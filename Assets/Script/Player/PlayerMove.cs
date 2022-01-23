using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    GameObject Text_Button;

    GameObject Button;
    GameManager Manager;

    public Animator animator; // Animator 속성 변수 생성
    public SpriteRenderer rend; // SpriteRenderer 속성 변수 생성
    private Vector2 vector;
    private Rigidbody2D rigidBody;
    
    //=========================
    //   플레이어 이동관련 변수
    //=========================
    float baseSpeed = 1.5f;     // 기본 이동 속도
    public float Speed = 1.5f;      // 현재 플레이어 이동 속도
    public float dashSpeed;         // 대쉬 속도
    private float dashTime = 0.45f;        // 대쉬 유지시간
    public bool isDash;        // 대쉬중인지 체크하는 변수
    float Vertical;      // 위쪽, 아래쪽 입력을 받는 변수
    float Horizontal;       // 왼쪽, 오른쪽 입력을 받는 변수

    //==============================
    //   플레이어 애니메이션관련 변수
    //==============================
    int moveRot = 2;    // 마지막으로 이동한 방향
    const string PLAYER_IDLE_DOWN = "Player_Idle_Down";
    const string PLAYER_IDLE_UP = "Player_Idle_Up";
    const string PLAYER_IDLE_RIGHT = "Player_Idle_Right";
    const string PLAYER_WALK_DOWN = "Player_Walk_Down";
    const string PLAYER_WALK_UP = "Player_Walk_Up";
    const string PLAYER_WALK_RIGHT = "Player_Walk_Right";
    const string DASH_RIGHT = "Dash_Right";
    const string DASH_UP = "Dash_Up";
    const string DASH_DOWN = "Dash_Down";
    const string ATTACK = "Attack";



    private GameObject talkNpc;
    private string currentState;    // 현재 동작중인 애니메이션



    bool Talk = false;  // 대화 가능한지 체크하는 변수
    bool isTalk = false;    // 대화중인지 체크하는 변수

    private void Start()
    {
        animator = GetComponent<Animator>(); // animator 변수를 Player의 Animator 속성으로 초기화
        rigidBody = GetComponent<Rigidbody2D>();
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    // 키 입력 내용을 모아둠
    private void Update()
    {
        if (!isDash && Manager.isAction == false)
        {
            vector.y = Input.GetAxisRaw("Vertical");
            vector.x = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space) && (vector.x != 0 || vector.y != 0))
            {
                StartCoroutine(Dash());
            }
        }

        if (GameObject.Find("Attack").GetComponent<SkillManager>().isSkill == true)
        {
            vector = Vector2.zero;
        }

        if (Input.GetKeyDown(KeyCode.F) && talkNpc!=null)
        {
            Manager.action(talkNpc);
        }
    }

    private void FixedUpdate()
    {
        Move(); // 플레이어 이동
    }


    public void Move()
    {
        if (!isDash && GameObject.Find("Attack").GetComponent<SkillManager>().isSkill == false)
        {
            // 가로 이동 애니메이션 동작
            if (vector.x != 0)
            {
                ChangeAnimationState(PLAYER_WALK_RIGHT);
                moveRot = 1;
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
                ChangeAnimationState(PLAYER_WALK_DOWN);
                moveRot = 2;
            }
            else if (vector.y > 0)
            {
                ChangeAnimationState(PLAYER_WALK_UP);
                moveRot = 3;
            }
            else  // 이동중이 아닐때 기본자세 애니메이션 동작
            {
                switch (moveRot)
                {
                    case 1:
                        ChangeAnimationState(PLAYER_IDLE_RIGHT);
                        break;
                    case 2:
                        ChangeAnimationState(PLAYER_IDLE_DOWN);
                        break;
                    case 3:
                        ChangeAnimationState(PLAYER_IDLE_UP);
                        break;
                }
            }
        }


        // 플레이어 이동처리
        rigidBody.velocity = vector.normalized * Speed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 부딪힌 오브젝트가 NPC일 경우 NPC머리위에 대화버튼 활성화
        if (collision.gameObject.tag == "Npc")
        {
            talkNpc = collision.gameObject;
            Vector3 pos = collision.gameObject.transform.position;      // NPC의 위치
            pos = new Vector3(pos.x, pos.y + 0.2f, pos.z);          // pos에 NPC 머리위 위치값 저장 
            Button = Instantiate(Text_Button, pos, Quaternion.identity);    // NPC머리위에 대화버튼 생성
            
        }
        Debug.Log(collision.collider.name);
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        // NPC와 멀어지면 대화버튼 제거
        Destroy(Button);
        talkNpc = null;
    }

    public void ChangeAnimationState(string newState)
    {
        // 현재 애니메이션과 교체될 애니메이션이 같으면 실행하지 않음
        if (currentState == newState) return;

        // 교체된 애니메이션 실행
        animator.Play(newState);

        // 현재 상태를 교체된 애니메이션으로 변경
        currentState = newState;
    }

    // 대쉬 기능
    IEnumerator Dash()
    {
        isDash = true;
        if (moveRot == 1)
            ChangeAnimationState(DASH_RIGHT);
        else if (moveRot == 2)
            ChangeAnimationState(DASH_DOWN);
        else
            ChangeAnimationState(DASH_UP);
        Speed = dashSpeed;

        yield return new WaitForSeconds(dashTime);

        Speed = baseSpeed;
        isDash = false;

    }
}