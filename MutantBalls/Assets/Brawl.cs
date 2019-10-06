using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawl : MonoBehaviour
{
    public Copulate CopulateManager;

    void Awake()
    {
        this.OnDisable();
    }

    void OnEnable()
    {
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
