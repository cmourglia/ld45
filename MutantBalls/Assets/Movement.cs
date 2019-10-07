using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 2;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (GetComponent<Blob>())
            speed = GetComponent<Blob>().Speed;
    }

    // Update is called once per frame
    void Update()
    {
        // Brake
        if (Input.GetKey(KeyCode.F))
        {
            // Forward dash
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity *= 0.0f;
            return;
        }
        else if (rb.bodyType == RigidbodyType2D.Kinematic)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            return;
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(x * speed, y * speed, 0);

        if (Input.GetKey(KeyCode.R))
        {
            rb.angularVelocity += 2000.0f;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Forward dash
            rb.velocity *= 20.0f;
        }


    }
}
