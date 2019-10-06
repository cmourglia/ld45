using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    public float HP;
    public float Size;
    public Color Color;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        this.spriteRenderer.color = this.Color;
        this.transform.localScale = new Vector3(this.Size, this.Size, 0);
    }

    public void Hurt(int dmg)
    {
        this.HP -= dmg;
    }
}
