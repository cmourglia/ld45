using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Blob))]
public class FindMate : MonoBehaviour
{
    public float reactionTimeSeconds = 2.0f;
    public float reactionTimeFleeingTarget = .5f;

    private Blob blob;
    private Blob target;
    private bool isFleeing;
    private float lastFleeingTargetTime;
    private float enabledTime;

    void OnEnable()
    {
        this.blob = GetComponent<Blob>();

        this.enabledTime = Time.time;
        lastFleeingTargetTime = Time.time;
        var appendices = this.blob.AppendixRight + this.blob.AppendixLeft + this.blob.AppendixDown + this.blob.AppendixUp;
        this.isFleeing = appendices > 8;
    }

    void Update()
    {
        // wit 2 secs before starting
        if (Time.time - enabledTime < reactionTimeSeconds) { return; }

        if (this.isFleeing)
        {
            if (Time.time - lastFleeingTargetTime > reactionTimeFleeingTarget)
            {
                lastFleeingTargetTime = Time.time;
                var closest = Blob.instances
                    .OrderBy(x => (this.transform.position - x.transform.position).sqrMagnitude)
                    .Where(x => x.gameObject != this.gameObject)
                    .FirstOrDefault();
                this.target = closest;
            }
        }
        else
        {
            if (target == null || !target.IsAlive)
            {
                this.target = this.findTarget();
            }
        }

        if (this.target == null) { return; }

        // move towards target at full speed
        var dir = this.target.transform.position - this.transform.position;
        dir.Normalize();
        dir *= this.blob.Speed;
        dir.z = 0;
        if (this.isFleeing) { dir *= -1; }

        this.GetComponent<Rigidbody2D>().velocity = dir;
        this.GetComponent<Rigidbody2D>().angularVelocity = 1000.0f;
    }

    Blob findTarget()
    {
        return Blob.instances.Where(x => x.IsAlive).OrderBy(x => Random.Range(0, 100000)).FirstOrDefault();
    }
}
