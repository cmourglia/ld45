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
    public Text LoopNumber;

    int loopNumber = 0;

    void OnEnable()
    {
        this.Player.Heal();
        // should probably not be here
        this.Player.MakeArms();

        this.MateSelector.gameObject.SetActive(true);
        this.GetComponent<BlobGenerator>().Regenerate();
        this.gameObject.SetActive(false);

        foreach (var mb in Object.FindObjectsOfType<FindMate>())
        {
            mb.enabled = true;
        }

        this.Instructions.text = "pick a mate";
        this.loopNumber++;
        this.LoopNumber.text = $"loop #{this.loopNumber}";
    }

}
