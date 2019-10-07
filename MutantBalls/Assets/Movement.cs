using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 2;
    Rigidbody2D rb;

    public bool ModeBeauGosse = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (ModeBeauGosse)
        {
            rb.MovePosition(new Vector2(x, y) + rb.position);
            return;
        }

        rb.velocity = new Vector3(x * speed, y * speed, 0);

        if (Input.GetKeyDown(KeyCode.R))
        {
            rb.angularVelocity = 10.0f;
        }
        else
        {
            rb.angularVelocity = 0.0f;
        }
    }
}
