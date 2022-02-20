using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 플레이어 조작에 관련된 스크립트 //

    [SerializeField] GameObject Text_Button;

    GameObject Button;
    SkillManager SkillManager;
    GameManager GameManager;
    public Animator animator; // Animator 속성 변수 생성
    public SpriteRenderer rend; // SpriteRenderer 속성 변수 생성
    private Vector2 vector;
    private Rigidbody2D rigidBody;



    //=========================
    //   플레이어 행동관련 변수
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
    int moveRot = 3;    // 마지막으로 이동한 방향
    const string PLAYER_IDLE_DOWN = "Player_Idle_Down";
    const string PLAYER_IDLE_UP = "Player_Idle_Up";
    const string PLAYER_IDLE_LEFT = "Left_Idle_Right";
    const string PLAYER_IDLE_RIGHT = "Player_Idle_Right";
    const string PLAYER_WALK_DOWN = "Player_Walk_Down";
    const string PLAYER_WALK_UP = "Player_Walk_Up";
    const string PLAYER_WALK_RIGHT = "Player_Walk_Right";
    const string DASH_RIGHT = "Dash_Right";
    const string DASH_UP = "Dash_Up";
    const string DASH_DOWN = "Dash_Down";
    const string RIGHT_ATTACK = "Right_Attack";
    const string DOWN_ATTACK = "Down_Attack";
    const string UP_ATTACK = "Up_Attack";




    public GameObject talkNpc;
    private string currentState;    // 현재 동작중인 애니메이션



    bool Talk = false;  // 대화 가능한지 체크하는 변수
    public bool isTalk = false;    // 대화중인지 체크하는 변수
    public bool isInventory = false;

    private void Start()
    {
        animator = GetComponent<Animator>(); // animator 변수를 Player의 Animator 속성으로 초기화
        rigidBody = GetComponent<Rigidbody2D>();
        SkillManager = GameObject.Find("SkillManager").GetComponent<SkillManager>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    // 키 입력 내용을 모아둠
    private void Update()
    {
        if (!isDash && !SkillManager.isSkill && GameManager.isTalk == false)
        {
            vector.y = Input.GetAxisRaw("Vertical");
            vector.x = Input.GetAxisRaw("Horizontal");


            // 대쉬기능
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // 움직이지 않고 제자리에 있어도 대쉬할수있도록 vector값을 moveRot방향으로 바꿔줌
                if (moveRot == 1)
                    vector.x = 1;
                else if (moveRot == 2)
                    vector.x = -1;
                else if (moveRot == 3)
                    vector.y = -1;
                else
                    vector.y = 1;

                // 대쉬 코루틴 실행
                StartCoroutine(Dash());
            }
        }

        // F를 누를시 대화
        if (Input.GetKeyDown(KeyCode.F))
        {
            vector = new Vector2(0, 0);
            if (talkNpc != null)
                GameManager.Action(talkNpc);
            GameManager.inventory.SetActive(false);
            isInventory = false;
        }

        // I를 누를시 인벤토리창 ON OFF
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventory = !isInventory;
            GameManager.inventory.SetActive(isInventory);
        }

    }

    private void FixedUpdate()
    {
        // 스킬사용중에는 움직일 수 없음
        if (SkillManager.isSkill == true)
        {
            vector = Vector2.zero;
        }

        Move(); // 플레이어 이동
    }


    // 스킬 사용시 애니메이션을 출력하는 함수
    public void SkillAnimation(float angle)
    {
        // 스킬 사용시 마우스 방향에따라 애니메이션 방향을 바꾸어 실행함
        rend.flipX = false;
        if (angle >= 1.57f && angle < 2.3f)
            ChangeAnimationState(UP_ATTACK);
        else if (angle >= 2.3f && angle < 3.9f)
        {
            rend.flipX = true;
            ChangeAnimationState(RIGHT_ATTACK);
        }
        else if (angle >= 3.9f && angle < 5.5f)
            ChangeAnimationState(DOWN_ATTACK);
        else if (angle >= 5.5f && angle < 7.0f)
            ChangeAnimationState(RIGHT_ATTACK);
        else if (angle >= 7.0f && angle <= 7.8f)
            ChangeAnimationState(UP_ATTACK);
    }


    

        // 플레이어 이동 함수
        public void Move()
        {
            if (!isDash && !SkillManager.isSkill)
            {
                // 가로 이동 애니메이션 동작
                if (vector.x != 0)
                {
                    ChangeAnimationState(PLAYER_WALK_RIGHT);
                    if (vector.x > 0)
                    {
                        moveRot = 1;
                        rend.flipX = false; // Player의 Sprite를 좌우반전시키는 함수 , true일 때 반전
                    }
                    else
                    {
                        moveRot = 2;
                        rend.flipX = true;
                    }
                }
                else if (vector.y < 0)  // 세로 방향 애니메이션 동작
                {
                    ChangeAnimationState(PLAYER_WALK_DOWN);
                    moveRot = 3;
                }
                else if (vector.y > 0)
                {
                    ChangeAnimationState(PLAYER_WALK_UP);
                    moveRot = 4;
                }
                else  // 이동중이 아닐때 기본자세 애니메이션 동작
                {
                    switch (moveRot)
                    {
                        case 1:
                            ChangeAnimationState(PLAYER_IDLE_RIGHT);
                            break;
                        case 2:
                            ChangeAnimationState(PLAYER_IDLE_RIGHT);
                            break;
                        case 3:
                            ChangeAnimationState(PLAYER_IDLE_DOWN);
                            break;
                        case 4:
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
            if (collision.gameObject.tag == "Object")
            {
                talkNpc = collision.gameObject;
                Vector3 pos = collision.gameObject.transform.position;      // NPC의 위치
                pos = new Vector3(pos.x, pos.y + 0.2f, pos.z);          // pos에 NPC 머리위 위치값 저장 
                Button = Instantiate(Text_Button, pos, Quaternion.identity);    // NPC머리위에 대화버튼 생성
            }

            if (collision.gameObject.GetComponent<FieldItems>() != null)
            {
                GameObject.Find("SkillSlot").GetComponent<SkillSlot>().Equip(collision.gameObject.GetComponent<FieldItems>().skill);
                collision.gameObject.GetComponent<FieldItems>().DestroyItem();
            }
        }


        private void OnCollisionExit2D(Collision2D collision)
        {
            // NPC와 멀어지면 대화버튼 제거
            Destroy(Button);
            talkNpc = null;
        }

        // 애니메이션 교체 함수
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

            if (moveRot == 1 || moveRot == 2)
                ChangeAnimationState(DASH_RIGHT);
            else if (moveRot == 3)
                ChangeAnimationState(DASH_DOWN);
            else if (moveRot == 4)
                ChangeAnimationState(DASH_UP);
            Speed = dashSpeed;

            yield return new WaitForSeconds(dashTime);

            Speed = baseSpeed;
            isDash = false;

        }
    
}