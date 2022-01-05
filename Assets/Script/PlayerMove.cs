using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 1.5f; // �÷��̾� �̵� �ӵ�
    float Vertical; // ����, �Ʒ��� �Է��� �޴� ����
    float Horizontal; // ����, ������ �Է��� �޴� ����

    public Animator animator; // Animator �Ӽ� ���� ����

    public SpriteRenderer rend; // SpriteRenderer �Ӽ� ���� ����

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
    }


    // �÷��̾�� �浹�Ǵ� ��ü�� �̸��� �ܼ�â�� ��� (�׽�Ʈ��)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.name);
    }
}