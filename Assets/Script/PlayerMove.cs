using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    Vector3 position;
    public float Speed = 2.0f;
    public string Rotate;

    private void Start()
    {
        position = transform.position;
    }


    void Update()
    {

        position.x += Speed * Time.deltaTime * Input.GetAxisRaw("Horizontal");
        position.y += Speed * Time.deltaTime * Input.GetAxisRaw("Vertical");

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            position.x += Speed * 60 * Time.deltaTime * Input.GetAxisRaw("Horizontal");
            position.y += Speed * 60 * Time.deltaTime * Input.GetAxisRaw("Vertical");
        }*/

        transform.position = position;

    }

}
