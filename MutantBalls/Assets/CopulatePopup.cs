using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopulatePopup : MonoBehaviour
{
    public Blob Parent1;
    public Blob Parent2;
    public Blob Child;
    public Brawl BrawlManager;

    public void Show(Blob parent1, Blob parent2)
    {
        this.gameObject.SetActive(true);
        parent1.CopyTo(this.Parent1);
        parent2.CopyTo(this.Parent2);
        this.Child.MergeFrom(this.Parent1, this.Parent2);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            this.BrawlManager.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
