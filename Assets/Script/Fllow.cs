using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fllow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Player_pos = GameObject.Find("Player").transform.position;
        float angle = GameObject.Find("Mouse_Rot").transform.rotation.eulerAngles.z + 90;
        angle = angle / 360 * 2 * Mathf.PI;
        // 3.14 = 180도 = PI     2PI 360도
        // 정규화   0 ~ 1

        float r = 1;

        transform.position = new Vector2(Player_pos.x + Mathf.Cos(angle) * r, Player_pos.y + Mathf.Sin(angle) * r);
    }
}
