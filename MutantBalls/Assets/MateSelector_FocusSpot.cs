using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MateSelector_FocusSpot : MonoBehaviour
{
    public Blob blob;
    public SpriteRenderer idCard;
    public TextMeshPro lifeText;
    public TextMeshPro nodesText;
    public TextMeshPro skillsText;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public void Select(Blob blobToSelect)
    {
        var noneSelected = (blobToSelect == null);
        //this.gameObject.SetActive(!noneSelected);
        idCard.gameObject.SetActive(!noneSelected);
        blob.gameObject.SetActive(!noneSelected);


        if (noneSelected)
            return;

        blob.Color = blobToSelect.Color;
        blob.Size = blobToSelect.Size;

        if (idCard != null)
        {
            idCard.gameObject.SetActive(true);
            
            if (lifeText != null)
            {
                //var hpStr = blobToSelect.HP.ToString();
                //nodesText.text = blobToSelect.Nodes;
                //skillsText.text = 
            }
        }

        this.gameObject.SetActive(true);
        // blob Scale is maximal draw area
    }
}
