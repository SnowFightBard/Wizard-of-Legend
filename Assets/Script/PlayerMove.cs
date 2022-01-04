using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 1.0f; // �÷��̾� �̵� �ӵ�
    float Vertical; // ����, �Ʒ��� ���Ⱚ�� �޴� ����
    float Horizontal; // ����, ������ ���Ⱚ�� �޴� ����

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
        else
        {
            animator.SetBool("walk_h", false);
        }

        if (Vertical < 0)
        {
            animator.SetBool("walk_down", true);
            animator.SetBool("walk_up", false);
        }
        else if (Vertical > 0)
        {
            animator.SetBool("walk_up", true);
            animator.SetBool("walk_down", false);
        }
        else
        {
            animator.SetBool("walk_up", false);
            animator.SetBool("walk_down", false);
        }

        Vector3 dir = (Vertical * Vector3.up) + (Horizontal * Vector3.right); // transform.Translate() ������ �ڷ����� ���߱� ���� ������ ���ο� Vector3 ���� ����
        this.transform.Translate(dir * moveSpeed * Time.deltaTime); // Player ������Ʈ �̵� �Լ�
    }
}