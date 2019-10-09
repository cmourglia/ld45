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
    public CopulatePopup SelectPopup;

    private int loopNumber = 0;
    private float startTime;
    private bool started;

    void OnEnable()
    {
        this.Player.Heal();
        // should probably not be here
        this.Player.MakeArms();

        this.MateSelector.gameObject.SetActive(true);
        this.GetComponent<BlobGenerator>().Regenerate();

        Utils.SetAllMBEnabled<HurtBlobs>(false);
        Utils.SetAllMBEnabled<FindMate>(true);

        this.loopNumber++;
        this.LoopNumber.text = $"loop #{this.loopNumber}";

        this.Player.GetComponent<Movement>().enabled = false;
        this.startTime = Time.time + 3;
        this.started = false;
    }

    void Update()
    {
        if (Time.time < startTime)
        {
            this.Instructions.text = (startTime - Time.time).ToString("F");
            return;
        }

        if (!this.started)
        {
            this.Player.GetComponent<Movement>().enabled = true;
            this.Instructions.text = "pick a mate";
            this.started = true;
        }
    }

    public void SelectMate(Blob target)
    {
        var playerBlob = this.Player.GetComponent<Blob>();
        var child = this.SelectPopup.Show(playerBlob, target);

        child.CopyTo(playerBlob);
        playerBlob.Heal(); // since its health changed

        Utils.SetAllMBEnabled<FindMate>(false);

        this.gameObject.SetActive(false);
    }
}
