using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Folllow : MonoBehaviour
{
    public GameObject Target;

    void Update()
    {
        this.transform.position = this.Target.transform.position;
    }
}
