using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;

public class HandController : MonoBehaviour
{
    public bool isRight;
    public float detectRange;
    public PhysicMaterial material;
    int boxLayer;
    Rigidbody rb;
    Collider grabbed;
    bool pressed = false;

    // Start is called before the first frame update
    void Awake()
    {
        boxLayer = LayerMask.NameToLayer("Box");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (grabbed != null && 
            Vector3.Distance(transform.position, grabbed.transform.position) > detectRange * 2f)
        {
            Ungrab(grabbed);
        }

        float grabAxis = InputManager.GetAxis("Grab");
        if (isRight ? grabAxis < 0.5f : grabAxis > -0.5f)
        {
            Ungrab(grabbed);
        }
        else if (grabbed == null)
        {
            //check for boxes in range
            Collider[] hits = new Collider[2];
            int count = Physics.OverlapSphereNonAlloc(transform.position, detectRange, hits, 1 << boxLayer);
            //check rt if right hand, otherwise check lt
            if (count > 0 && isRight ? grabAxis >= 0.5f : grabAxis <= -0.5f)
            {
                print("hit and rt?");
                Ungrab(grabbed);
                Grab(hits[0]);
            }
        }
    }

    void Ungrab(Collider coll)
    {
        if (coll != null && coll.GetComponent<Joint>())
        {
            Joint j = coll.GetComponent<Joint>();
            Destroy(j);
            grabbed = null;
        }
    }

    void Grab(Collider coll)
    {
        if (coll != null && coll.GetComponent<Joint>() == null)
        {
            print("grabbing + " + coll.gameObject.name);
            //coll.gameObject.transform.position = transform.position;
            Joint j = coll.gameObject.AddComponent<FixedJoint>();
            j.connectedBody = rb;
            grabbed = coll;
        }
    }
}
