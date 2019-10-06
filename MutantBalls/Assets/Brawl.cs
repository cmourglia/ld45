using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawl : MonoBehaviour
{
    public List<HurtBlobs> Hurters;

    void OnEnable()
    {
        foreach (var hurter in this.Hurters)
        {
            hurter.Enabled = true;
        }
    }

    void OnDisable()
    {
        foreach (var hurter in this.Hurters)
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
