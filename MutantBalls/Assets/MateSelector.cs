using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MateSelector : MonoBehaviour
{
    public GameObject Player;
    public GameObject TargetAim;
    public Copulate Copulate;

    private Collider2D playerCollider;
    private Collider2D collider;
    private HashSet<Blob> triggers = new HashSet<Blob>();

    void Start()
    {
        this.collider = this.GetComponent<Collider2D>();
        this.playerCollider = this.Player.GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider) { return; }

        var blob = other.GetComponent<Blob>();
        if (blob == null) { return; }

        this.triggers.Add(blob);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var blob = other.GetComponent<Blob>();
        if (blob == null) { return; }

        this.triggers.Remove(blob);
    }

    void Update()
    {
        var target = this.triggers.OrderBy(other => (this.transform.position - other.transform.position).sqrMagnitude).FirstOrDefault();
        if (target == null)
        {
            this.TargetAim.SetActive(false);
            return;
        }

        this.TargetAim.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, this.TargetAim.transform.position.z);
        this.TargetAim.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            this.gameObject.SetActive(false);
            this.Copulate.SelectMate(target);
        }
    }
}
