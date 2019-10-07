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

        foreach (var hurter in Object.FindObjectsOfType<HurtBlobs>())
        {
            hurter.Enabled = true;
        }
        foreach (var mb in Object.FindObjectsOfType<Ennemy>())
        {
            mb.enabled = true;
        }

        this.Instructions.text = "brawl!";

        blobs = Object.FindObjectsOfType<Blob>();
        copulateTimerStarted = false;
    }

    void OnDisable()
    {
        foreach (var hurter in Object.FindObjectsOfType<HurtBlobs>())
        {
            hurter.Enabled = false;
        }
        foreach (var mb in Object.FindObjectsOfType<Ennemy>())
        {
            mb.enabled = false;
        }
    }

    void Update()
    {
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
