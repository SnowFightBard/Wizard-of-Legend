using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    float angle;
    Vector2 target, mouse;

    private void Start()
    {
        target = transform.position;
    }
    private void Update()
    {
        //mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
        //this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        //���� ����� ���� ���콺�� ���� ������Ʈ�� ������ ��ǥ�� �ӽ÷� �����մϴ�.
        Vector3 mPosition = Input.mousePosition; //���콺 ��ǥ ����
        Vector3 oPosition = transform.position; //���� ������Ʈ ��ǥ ����

        //ī�޶� �ո鿡�� �ڷ� ���� �ֱ� ������, ���콺 position�� z�� ������
        //���� ������Ʈ�� ī�޶���� z���� ���̸� �Է½������ �մϴ�.
        mPosition.z = oPosition.z - Camera.main.transform.position.z;

        //ȭ���� �ȼ����� ��ȭ�Ǵ� ���콺�� ��ǥ�� ����Ƽ�� ��ǥ�� ��ȭ�� ��� �մϴ�.
        //�׷���, ��ġ�� ã�ư� �� �ְڽ��ϴ�.
        Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);

        //������ ��ũź��Ʈ(arctan, ��ź��Ʈ)�� ���� ������Ʈ�� ��ǥ�� ���콺 ����Ʈ�� ��ǥ��
        //�̿��Ͽ� ������ ���� ��, ���Ϸ�(Euler)ȸ�� �Լ��� ����Ͽ� ���� ������Ʈ�� ȸ����Ű��
        //����, �� ���� �Ÿ����� ���� �� ���Ϸ� ȸ���Լ��� �����ŵ�ϴ�.

        //�켱 �� ���� �Ÿ��� ����Ͽ�, dy, dx�� ������ �Ӵϴ�.
        float dy = target.y - oPosition.y;
        float dx = target.x - oPosition.x;

        //������ ȸ�� �Լ��� 0���� 180 �Ǵ� 0���� -180�� ������ �Է� �޴µ� ���Ͽ�
        //(���� 270�� ���� ���� �Էµ� ���� ���������ϴ�.) ��ũź��Ʈ Atan2()�Լ��� ��� ����
        //���� ��(180���� ����(3.141592654...)��)���� ��µǹǷ�
        //���� ���� ������ ��ȭ�ϱ� ���� Rad2Deg�� �����־�� ������ �˴ϴ�.
        float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        //������ ������ ���Ϸ� ȸ�� �Լ��� �����Ͽ� z���� �������� ���� ������Ʈ�� ȸ����ŵ�ϴ�.
        transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree - 90);
    }
}
