using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBlobs : MonoBehaviour
{
    public int Damage = 1;
    public bool Enabled;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!this.Enabled) { return; }

        var b = other.gameObject.GetComponent<Blob>();
        if (b != null && b.gameObject != this.gameObject)
        {
            b.Hurt(this.Damage);
        }
    }
}
