﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Brawl : MonoBehaviour
{
    public Blob Player;
    public Copulate CopulateManager;
    public Text Instructions;
    public SpriteRenderer Background;

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
        this.Background.color = new Color(0xFF / 255f, 0xCA / 255f, 0xB8 / 255f, 1);

        // should probably not be here
        this.Player.Heal();
        this.Player.MakeArms();

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

    void FixedUpdate()
    {
        if (!this.started)
        {
            foreach (var blob in blobs)
            {
                var rb = blob.GetComponent<Rigidbody2D>();
                rb.velocity = Vector3.zero;
                rb.angularVelocity = 0;
            }
        }
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
            Utils.SetAllMBEnabled<HurtBlobs>(true);
            Utils.SetAllMBEnabled<Ennemy>(true);
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
