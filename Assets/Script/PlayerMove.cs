using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    GameObject Text_Button;

    GameObject Button;

    public Animator animator; // Animator �Ӽ� ���� ����
    public SpriteRenderer rend; // SpriteRenderer �Ӽ� ���� ����
    private Vector2 vector;
    private Rigidbody2D rigidBody;
    
    //=========================
    //   �÷��̾� �̵����� ����
    //=========================
    float baseSpeed = 1.5f;     // �⺻ �̵� �ӵ�
    public float Speed = 1.5f;      // ���� �÷��̾� �̵� �ӵ�
    public float dashSpeed;         // �뽬 �ӵ�
    private float dashTime = 0.45f;        // �뽬 �����ð�
    private bool isDash;        // �뽬������ üũ�ϴ� ����
    float Vertical;      // ����, �Ʒ��� �Է��� �޴� ����
    float Horizontal;       // ����, ������ �Է��� �޴� ����

    //==============================
    //   �÷��̾� �ִϸ��̼ǰ��� ����
    //==============================
    int moveRot = 2;    // ���������� �̵��� ����
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

    private string currentState;    // ���� �������� �ִϸ��̼�



    bool Talk = false;  // ��ȭ �������� üũ�ϴ� ����
    bool isTalk = false;    // ��ȭ������ üũ�ϴ� ����

    private void Start()
    {
        animator = GetComponent<Animator>(); // animator ������ Player�� Animator �Ӽ����� �ʱ�ȭ
        rigidBody = GetComponent<Rigidbody2D>();
    }
    
    
    // Ű �Է� ������ ��Ƶ�
    private void Update()
    {

        if (!isDash)
        {
            vector.y = Input.GetAxisRaw("Vertical");
            vector.x = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space) && (vector.x != 0 || vector.y != 0))
            {
                StartCoroutine(Dash());
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("��Ŭ��");
                ChangeAnimationState(ATTACK);
            }
        }
    }

    private void FixedUpdate()
    {
        Move(); // �÷��̾� �̵�
    }


    void Move()
    {
        if (!isDash)
        {
            // ���� �̵� �ִϸ��̼� ����
            if (vector.x != 0)
            {
                ChangeAnimationState(PLAYER_WALK_RIGHT);
                moveRot = 1;
                if (vector.x > 0)
                {
                    rend.flipX = false; // Player�� Sprite�� �¿������Ű�� �Լ� , true�� �� ����
                }
                else
                {
                    rend.flipX = true;
                }
            }
            else if (vector.y < 0)  // ���� ���� �ִϸ��̼� ����
            {
                ChangeAnimationState(PLAYER_WALK_DOWN);
                moveRot = 2;
            }
            else if (vector.y > 0)
            {
                ChangeAnimationState(PLAYER_WALK_UP);
                moveRot = 3;
            }
            else  // �̵����� �ƴҶ� �⺻�ڼ� �ִϸ��̼� ����
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

        // �÷��̾� �̵�ó��
        rigidBody.velocity = vector.normalized * Speed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ε��� ������Ʈ�� NPC�� ��� NPC�Ӹ����� ��ȭ��ư Ȱ��ȭ
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
        // NPC�� �־����� ��ȭ��ư ����
        Destroy(Button);
    }

    void ChangeAnimationState(string newState)
    {
        // ���� �ִϸ��̼ǰ� ��ü�� �ִϸ��̼��� ������ �������� ����
        if (currentState == newState) return;

        // ��ü�� �ִϸ��̼� ����
        animator.Play(newState);

        // ���� ���¸� ��ü�� �ִϸ��̼����� ����
        currentState = newState;
    }

    // �뽬 ���
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