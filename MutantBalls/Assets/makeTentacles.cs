using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeTentacles : MonoBehaviour
{
    public int[] tentaclesDepth = new int[4];
    public GameObject tentacleBase;

    // Start is called before the first frame update
    void Start()
    {
        Vector3[] directions = new Vector3[4]{
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(-1, 0, 0),
            new Vector3(0, -1, 0),
        };

        for (int i = 0; i < 4; ++i)
        {
            var depth = tentaclesDepth[i];

            if (depth > 0)
            {
                var nodes = new GameObject[depth];

                Vector3 direction = directions[i];
                float scale = 0.2f;

                for (int d = 0; d < depth; ++d)
                {
                    nodes[d] = Instantiate(tentacleBase, direction * scale * (float)(d + 1), Quaternion.identity);
                }

                for (int d = 1; d < depth; ++d)
                {
                    nodes[d - 1].GetComponent<HingeJoint2D>().connectedBody = nodes[d].GetComponent<Rigidbody2D>();
                }

                nodes[depth - 1].GetComponent<HingeJoint2D>().enabled = false;

                GetComponents<HingeJoint2D>()[i].connectedBody = nodes[0].GetComponent<Rigidbody2D>();
            }
            else
            {
                GetComponents<HingeJoint2D>()[i].enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
