using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BlobGenerator))]
public class Copulate : MonoBehaviour
{
    public Blob Player;
    public MateSelector MateSelector;
    public Text Instructions;

    void OnEnable()
    {
        this.Player.Heal();
        // should probably not be here
        this.Player.MakeArms();

        this.MateSelector.gameObject.SetActive(true);
        this.GetComponent<BlobGenerator>().Regenerate();
        this.gameObject.SetActive(false);

        this.Instructions.text = "pick a mate";
    }
}
