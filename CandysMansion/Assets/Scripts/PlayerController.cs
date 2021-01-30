using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;

public class PlayerController : MonoBehaviour
{
    public static PlayerController singleton;
    public GameObject camera;
    public float speed;
    public float cameraSensitivity;
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
        //manipulate camera
        float camx = InputManager.GetAxis("CameraH");
        float camy = InputManager.GetAxis("CameraV");
        transform.Rotate(0f, camx * cameraSensitivity, 0f);
        camera.transform.Rotate(new Vector3(0f, camx * cameraSensitivity, 0f), Space.World);
        camera.transform.Rotate(new Vector3(camy * cameraSensitivity, 0f, 0f), Space.Self);

        //move player (and camera anchor)
        float x = InputManager.GetAxis("Horizontal");
        float z = InputManager.GetAxis("Vertical");
        rb.AddForce(transform.rotation*(new Vector3(x, 0f, z)) * speed);
        camera.transform.position = transform.position;

        bool leftGrabbing = InputManager.GetAxis("Grab") <= -0.5f;
        bool rightGrabbing = InputManager.GetAxis("Grab") >= 0.5f;
    }
}
