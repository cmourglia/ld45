using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum Orientation
{
    East,
    North,
    West,
    South,
};

//public class Toto : MonoBehaviour
//{
//    public Orientation orientation;
//    private Vector3 direction;

//    // Start is called before the first frame update
//    void Start()
//    {
//        direction = GetDirection(orientation);
//        // mettre le rigibody en kinematic et soit piloté par son parent (va la chercher a chaque update)        


//        var r = GetComponent<CircleCollider2D>().radius;
//        transform.localPosition = direction * transform.parent.localScale.x * 0.01f - new Vector3(r, r, r);
//    }

//    static Vector3 GetDirection(Orientation orientation)
//    {
//        switch (orientation)
//        {
//            case Orientation.East:
//                return new Vector3(1, 0, 0);
//            case Orientation.North:
//                return new Vector3(0, 1, 0);
//            case Orientation.West:
//                return new Vector3(-1, 0, 0);
//            case Orientation.South:
//                return new Vector3(0, -1, 0);
//        }

//        return new Vector3();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        var r = GetComponent<CircleCollider2D>().radius;
//        transform.localPosition = direction * transform.parent.localScale.x * 0.01f - new Vector3(r, r, r);
//    }

public class makeTentacles : MonoBehaviour
{
    public int[] tentaclesDepth = new int[4];
    public GameObject tentacleBase;

    public GameObject north, south, east, west;
    private Rigidbody2D[] subBodies = new Rigidbody2D[4];
    private Vector3 _lastScale;

    private void Start()
    {
        var b = GetComponent<Blob>();
        var c = GetComponent<CircleCollider2D>();
        for (int i = 0; i < 4; ++i)
        {
            var dir = GetDirection(i);
            var depth = tentaclesDepth[i];

            if (depth > 0)
            {
                var nodes = new GameObject[depth];

                Vector3 direction = GetDirection(i);
                float scale = b.Size / 10.0f;

                for (int d = 0; d < depth; ++d)
                {
                    var pos = this.transform.localPosition + dir.normalized * c.radius;
                    nodes[d] = Instantiate(tentacleBase, pos, Quaternion.identity);
                }

                for (int d = 1; d < depth; ++d)
                {
                    nodes[d - 1].GetComponent<HingeJoint2D>().connectedBody = nodes[d].GetComponent<Rigidbody2D>();
                }

                nodes[depth - 1].GetComponent<HingeJoint2D>().enabled = false;

                GetComponent<HingeJoint2D>().connectedBody = nodes[0].GetComponent<Rigidbody2D>();


                subBodies[i] = nodes[0].GetComponent<Rigidbody2D>();
            }
            else
            {
                //GetComponent<HingeJoint2D>().enabled = false;
            }
        }
    }

    //// Start is called before the first frame update
    //void Start()
    //{
    //    var b = GetComponent<Blob>();
    //    var c = GetComponent<CircleCollider2D>();

    //    _lastScale = this.transform.localScale;

    //    for (int i = 0; i < 4; ++i)
    //    {
    //        var targetBody = i == 0 ? east :
    //                         i == 1 ? north :
    //                         i == 2 ? west :
    //                                  south;
    //        var depth = tentaclesDepth[i];

    //        if (depth > 0)
    //        {
    //            var nodes = new GameObject[depth];

    //            Vector3 direction = GetDirection(i);
    //            float scale = b.Size / 10.0f;

    //            for (int d = 0; d < depth; ++d)
    //            {
    //                nodes[d] = Instantiate(tentacleBase, targetBody.transform.position, Quaternion.identity);
    //            }

    //            for (int d = 1; d < depth; ++d)
    //            {
    //                nodes[d - 1].GetComponent<HingeJoint2D>().connectedBody = nodes[d].GetComponent<Rigidbody2D>();
    //            }

    //            nodes[depth - 1].GetComponent<HingeJoint2D>().enabled = false;

    //            targetBody.GetComponent<HingeJoint2D>().connectedBody = nodes[0].GetComponent<Rigidbody2D>();


    //            subBodies[i] = nodes[0].GetComponent<Rigidbody2D>();
    //        }
    //        else
    //        {
    //            targetBody.GetComponent<HingeJoint2D>().enabled = false;
    //        }
    //    }

    //    //Vector3[] directions = new Vector3[4]{
    //    //    new Vector3(1, 0, 0),
    //    //    new Vector3(0, 1, 0),
    //    //    new Vector3(-1, 0, 0),
    //    //    new Vector3(0, -1, 0),
    //    //};

    //    //var b = GetComponent<Blob>();

    //    //for (int i = 0; i < 4; ++i)
    //    //{
    //    //    var targetBody = i == 0 ? east :
    //    //                     i == 1 ? north :
    //    //                     i == 2 ? west :
    //    //                              south;
    //    //    var depth = tentaclesDepth[i];

    //    //    if (depth > 0)
    //    //    {
    //    //        var nodes = new GameObject[depth];

    //    //        Vector3 direction = directions[i];
    //    //        float scale = b.Size / 10.0f;

    //    //        for (int d = 0; d < depth; ++d)
    //    //        {
    //    //            nodes[d] = Instantiate(tentacleBase, targetBody.transform.position, Quaternion.identity);
    //    //        }

    //    //        for (int d = 1; d < depth; ++d)
    //    //        {
    //    //            nodes[d - 1].GetComponent<HingeJoint2D>().connectedBody = nodes[d].GetComponent<Rigidbody2D>();
    //    //        }

    //    //        nodes[depth - 1].GetComponent<HingeJoint2D>().enabled = false;

    //    //        targetBody.GetComponent<HingeJoint2D>().connectedBody = nodes[0].GetComponent<Rigidbody2D>();
    //    //    }
    //    //    else
    //    //    {
    //    //        targetBody.GetComponent<HingeJoint2D>().enabled = false;
    //    //    }
    //    //}
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (!this.transform.localScale.Equals(_lastScale))
    //    {
    //        var s = this.transform.localScale;
    //        _lastScale = s;
    //        var r = GetComponent<CircleCollider2D>().radius;
    //        //transform.localPosition = direction * transform.parent.localScale.x * 0.01f - new Vector3(r, r, r);
    //        for (int i = 0; i < 4; ++i)
    //        {
    //            if (subBodies[i] != null)
    //            {
    //                var targetBody = i == 0 ? east :
    //                                 i == 1 ? north :
    //                                 i == 2 ? west :
    //                                          south;

    //                var hj = targetBody.GetComponent<HingeJoint2D>();
    //                hj.autoConfigureConnectedAnchor = false;
    //                hj.anchor.Set(0, 0);
    //                //var d = GetDirection(i);
    //                //subBodies[i].gameObject.transform.localPosition = d * s.x * 0.01f - new Vector3(r, r, r);

    //                //subBodies[i].MovePosition(this.transform.localPosition + targetBody.transform.localPosition);
    //                //subBodies[i].transform.localScale = thitargetBody.transform.localScale;

    //            }
    //        }
    //    }
    //}

    static Vector3 GetDirection(int i)
    {
        var o = i == 0 ? Orientation.East :
                         i == 1 ? Orientation.North :
                         i == 2 ? Orientation.West :
                                  Orientation.South ;
        return GetDirection(o);
    }
    static Vector3 GetDirection(Orientation orientation)
    {
        switch (orientation)
        {
            case Orientation.East:
                return new Vector3(1, 0, 0);
            case Orientation.North:
                return new Vector3(0, 1, 0);
            case Orientation.West:
                return new Vector3(-1, 0, 0);
            case Orientation.South:
                return new Vector3(0, -1, 0);
        }

        return new Vector3();
    }
}
