using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentaculator : MonoBehaviour
{
    public int arms = 4;
    public int[] armDepths = new int[] { 3, 3, 3, 3 };
    public GameObject subNodePrefab;

    private List<HingeJoint2D> _hinges = new List<HingeJoint2D>();
    private List<GameObject> _armNodes = new List<GameObject>();
    private Vector3 _lastScale;

    // Start is called before the first frame update
    void Start()
    {
        _lastScale = this.transform.localScale;
        makeArms();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!this.transform.localScale.Equals(_lastScale))
        {
            makeArms();
            _lastScale = this.transform.localScale;
        }
    }

    public void destroyArms()
    {
        _armNodes.ForEach(Destroy);
        _hinges.ForEach(Destroy);
    }

    public void DestroyInSeconds(float seconds)
    {
        foreach (var arm in _armNodes)
        {
            Object.Destroy(arm, seconds);
        }
    }

    public void makeArms()
    {
        _armNodes.ForEach(Destroy);
        _hinges.ForEach(Destroy);

        var rb = GetComponent<Rigidbody2D>();

        // Add Hinges sources
        _hinges = new List<HingeJoint2D>(arms);
        float scale = this.transform.localScale.x * 0.05f;
        for (int i = 0; i < arms; ++i)
        {
            if (armDepths.Length < i + 1) { break; }
            var armDepth = armDepths[i];

            var hj = this.gameObject.AddComponent<HingeJoint2D>();
            _hinges.Add(hj);

            // Instantiate sub arm first node
            var dir = GetDirection(i);
            var pos = this.transform.position + dir * scale;
            var no = Instantiate(subNodePrefab, pos, Quaternion.identity);
            _armNodes.Add(no);

            var rb2 = no.GetComponent<Rigidbody2D>();
            //var hj2 = no.AddComponent<HingeJoint2D>();

            no.GetComponent<SpriteRenderer>().color = Color.red;

            hj.connectedBody = rb2;
            hj.useLimits = true;
            hj.breakForce = Mathf.Infinity;

            var jj = new JointAngleLimits2D();
            jj.min = 0.0f;
            jj.max = 0.0f;
            hj.limits = jj;

            // Add sub arms
            var snodes = new List<GameObject>(armDepth);
            var sradius = subNodePrefab.GetComponent<CircleCollider2D>().radius;
            for (int j = 0; j < armDepth; ++j)
            {
                var sno = Instantiate(subNodePrefab, pos + (j + 1) * 2.0f * sradius * dir.normalized /** scale / 5*/, Quaternion.identity);
                snodes.Add(sno);
                _armNodes.Add(sno);
            }

            // Join first joint with first sub arm
            if (armDepth > 0)
            {
                var phj = no.AddComponent<HingeJoint2D>();
                _hinges.Add(phj);
                phj.useLimits = true;
                phj.breakForce = Mathf.Infinity;

                var sno = snodes[0];
                var srb = sno.GetComponent<Rigidbody2D>();

                var sjj = new JointAngleLimits2D();
                sjj.min = -25.0f;
                sjj.max = 25.0f;
                phj.limits = sjj;

                phj.connectedBody = srb;
            }

            // Connect sub arms with each other
            for (int j = 1; j < armDepth; ++j)
            {
                var sno1 = snodes[j - 1];
                var sno2 = snodes[j];

                var hj1 = sno1.AddComponent<HingeJoint2D>();
                _hinges.Add(hj1);
                var srb2 = sno2.GetComponent<Rigidbody2D>();

                var sjj = new JointAngleLimits2D();
                sjj.min = -25.0f;
                sjj.max = 25.0f;
                hj1.limits = sjj;
                hj1.breakForce = Mathf.Infinity;

                hj1.connectedBody = srb2;
            }
        }


    }

    static Vector3 GetDirection(int i)
    {
        return i == 0 ? new Vector3(1, 0, 0) :
               i == 1 ? new Vector3(0, 1, 0) :
               i == 2 ? new Vector3(-1, 0, 0) :
                        new Vector3(0, -1, 0);
    }
}
