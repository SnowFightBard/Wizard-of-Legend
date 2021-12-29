using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    Vector3 position;
    public float Speed = 5.0f;

    private void Start()
    {
        position = transform.position;
    }


    void Update()
    {

        if (Input.GetKey(KeyCode.A))
            position.x -= Speed * Time.deltaTime;


        if (Input.GetKey(KeyCode.D))
            position.x += Speed * Time.deltaTime;


        if (Input.GetKey(KeyCode.W))
            position.y += Speed * Time.deltaTime;


        if (Input.GetKey(KeyCode.S))
            position.y -= Speed * Time.deltaTime;

        if(Input.GetKey(KeyCode.Space))


        transform.position = position;

    }

}
