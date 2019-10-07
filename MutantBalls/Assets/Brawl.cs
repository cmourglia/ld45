using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawl : MonoBehaviour
{
    public Blob Player;
    public Copulate CopulateManager;

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
    }

    void OnDisable()
    {
        foreach (var hurter in Object.FindObjectsOfType<HurtBlobs>())
        {
            hurter.Enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            this.CopulateManager.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
