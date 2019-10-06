using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopulatePopup : MonoBehaviour
{
    public Blob Parent1;
    public Blob Parent2;
    public Blob Child;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show(Blob parent1, Blob parent2)
    {
        this.gameObject.SetActive(true);
        parent1.CopyTo(this.Parent1);
        parent2.CopyTo(this.Parent2);
        this.Child.MergeFrom(this.Parent1, this.Parent2);
    }
}
