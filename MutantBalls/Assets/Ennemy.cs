using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Blob))]
public class Ennemy : MonoBehaviour
{
    public float reactionTimeSeconds = 2.0f;

    private Blob _blob;
    private Blob _target = null;
    private float _lastTime;

    private void Start()
    {
        _lastTime = Time.time;
        _blob = GetComponent<Blob>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - _lastTime >= reactionTimeSeconds)
        {
            // detect
            _target = GetClosestBlob();
            _lastTime = Time.time;
            return;
        }

        if (_target != null)
        {
            // move towards target at full speed
            var dir = _target.transform.position - transform.position;
            dir.Normalize();
            dir *= _blob.Speed;
            dir.z = 0;

            this.GetComponent<Rigidbody2D>().velocity = dir;
            this.GetComponent<Rigidbody2D>().angularVelocity = 1000.0f;
        }
    }

    Blob GetClosestBlob()
    {
        Blob bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Blob potentialTarget in Blob.instances)
        {
            if (potentialTarget == _blob)
                continue;
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }
}
