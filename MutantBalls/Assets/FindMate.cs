using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Blob))]
public class FindMate : MonoBehaviour
{
    public float reactionTimeSeconds = 2.0f;

    private Blob blob;
    private Blob target;
    private float enabledTime;

    void Start()
    {
        this.blob = GetComponent<Blob>();
    }

    void OnEnable()
    {
        this.enabledTime = Time.time;
    }

    void Update()
    {
        if (target == null || !target.IsAlive)
        {
            this.target = this.findTarget();
            if (this.target == null) { return; }
        }

        if (Time.time - enabledTime < reactionTimeSeconds) { return; }

        if (this.target != null)
        {
            // move towards target at full speed
            var dir = this.target.transform.position - this.transform.position;
            dir.Normalize();
            dir *= this.blob.Speed;
            dir.z = 0;

            this.GetComponent<Rigidbody2D>().velocity = dir;
            this.GetComponent<Rigidbody2D>().angularVelocity = 1000.0f;
        }
    }

    Blob findTarget()
    {
        return Blob.instances.Where(x => x.IsAlive).OrderBy(x => Random.Range(0, 100000)).FirstOrDefault();
    }
}
