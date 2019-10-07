using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float cameraDistance = 30.0f;

    //private void Awake()
    //{
    //    GetComponent<Camera>().orthographicSize = ((Screen.height / 2) * cameraDistance);
    //}

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -cameraDistance);
    }
}
