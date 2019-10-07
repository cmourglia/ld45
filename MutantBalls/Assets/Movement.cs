using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Blob))]
public class Movement : MonoBehaviour
{
    Blob blob;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        blob = this.GetComponent<Blob>();
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

        rb.velocity = new Vector3(x * this.blob.Speed, y * this.blob.Speed, 0);

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
