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

        // Instantiate enough hinges
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

            // Instantiate sub arm first node
            var dir = getDirection(i);
            var pos = this.transform.position + dir * scale;

            var rootNode = pool.GetInstance(subNodePrefab);
            rootNode.transform.position = pos;
            rootNode.transform.rotation = Quaternion.identity;
            _armNodes.Add(rootNode);

            rootNode.GetComponent<SpriteRenderer>().color = Color.red;

            var armHJ = hinges[i];
            configureHingeJoint(armHJ, rootNode.GetComponent<Rigidbody2D>(), 0f, 0f);

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

            var phj = rootNode.GetComponent<HingeJoint2D>();
            if (armDepth <= 0)
            {
                phj.enabled = false;
                break;
            }

            // Join first joint with first sub arm
            configureHingeJoint(phj, snodes.First().GetComponent<Rigidbody2D>());

            // Connect sub arms with each other
            for (int j = 1; j < armDepth; ++j)
            {
                var previousNode = snodes[j - 1];
                var node = snodes[j];
                configureHingeJoint(previousNode.GetComponent<HingeJoint2D>(), node.GetComponent<Rigidbody2D>());
            }

            // just to be safe
            var lastHJ = snodes.Last().GetComponent<HingeJoint2D>();
            lastHJ.connectedBody = null;
            lastHJ.enabled = false;
        }
    }

    private static void configureHingeJoint(HingeJoint2D joint, Rigidbody2D connectedBody, float min = -25f, float max = 25f)
    {
        joint.limits = new JointAngleLimits2D()
        {
            min = min,
            max = max,
        };
        joint.useLimits = true;
        joint.breakForce = Mathf.Infinity;
        joint.connectedBody = connectedBody;
        joint.enabled = true;
    }

    private static Vector3 getDirection(int i)
    {
        return i == 0 ? new Vector3(1, 0, 0) :
               i == 1 ? new Vector3(0, 1, 0) :
               i == 2 ? new Vector3(-1, 0, 0) :
                        new Vector3(0, -1, 0);
    }
}
