using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopulatePopup : MonoBehaviour
{
    public Blob Parent1;
    public Blob Parent2;
    public Blob Child;
    public Brawl BrawlManager;

    public Blob Show(Blob parent1, Blob parent2)
    {
        this.gameObject.SetActive(true);
        Time.timeScale = 0;

        parent1.CopyTo(this.Parent1);
        parent2.CopyTo(this.Parent2);
        this.Child.MergeFrom(this.Parent1, this.Parent2);

        this.Parent1.Heal();
        this.Parent2.Heal();
        this.Child.Heal();

        return this.Child;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1;
            this.BrawlManager.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
