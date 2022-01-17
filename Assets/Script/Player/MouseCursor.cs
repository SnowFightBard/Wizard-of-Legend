using UnityEngine;
using System.Collections;

public class MouseCursor : MonoBehaviour
{

    //���콺 �����ͷ� ����� �ؽ�ó�� �Է¹޽��ϴ�.
    public Texture2D cursorTexture;

    //�ؽ�ó�� �߽��� ���콺 ��ǥ�� �� ������ üũ�ڽ��� �Է¹޽��ϴ�.
    public bool hotSpotIsCenter = false;

    //�ؽ�ó�� ����κ��� ���콺�� ��ǥ�� �� ������ �ؽ�ó��
    //��ǥ�� �Է¹޽��ϴ�.
    public Vector2 adjustHotSpot = Vector2.zero;

    //���ο��� ����� �ʵ带 �����մϴ�.
    private Vector2 hotSpot;


    public void Start()
    {

        //�ڷ�ƾ�� ����մϴ�. TargetCursor()�Լ��� ȣ���մϴ�.
        StartCoroutine("MyCursor");
    }


    //MyCursor()��� �̸��� �ڷ�ƾ�� ���۵˴ϴ�.
    IEnumerator MyCursor()
    {

        //��� �������� �Ϸ�� ������ ������״� �������� �Ϸ�Ǹ�
        //���� �޶�� ����Ƽ �������� ��Ź�ϰ� ����մϴ�.
        yield return new WaitForEndOfFrame();

        //�ؽ�ó�� �߽��� ���콺�� ��ǥ�� ����ϴ� ���
        //�ؽ�ó�� ���� ������ 1/2�� hot Spot ��ǥ�� �Է��մϴ�.
        if (hotSpotIsCenter)
        {
            hotSpot.x = cursorTexture.width / 2;
            hotSpot.y = cursorTexture.height / 2;
        }
        else
        {
            //�߽��� ������� ���� ��� Adjust Hot Spot���� �Է� ����
            //���� ����մϴ�.
            hotSpot = adjustHotSpot;
        }

        //���� ���ο� ���콺 Ŀ���� ȭ�鿡 ǥ���մϴ�.
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }
}