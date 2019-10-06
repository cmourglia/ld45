using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawl : MonoBehaviour
{
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
            this.gameObject.SetActive(false);
        }
    }
}
