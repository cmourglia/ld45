using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Tentaculator : MonoBehaviour
{
    public int arms = 4;
    public int[] armDepths = new int[] { 3, 3, 3, 3 };
    public GameObject subNodePrefab;

    private static PrefabPool pool = new PrefabPool();

    private List<GameObject> _armNodes = new List<GameObject>();
    private Vector3 _lastScale;

    // Start is called before the first frame update
    void Start()
    {
        if (this.subNodePrefab.GetComponent<HingeJoint2D>() == null
            || this.subNodePrefab.GetComponent<Rigidbody2D>() == null
            || this.subNodePrefab.GetComponent<SpriteRenderer>() == null)
        {
            Debug.LogError($"{this.subNodePrefab.name} must have SpriteRenderer, HingeJoint2D and RigidBody2D components");
            this.gameObject.SetActive(false);
        }
        _lastScale = this.transform.localScale;
        makeArms();
    }

    void LateUpdate()
    {
        if (!this.transform.localScale.Equals(_lastScale))
        {
            makeArms();
            _lastScale = this.transform.localScale;
        }
    }

    public void DestroyInSeconds(float seconds)
    {
        // todo use delay
        foreach (var hj in this.GetComponents<HingeJoint2D>())
        {
            hj.connectedBody = null;
            hj.enabled = false;
        }
        foreach (var arm in _armNodes)
        {
            var hj = arm.GetComponent<HingeJoint2D>();
            hj.connectedBody = null;
            hj.enabled = false;
            arm.GetComponent<Rigidbody2D>();
            pool.Release(subNodePrefab, arm);
        }
        _armNodes.Clear();
    }

    public void makeArms()
    {
        this.DestroyInSeconds(0);

        var rb = GetComponent<Rigidbody2D>();

        var hinges = this.GetComponents<HingeJoint2D>().ToList();
        for (int i = hinges.Count; i < arms; ++i)
        {
            hinges.Add(this.gameObject.AddComponent<HingeJoint2D>());
        }

        // Add Hinges sources
        float scale = this.transform.localScale.x * 0.05f;
        for (int i = 0; i < arms; ++i)
        {
            if (armDepths.Length < i + 1) { break; }
            var armDepth = armDepths[i];

            var hj = hinges[i];
            hj.enabled = true;

            // Instantiate sub arm first node
            var dir = GetDirection(i);
            var pos = this.transform.position + dir * scale;

            var no = pool.GetInstance(subNodePrefab);
            no.transform.position = pos;
            no.transform.rotation = Quaternion.identity;
            _armNodes.Add(no);

            var rb2 = no.GetComponent<Rigidbody2D>();
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
                var sno = pool.GetInstance(subNodePrefab);
                sno.transform.position = pos + (j + 1) * 2.0f * sradius * dir.normalized /** scale / 5*/;
                sno.transform.rotation = Quaternion.identity;
                sno.GetComponent<SpriteRenderer>().color = Color.white;
                snodes.Add(sno);
                _armNodes.Add(sno);
            }

            var phj = no.GetComponent<HingeJoint2D>();
            // Join first joint with first sub arm
            if (armDepth == 0)
            {
                phj.enabled = false;
            }
            else
            {
                phj.enabled = true;
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

                var hj1 = sno1.GetComponent<HingeJoint2D>();
                var srb2 = sno2.GetComponent<Rigidbody2D>();

                var sjj = new JointAngleLimits2D();
                sjj.min = -25.0f;
                sjj.max = 25.0f;

                hj1.limits = sjj;
                hj1.breakForce = Mathf.Infinity;
                hj1.connectedBody = srb2;
                hj1.enabled = true;

                // just to be safe
                var hj2 = sno2.GetComponent<HingeJoint2D>();
                hj2.connectedBody = null;
                hj2.enabled = false;
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
