using UnityEngine;
using System.Collections;

public class Sort : MonoBehaviour
{

    private float xpos;
    private float ypos;

    Transform tf;

    void Start()
    {
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        xpos = tf.position.x;
        ypos = tf.position.y;
        tf.position = new Vector3(xpos, ypos, ypos / 1000.0f);
    }
}