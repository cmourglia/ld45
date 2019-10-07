using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BlobGenerator))]
public class Copulate : MonoBehaviour
{
    public Blob Player;
    public MateSelector MateSelector;

    void OnEnable()
    {
        this.Player.Heal();
        // should probably not be here
        this.Player.MakeArms();

        this.MateSelector.gameObject.SetActive(true);
        this.GetComponent<BlobGenerator>().Regenerate();
        this.gameObject.SetActive(false);
    }
}
