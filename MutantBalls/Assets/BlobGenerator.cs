using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobGenerator : MonoBehaviour
{
    public int nbBlobs = 100;
    public Bounds area = new Bounds(new Vector3(0, 0, 0), new Vector3(5, 5, 0));
    public GameObject BlobPrefab;


    void Start()
    {
        Debug.Log(area.min);
        for (var i = 0; i < nbBlobs; i++)
        {
            var blobGo = Instantiate(this.BlobPrefab, new Vector3(Random.Range(area.min.x, area.max.x), Random.Range(area.min.y, area.max.y), 0), Quaternion.identity);
            var blob = blobGo.GetComponent<Blob>();

            blob.Color = Random.ColorHSV(0, 1, .5f, 1, .5f, 1);
            blob.Size = 1 + Random.value * 4;
            blob.HP = blob.Size;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
