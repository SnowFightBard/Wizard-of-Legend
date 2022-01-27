using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float hp = 50f;
    public float moveSpeed = 3.0f;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(hp<=0)
        {
            Destroy(this.gameObject);
        }
    }
}
