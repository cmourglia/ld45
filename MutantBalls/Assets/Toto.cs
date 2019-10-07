//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[Serializable]
//public enum Orientation
//{
//    East, 
//    North, 
//    West, 
//    South,
//};

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
//        transform.localPosition = direction * transform.parent.localScale.x * 0.01f - new Vector3(r,r,r);
//    }
//}
