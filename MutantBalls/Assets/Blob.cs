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
        this.Color = Random.ColorHSV(0, 1, .5f, 1, .5f, 1);
        this.Size = 1 + Random.value * 4;
        this.HP = this.Size;

        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        this.spriteRenderer.color = this.Color;
        this.transform.localScale = new Vector3(this.Size, this.Size, 0);
    }
}
