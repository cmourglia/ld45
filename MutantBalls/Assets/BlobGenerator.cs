using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobGenerator : MonoBehaviour
{
    public int nbBlobs = 100;
    public Bounds area = new Bounds(new Vector3(0, 0, 0), new Vector3(5, 5, 0));
    public GameObject BlobPrefab;

    private List<Blob> Blobs;

    void Start()
    {

    }

    public void Regenerate()
    {
        if (this.Blobs == null)
        {
            this.Blobs = new List<Blob>();
            for (var i = 0; i < nbBlobs; i++)
            {
                var blobGo = Instantiate(this.BlobPrefab, new Vector3(Random.Range(area.min.x, area.max.x), Random.Range(area.min.y, area.max.y), 0), Quaternion.identity);
                var blob = blobGo.GetComponent<Blob>();
                blob.IsAlive = false; // so they can be generated
                this.Blobs.Add(blob);
            }
        }

        foreach (var blob in this.Blobs)
        {
            if (blob.IsAlive)
            {
                blob.Heal();
                continue;
            }

            blob.IsAlive = true;
            blob.Color = Random.ColorHSV(0, 1, .5f, 1, .5f, 1);
            blob.Size = 4 + Random.value * 4;
            blob.HP = blob.Size;
            blob.AppendixUp = Mathf.RoundToInt(Random.Range(1, 5));
            blob.AppendixRight = Mathf.RoundToInt(Random.Range(1, 5));
            blob.AppendixDown = Mathf.RoundToInt(Random.Range(1, 5));
            blob.AppendixLeft = Mathf.RoundToInt(Random.Range(1, 5));
            blob.MakeArms();
            blob.gameObject.SetActive(true);

            blob.gameObject.AddComponent<Ennemy>();
        }
    }
}
