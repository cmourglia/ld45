using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tentaculator))]
public class Blob : MonoBehaviour
{
    public static List<Blob> instances = new List<Blob>();

    public bool IsAlive = true;
    public float HP;
    public float Size;
    public Color Color;
    public float Speed = 5.0f;

    public int AppendixUp;
    public int AppendixRight;
    public int AppendixDown;
    public int AppendixLeft;
    public Tentaculator Tentaculator;

    private SpriteRenderer spriteRenderer;
    private float currentHP;

    void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        Blob.instances.Add(this);
    }

    private void OnDestroy()
    {
        Blob.instances.Remove(this);
    }

    void OnEnable()
    {
        this.Heal();
    }

    public void MakeArms()
    {
        var tentaculator = this.GetComponent<Tentaculator>();
        tentaculator.arms = 4;
        tentaculator.armDepths = new int[]{
            this.AppendixUp,
            this.AppendixRight,
            this.AppendixDown,
            this.AppendixLeft,
        };
        tentaculator.RootColor = this.Color;
        tentaculator.makeArms();
    }

    void Update()
    {
        this.spriteRenderer.color = this.Color;
        this.transform.localScale = new Vector3(this.Size, this.Size, 1);
    }

    void OnGUI()
    {
        var screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);
        var text = Mathf.Round(currentHP).ToString();

        var style = new GUIStyle();
        style.fontSize = 10;
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.white;

        GUI.Label(new Rect(screenPoint.x - 10, Screen.height - screenPoint.y - 10, 20, 20), text, style);
    }

    public void Hurt(int dmg)
    {
        this.currentHP -= dmg;
        if (this.currentHP <= 0)
        {
            this.IsAlive = false;
            GetComponent<Tentaculator>().DestroyInSeconds(2.0f);
            this.gameObject.SetActive(false);
        }
    }

    public void Heal()
    {
        this.currentHP = this.HP;
    }

    public void CopyTo(Blob target)
    {
        target.HP = this.HP;
        target.Size = this.Size;
        target.Color = this.Color;
        target.Speed = this.Speed;
        target.AppendixUp = this.AppendixUp;
        target.AppendixRight = this.AppendixRight;
        target.AppendixDown = this.AppendixDown;
        target.AppendixLeft = this.AppendixLeft;
    }

    public void MergeFrom(Blob parent1, Blob parent2)
    {
        this.HP = (parent1.HP + parent2.HP) / 2;
        this.Size = (parent1.Size + parent2.Size) / 2;
        this.Speed = (parent1.Speed + parent2.Speed) / 2;

        this.Color = Color.Lerp(parent1.Color, parent2.Color, .5f);

        this.AppendixUp = (parent1.AppendixUp + parent2.AppendixUp) / 2;
        this.AppendixRight = (parent1.AppendixRight + parent2.AppendixRight) / 2;
        this.AppendixDown = (parent1.AppendixDown + parent2.AppendixDown) / 2;
        this.AppendixLeft = (parent1.AppendixLeft + parent2.AppendixLeft) / 2;
    }
}
