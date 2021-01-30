using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public GameObject forceDirection;
    public float jumpForce = 25.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //other.attachedRigidbody.AddForce(this.transform.up * jumpForce);
        other.attachedRigidbody.velocity = this.transform.up * jumpForce;
    }
}
