using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MateSelector : MonoBehaviour
{
    public GameObject Player;
    public GameObject TargetAim;
    private Collider2D playerCollider;
    private Collider2D collider;
    private HashSet<Collider2D> triggers = new HashSet<Collider2D>();

    void Start()
    {
        this.collider = this.GetComponent<Collider2D>();
        this.playerCollider = this.Player.GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider) { return; }
        this.triggers.Add(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        this.triggers.Remove(other);
    }

    void Update()
    {
        var target = this.triggers.OrderBy(other => (this.transform.position - other.transform.position).sqrMagnitude).FirstOrDefault();
        if (target == null)
        {
            this.TargetAim.SetActive(false);
        }
        else
        {
            this.TargetAim.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, this.TargetAim.transform.position.z);
            this.TargetAim.SetActive(true);
        }
    }
}
