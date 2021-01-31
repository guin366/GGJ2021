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
    public float throwForce = 500f;

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

        string grabName = isRight ? "GrabR" : "GrabL";
        float grabAxis = InputManager.GetAxis(grabName);
        if (grabAxis < 0.5f)
        {
            Ungrab(grabbed);
        }
        else if (grabbed == null)
        {
            //check for boxes in range
            Collider[] hits = new Collider[2];
            int count = Physics.OverlapSphereNonAlloc(transform.position, detectRange, hits, 1 << boxLayer);
            //check rt if right hand, otherwise check lt
            if (count > 0)
            {
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
            grabbed.GetComponent<BombBox>().grabbed = false;
            grabbed.GetComponent<BombBox>().thrown = true;
            grabbed = null;
            StartCoroutine(Throw(coll.attachedRigidbody));
        }
    }

    IEnumerator Throw(Rigidbody proj)
    {
        for(int i=0; i<2; i++)
        {
            proj.AddForce((Camera.main.transform.forward + new Vector3(0,0.5f,0)).normalized * throwForce);
            yield return new WaitForFixedUpdate();
        }
    }

    void Grab(Collider coll)
    {
        if (coll != null && coll.GetComponent<Joint>() == null)
        {
            //coll.gameObject.transform.position = transform.position;
            Joint j = coll.gameObject.AddComponent<FixedJoint>();
            j.connectedBody = rb;
            grabbed = coll;
            grabbed.GetComponent<BombBox>().grabbed = true;
        }
    }
}
