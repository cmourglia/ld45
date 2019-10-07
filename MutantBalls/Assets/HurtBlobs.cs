using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBlobs : MonoBehaviour
{
    public int Damage = 1;
    public bool Enabled;

    public bool isHingedJointWith(Blob theBlob,GameObject root)
    {
        var hjs = root.GetComponents<HingeJoint2D>();
        foreach (HingeJoint2D hj in hjs)
        {
            var b = hj.connectedBody.gameObject.GetComponent<Blob>();
            if (b != null && b == theBlob)
            {
                return true;
            }
            var resultInJointedBodies = isHingedJointWith(theBlob, hj.connectedBody.gameObject);
            if (resultInJointedBodies)
            {
                return true;
            }
        }

        return false;
    }

    private bool _findThisInConnectedBodies(GameObject source)
    {
        var hjs = source.GetComponents<HingeJoint2D>();
        foreach (HingeJoint2D hj in hjs)
        {
            var cg = hj.connectedBody.gameObject;
            if (cg == this.gameObject)
            {
                return true;
            }
            var connected = _findThisInConnectedBodies(cg);
            if (connected)
            {
                return true;
            }
        }
        return false;
    }

    private bool belongsTo(Blob b)
    {
        return _findThisInConnectedBodies(b.gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!this.Enabled) { return; }

        var b = other.gameObject.GetComponent<Blob>();

        if (b == null || this.belongsTo(b))
            return;

        if (b.gameObject != this.gameObject)
        {
            b.Hurt(this.Damage);
        }
    }
}
