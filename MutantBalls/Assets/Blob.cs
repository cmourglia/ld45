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

    public void CopyTo(Blob target)
    {
        target.HP = this.HP;
        target.Size = this.Size;
        target.Color = this.Color;
    }

    public void MergeFrom(Blob parent1, Blob parent2)
    {
        this.HP = (parent1.HP + parent2.HP) / 2;
        this.Size = (parent1.Size + parent2.Size) / 2;
        this.Color = Color.Lerp(parent1.Color, parent2.Color, .5f);
    }
}
