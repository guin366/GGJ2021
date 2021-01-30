using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;

public class PlayerController : MonoBehaviour
{
    public static PlayerController singleton;
    public float speed;
    Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        singleton = this;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = InputManager.GetAxis("Horizontal");
        float z = InputManager.GetAxis("Vertical");

        rb.AddForce(new Vector3(x, 0f, z)*speed);


    }
}
