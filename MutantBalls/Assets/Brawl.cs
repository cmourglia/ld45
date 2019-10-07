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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            this.CopulateManager.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }

        if (!this.Player.IsAlive)
        {
            this.Instructions.text = "loser.";
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
                gameObject.SetActive(false);
                CopulateManager.gameObject.SetActive(true);
            }
        }
    }
}
