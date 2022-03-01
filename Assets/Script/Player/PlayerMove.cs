using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // �÷��̾� ���ۿ� ���õ� ��ũ��Ʈ //

    [SerializeField] GameObject Text_Button;

    GameObject Button;
    SkillManager SkillManager;
    GameManager GameManager;
    public Animator animator; // Animator �Ӽ� ���� ����
    public SpriteRenderer rend; // SpriteRenderer �Ӽ� ���� ����
    private Vector2 vector;
    private Rigidbody2D rigidBody;



    //=========================
    //   �÷��̾� �ൿ���� ����
    //=========================
    float baseSpeed = 1.5f;     // �⺻ �̵� �ӵ�
    public float Speed = 1.5f;      // ���� �÷��̾� �̵� �ӵ�
    public float dashSpeed;         // �뽬 �ӵ�
    private float dashTime = 0.45f;        // �뽬 �����ð�
    public bool isDash;        // �뽬������ üũ�ϴ� ����
    float Vertical;      // ����, �Ʒ��� �Է��� �޴� ����
    float Horizontal;       // ����, ������ �Է��� �޴� ����

    //==============================
    //   �÷��̾� �ִϸ��̼ǰ��� ����
    //==============================
    int moveRot = 3;    // ���������� �̵��� ����
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
    private string currentState;    // ���� �������� �ִϸ��̼�



    bool Talk = false;  // ��ȭ �������� üũ�ϴ� ����
    public bool isTalk = false;    // ��ȭ������ üũ�ϴ� ����
    public bool isInventory = false;

    private void Start()
    {
        animator = GetComponent<Animator>(); // animator ������ Player�� Animator �Ӽ����� �ʱ�ȭ
        rigidBody = GetComponent<Rigidbody2D>();
        SkillManager = GameObject.Find("SkillManager").GetComponent<SkillManager>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    // Ű �Է� ������ ��Ƶ�
    private void Update()
    {
        if (!isDash && !SkillManager.isSkill && GameManager.isTalk == false)
        {
            vector.y = Input.GetAxisRaw("Vertical");
            vector.x = Input.GetAxisRaw("Horizontal");


            // �뽬���
            if (Input.GetKeyDown(KeyCode.Space) && !isInventory)
            {
                // �������� �ʰ� ���ڸ��� �־ �뽬�Ҽ��ֵ��� vector���� moveRot�������� �ٲ���
                if (moveRot == 1)
                    vector.x = 1;
                else if (moveRot == 2)
                    vector.x = -1;
                else if (moveRot == 3)
                    vector.y = -1;
                else
                    vector.y = 1;

                // �뽬 �ڷ�ƾ ����
                StartCoroutine(Dash());
            }
        }

        // F�� ������ ��ȭ
        if (Input.GetKeyDown(KeyCode.F))
        {
            vector = new Vector2(0, 0);
            if (talkNpc != null)
                GameManager.Action(talkNpc);
            GameManager.inventory.SetActive(false);
            isInventory = false;
        }

        // I�� ������ �κ��丮â ON OFF
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventory = !isInventory;
            GameManager.inventory.SetActive(isInventory);
        }

    }

    private void FixedUpdate()
    {
        // ��ų����߿��� ������ �� ����
        if (SkillManager.isSkill == true)
        {
            vector = Vector2.zero;
        }

        Move(); // �÷��̾� �̵�
    }


    // ��ų ���� �ִϸ��̼��� ����ϴ� �Լ�
    public void SkillAnimation(float angle)
    {
        // ��ų ���� ���콺 ���⿡���� �ִϸ��̼� ������ �ٲپ� ������
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




    // �÷��̾� �̵� �Լ�
    public void Move()
    {
        if (!isDash && !SkillManager.isSkill)
        {
            // ���� �̵� �ִϸ��̼� ����
            if (vector.x != 0)
            {
                ChangeAnimationState(PLAYER_WALK_RIGHT);
                if (vector.x > 0)
                {
                    moveRot = 1;
                    rend.flipX = false; // Player�� Sprite�� �¿������Ű�� �Լ� , true�� �� ����
                }
                else
                {
                    moveRot = 2;
                    rend.flipX = true;
                }
            }
            else if (vector.y < 0)  // ���� ���� �ִϸ��̼� ����
            {
                ChangeAnimationState(PLAYER_WALK_DOWN);
                moveRot = 3;
            }
            else if (vector.y > 0)
            {
                ChangeAnimationState(PLAYER_WALK_UP);
                moveRot = 4;
            }
            else  // �̵����� �ƴҶ� �⺻�ڼ� �ִϸ��̼� ����
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


        // �÷��̾� �̵�ó��
        rigidBody.velocity = vector.normalized * Speed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ε��� ������Ʈ�� NPC�� ��� NPC�Ӹ����� ��ȭ��ư Ȱ��ȭ
        if (collision.gameObject.tag == "Object")
        {
            talkNpc = collision.gameObject;
            Vector3 pos = collision.gameObject.transform.position;      // NPC�� ��ġ
            pos = new Vector3(pos.x, pos.y + 0.2f, pos.z);          // pos�� NPC �Ӹ��� ��ġ�� ���� 
            Button = Instantiate(Text_Button, pos, Quaternion.identity);    // NPC�Ӹ����� ��ȭ��ư ����
        }

        // �ε��� ������Ʈ�� ��ų(������)�� ���
        if (collision.gameObject.GetComponent<FieldItems>() != null)
        {
            GameObject.Find("SkillSlot").GetComponent<SkillSlot>().Equip(collision.gameObject.GetComponent<FieldItems>().skill);
            collision.gameObject.GetComponent<FieldItems>().DestroyItem();
        }

    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        // NPC�� �־����� ��ȭ��ư ����
        Destroy(Button);
        talkNpc = null;
    }

    // �ִϸ��̼� ��ü �Լ�
    public void ChangeAnimationState(string newState)
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