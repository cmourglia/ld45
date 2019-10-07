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
    }
}
