using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // �÷��̾� ���ۿ� ���õ� ��ũ��Ʈ //
    
    [SerializeField]
    GameObject Text_Button;

    
    GameObject Button;
    GameManager Manager;
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
    public bool isDash;        // �뽬������ üũ�ϴ� ����
    float Vertical;      // ����, �Ʒ��� �Է��� �޴� ����
    float Horizontal;       // ����, ������ �Է��� �޴� ����

    //================
    //   ��ų���� ����
    //================
    public GameObject Skill;
    public bool isSkill = false;
    public int index;
    public Vector2 skill_Des_Pos;

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
    bool isTalk = false;    // ��ȭ������ üũ�ϴ� ����
    bool isInventory = false;

    private void Start()
    {
        animator = GetComponent<Animator>(); // animator ������ Player�� Animator �Ӽ����� �ʱ�ȭ
        rigidBody = GetComponent<Rigidbody2D>();
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    // Ű �Է� ������ ��Ƶ�
    private void Update()
    {
        if (!isDash && !isSkill &&Manager.isTalk == false)
        {
            vector.y = Input.GetAxisRaw("Vertical");
            vector.x = Input.GetAxisRaw("Horizontal");


            // �뽬���
            if (Input.GetKeyDown(KeyCode.Space))
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
                Manager.Action(talkNpc);
            Manager.inventory.SetActive(false);
            isInventory = false;
        }

        // I�� ������ �κ��丮â ON OFF
        if(Input.GetKeyDown(KeyCode.I))
        {
            isInventory = !isInventory;
            Manager.inventory.SetActive(isInventory);
        }

        // �뽬 or ��ȭ���� �ƴҶ� ��ų��� ����
        if (isDash == false && Manager.isTalk == false && isInventory == false)
        {
            // ��Ŭ���� ��ų ���
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                index = 1;
                // ��ų ������� �ƴ϶�� ��ų�� ������ �����ϰ� �����
                if (!isSkill)
                {
                    // ��ų������Ʈ ���� �ڷ�ƾ ����
                    StartCoroutine("SkillSpawn");
                }
            }
        }

    }

    private void FixedUpdate()
    {
        // ��ų����߿��� ������ �� ����
        if (isSkill == true)
        {
            vector = Vector2.zero;
        }

        Move(); // �÷��̾� �̵�
    }


    // ��ų ���� �ִϸ��̼��� ����ϴ� �Լ�
    void SkillAnimation(float angle)
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
    

    // ��ų���� ������Ʈ �Լ�
    IEnumerator SkillSpawn()
    {
        isSkill = true;

        Vector2 Player_pos = GameObject.Find("Player").transform.position;
        float angle = GameObject.Find("Mouse_Rot").transform.rotation.eulerAngles.z + 90;
        angle = angle / 360 * 2 * Mathf.PI;
        //Debug.Log("���콺 ���� : " + angle);

        SkillAnimation(angle);

        Vector2 skill_Pos = new Vector2(Player_pos.x + Mathf.Cos(angle) * Manager.data[index].range, Player_pos.y + Mathf.Sin(angle) * Manager.data[index].range);   // ��ų ������ġ   // ��ų ������ġ
        skill_Des_Pos = new Vector2(Player_pos.x + Mathf.Cos(angle) * 100, Player_pos.y + Mathf.Sin(angle) * 100);      // ��ų�� ���ϴ� ��


        if (Manager.data[index].type == 1 || Manager.data[index].type == 2) // �ܹ߼� ���� , ���Ÿ� ��ų
        {
            Instantiate(Skill, skill_Pos, Quaternion.identity);
            yield return new WaitForSeconds(Manager.data[index].activeTime);
        }
        else if (Manager.data[index].type == 3)     // ���Ÿ� ���� ��ų 
        {
            // �������� 5�� ������
            for (int i = 1; i <= 5; i++)
            {
                skill_Pos = new Vector2(Player_pos.x + Mathf.Cos(angle) * (Manager.data[index].range + (i * 0.3f)), Player_pos.y + Mathf.Sin(angle) * (Manager.data[index].range + (i * 0.3f)));
                Instantiate(Skill, skill_Pos, Quaternion.identity);
                yield return new WaitForSeconds(Manager.data[index].activeTime / 5);
            }
        }
        else if (Manager.data[index].type == 4)     // ���Ÿ� ��ä�� ��ų
        {
            // ��ä�� ������� 3�� �߻�
            for (int i = 0; i < 3; i++)
            {
                skill_Pos = new Vector2(Player_pos.x + Mathf.Cos(angle - 45 + (i * 45)) * Manager.data[index].range, Player_pos.y + Mathf.Sin(angle - 45 + (i * 45)) * Manager.data[index].range);
                //skill_Des_Pos = new Vector2(Player_pos.x + Mathf.Cos(angle - 45 + (i * 45)) * 100, Player_pos.y + Mathf.Sin(angle - 45 + (i * 45)) * 100);
                Instantiate(Skill, skill_Pos, Quaternion.identity);
            }
            yield return new WaitForSeconds(Manager.data[index].activeTime);
        }

        isSkill = false;
        
    }

    // �÷��̾� �̵� �Լ�
    public void Move()
    {
        if (!isDash && !isSkill)
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
        else if(moveRot == 4)
            ChangeAnimationState(DASH_UP);
        Speed = dashSpeed;

        yield return new WaitForSeconds(dashTime);

        Speed = baseSpeed;
        isDash = false;

    }
}