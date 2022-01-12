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
    
    float baseSpeed = 1.5f;     // �⺻ �̵� �ӵ�
    public float Speed = 1.5f;      // ���� �÷��̾� �̵� �ӵ�
    public float dashSpeed;         // �뽬 �ӵ�
    private float dashTime = 0.3f;        // �뽬 �����ð�
    private bool isDash;        // �뽬������ üũ�ϴ� ����
    float Vertical;      // ����, �Ʒ��� �Է��� �޴� ����
    float Horizontal;       // ����, ������ �Է��� �޴� ����


    bool Talk = false;  // ��ȭ �������� üũ�ϴ� ����
    bool isTalk = false;    // ��ȭ������ üũ�ϴ� ����

    private void Start()
    {
        animator = GetComponent<Animator>(); // animator ������ Player�� Animator �Ӽ����� �ʱ�ȭ
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
        Move(); // �÷��̾� �̵�
    }


    void Move()
    {
        vector.y = Input.GetAxis("Vertical");
        vector.x = Input.GetAxis("Horizontal");

        // ���� �̵� �ִϸ��̼� ����
        if (vector.x != 0)
        {
            animator.SetBool("walk_h", true);
            animator.SetBool("walk_down", false);
            animator.SetBool("walk_up", false);

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
        else // �̵����� �ƴҶ� �⺻�ڼ� �ִϸ��̼� ����
        {
            animator.SetBool("walk_up", false);
            animator.SetBool("walk_down", false);
            animator.SetBool("walk_h", false);
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

    // �뽬 ���
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