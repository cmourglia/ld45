using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    public float HP;
    public float Size;
    public Color Color;
    public bool AppendixUp;
    public bool AppendixRight;
    public bool AppendixDown;
    public bool AppendixLeft;
    public GameObject AppendixUpGO;
    public GameObject AppendixRightGO;
    public GameObject AppendixDownGO;
    public GameObject AppendixLeftGO;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // should not be done every frame
        this.AppendixUpGO.SetActive(this.AppendixUp);
        this.AppendixRightGO.SetActive(this.AppendixRight);
        this.AppendixDownGO.SetActive(this.AppendixDown);
        this.AppendixLeftGO.SetActive(this.AppendixLeft);

        this.spriteRenderer.color = this.Color;
        this.transform.localScale = new Vector3(this.Size, this.Size, 0);
    }

    void OnGUI()
    {
        // doest not work , need to find x, y in screen size.
        var style = new GUIStyle();
        style.fontSize = 50;
        GUI.Label(new Rect(50, 50, 100, 20), this.HP.ToString(), style);
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
        target.AppendixUp = this.AppendixUp;
        target.AppendixRight = this.AppendixRight;
        target.AppendixDown = this.AppendixDown;
        target.AppendixLeft = this.AppendixLeft;
    }

    public void MergeFrom(Blob parent1, Blob parent2)
    {
        this.HP = (parent1.HP + parent2.HP) / 2;
        this.Size = (parent1.Size + parent2.Size) / 2;
        this.Color = Color.Lerp(parent1.Color, parent2.Color, .5f);

        this.AppendixUp = parent1.AppendixUp || parent2.AppendixUp;
        this.AppendixRight = parent1.AppendixRight || parent2.AppendixRight;
        this.AppendixDown = parent1.AppendixDown || parent2.AppendixDown;
        this.AppendixLeft = parent1.AppendixLeft || parent2.AppendixLeft;
    }
}
