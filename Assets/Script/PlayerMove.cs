using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 1.5f; // �÷��̾� �̵� �ӵ�
    float Vertical; // ����, �Ʒ��� �Է��� �޴� ����
    float Horizontal; // ����, ������ �Է��� �޴� ����
    [SerializeField]
    GameObject Text_Button;

    GameObject Button;

    public Animator animator; // Animator �Ӽ� ���� ����

    public SpriteRenderer rend; // SpriteRenderer �Ӽ� ���� ����

    bool Talk = false;  // ��ȭ �������� üũ�ϴ� ����
    bool isTalk = false;    // ��ȭ������ üũ�ϴ� ����
    private float dashTime;
    private bool isDash = false;

    private void Start()
    {
        animator = GetComponent<Animator>(); // animator ������ Player�� Animator �Ӽ����� �ʱ�ȭ
    }

    private void FixedUpdate()
    {
        Move(); // �÷��̾� �̵�
    }

    void Move()
    {
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");

        // ���� �̵� �ִϸ��̼� ����
        if (Horizontal != 0)
        {
            animator.SetBool("walk_h", true);
            animator.SetBool("walk_down", false);
            animator.SetBool("walk_up", false);

            if (Horizontal > 0)
            {
                rend.flipX = false; // Player�� Sprite�� �¿������Ű�� �Լ� , true�� �� ����
            }
            else
            {
                rend.flipX = true;
            }
        }
        else if (Vertical < 0)  // ���� ���� �ִϸ��̼� ����
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
        else // �̵����� �ƴҶ� �⺻�ڼ� �ִϸ��̼� ����
        {
            animator.SetBool("walk_up", false);
            animator.SetBool("walk_down", false);
            animator.SetBool("walk_h", false);
        }

        Vector3 dir = (Vertical * Vector3.up) + (Horizontal * Vector3.right);
        this.transform.Translate(dir * moveSpeed * Time.deltaTime); // Player ������Ʈ �̵� �Լ�

        if (Input.GetKeyDown("space"))
        {
            animator.SetBool("right_dash", true);
            moveSpeed = 6.0f;
            dashTime = 0.1f;
            isDash = true;
        }

        if(isDash)
        {
            if(dashTime > 0)
                dashTime -= Time.deltaTime;
        }

        if(dashTime <= 0 )
        {
            moveSpeed = 1.5f;
            isDash = false;
            animator.SetBool("right_dash", false);
        }
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
}




//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//public class PlayerMove : MonoBehaviour
//{
//    public float moveSpeed = 1.5f; // �÷��̾� �̵� �ӵ�
//    float Vertical; // ����, �Ʒ��� �Է��� �޴� ����
//    float Horizontal; // ����, ������ �Է��� �޴� ����
//    [SerializeField]
//    GameObject Text_Button;

//    GameObject Button;

//    public Animator animator; // Animator �Ӽ� ���� ����

//    public SpriteRenderer rend; // SpriteRenderer �Ӽ� ���� ����
//    public float dashSpeed; // �뽬 �ӵ�
//    private float dashTime;  //
//    public float defaultTime; // 
//    private bool isDash; // �뽬������ üũ�ϴ� ����

//    bool Talk = false;  // ��ȭ �������� üũ�ϴ� ����
//    bool isTalk = false;    // ��ȭ������ üũ�ϴ� ����

//    private void Start()
//    {
//        animator = GetComponent<Animator>(); // animator ������ Player�� Animator �Ӽ����� �ʱ�ȭ
//    }

//    private void FixedUpdate()
//    {
//        Move(); // �÷��̾� �̵�
//    }

//    void Move()
//    {
//        Vertical = Input.GetAxis("Vertical");
//        Horizontal = Input.GetAxis("Horizontal");

//        // ���� �̵� �ִϸ��̼� ����
//        if (Horizontal != 0)
//        {
//            animator.SetBool("walk_h", true);
//            animator.SetBool("walk_down", false);
//            animator.SetBool("walk_up", false);

//            if (Horizontal > 0)
//            {
//                rend.flipX = false; // Player�� Sprite�� �¿������Ű�� �Լ� , true�� �� ����
//            }
//            else
//            {
//                rend.flipX = true;
//            }
//        }
//        else if (Vertical < 0)  // ���� ���� �ִϸ��̼� ����
//        {
//            animator.SetBool("walk_down", true);
//            animator.SetBool("walk_up", false);
//            animator.SetBool("walk_h", false);
//        }
//        else if (Vertical > 0)
//        {
//            animator.SetBool("walk_up", true);
//            animator.SetBool("walk_down", false);
//            animator.SetBool("walk_h", false);
//        }
//        else // �̵����� �ƴҶ� �⺻�ڼ� �ִϸ��̼� ����
//        {
//            animator.SetBool("walk_up", false);
//            animator.SetBool("walk_down", false);
//            animator.SetBool("walk_h", false);
//        }

//        Vector3 dir = (Vertical * Vector3.up) + (Horizontal * Vector3.right);
//        this.transform.Translate(dir * moveSpeed * Time.deltaTime); // Player ������Ʈ �̵� �Լ�

//        if (Input.GetKeyDown("space"))
//        {
//            isDash = true;
//            animator.SetBool("right_dash", true);
//        }

//        if (dashTime <= 0)
//        {
//            if (isDash)
//                dashTime = defaultTime;
//            else
//            {
//                moveSpeed = 1.5f;
//                animator.SetBool("right_dash", false);
//            }
//        }
//        else
//        {
//            dashTime -= Time.deltaTime;
//            moveSpeed = dashSpeed;
//        }
//        isDash = false;
//    }


//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        // �ε��� ������Ʈ�� NPC�� ��� NPC�Ӹ����� ��ȭ��ư Ȱ��ȭ
//        if (collision.gameObject.tag == "Npc")
//        {
//            Vector3 pos = collision.gameObject.transform.position;
//            pos = new Vector3(pos.x, pos.y + 0.2f, pos.z);
//            Button = Instantiate(Text_Button, pos, Quaternion.identity);
//        }
//        Debug.Log(collision.collider.name);
//    }


//    private void OnCollisionExit2D(Collision2D collision)
//    {
//        // NPC�� �־����� ��ȭ��ư ����
//        Destroy(Button);
//    }
//}