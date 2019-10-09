using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Brawl : MonoBehaviour
{
    public Blob Player;
    public Copulate CopulateManager;
    public Text Instructions;

    public int MaxAliveBlobs;

    private Blob[] blobs;

    private float startTime;
    private bool started;
    private float copulateTimer;
    private bool copulateTimerStarted;

    void Awake()
    {
        this.OnDisable();
    }

    void OnEnable()
    {
        // should probably not be here
        this.Player.Heal();
        this.Player.MakeArms();

        Utils.SetAllMBEnabled<HurtBlobs>(true);
        Utils.SetAllMBEnabled<Ennemy>(true);

        copulateTimerStarted = false;
        blobs = Object.FindObjectsOfType<Blob>();

        this.Player.GetComponent<Movement>().enabled = false;
        this.startTime = Time.time + 3;
        this.started = false;
    }

    void OnDisable()
    {
        Utils.SetAllMBEnabled<HurtBlobs>(false);
        Utils.SetAllMBEnabled<Ennemy>(false);
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
            this.Instructions.text = "brawl!";
            this.started = true;
        }

        if (!this.Player.IsAlive)
        {
            this.Instructions.text = "loser.";
            return;
        }

        if (copulateTimerStarted)
        {
            copulateTimer -= Time.deltaTime;
            if (copulateTimer <= 0.0f)
            {
                copulateTimer = 0.0f;
                gameObject.SetActive(false);
                CopulateManager.gameObject.SetActive(true);
                return;
            }
        }

        if (this.copulateTimerStarted)
        {
            this.Instructions.text = "copulate in " + ((int)copulateTimer + 1) + "...";
        }
        else
        {
            int aliveBlobsCpt = 0;
            foreach (var blob in blobs)
            {
                if (blob.IsAlive)
                {
                    ++aliveBlobsCpt;
                }
            }

            if (aliveBlobsCpt < (int)((float)blobs.Length * 0.25f))
            {
                copulateTimerStarted = true;
                copulateTimer = 3.0f;
            }
        }
    }
}
