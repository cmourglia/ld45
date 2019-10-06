using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBlobs : MonoBehaviour
{
    public int Damage = 1;

    void OnCollisionEnter2D(Collision2D other)
    {
        var b = other.gameObject.GetComponent<Blob>();
        if (b != null && b.gameObject != this.gameObject)
        {
            // Hurt
            b.Hurt(this.Damage);
        }
    }
}
