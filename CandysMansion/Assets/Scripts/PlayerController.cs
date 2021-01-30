using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;

public class PlayerController : MonoBehaviour
{
    public static PlayerController singleton;
    public GameObject cameraAnchor;
    public float speed;
    public float cameraSensitivity;
    public Vector2 drag = new Vector2(10f, 0f);
    Rigidbody rb;
    public float jumpVel;
    public float gravity = 20f;

    // Start is called before the first frame update
    void Awake()
    {
        singleton = this;
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        cameraAnchor.transform.rotation = Quaternion.identity;
        cameraAnchor.transform.Rotate(0f, transform.rotation.y, 0f, Space.World);
    }

    private void Update()
    {
        //perform jump if on ground
        if (InputManager.GetButtonDown("Jump") && OnGround())
        {
            print("jump?");
            rb.velocity = new Vector3(rb.velocity.x, jumpVel, rb.velocity.z);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //manual drag implementation
        Vector3 newv = rb.velocity;
        newv.x *= 1 - Time.fixedDeltaTime * drag.x;
        newv.y *= 1 - Time.fixedDeltaTime * drag.y;
        newv.z *= 1 - Time.fixedDeltaTime * drag.x;
        rb.velocity = newv;

        //manipulate camera
        float camx = InputManager.GetAxis("CameraH");
        float camy = InputManager.GetAxis("CameraV");
        transform.Rotate(0f, camx * cameraSensitivity, 0f);
        cameraAnchor.transform.Rotate(new Vector3(0f, camx * cameraSensitivity, 0f), Space.World);
        if (!((Mathf.DeltaAngle(0f, Camera.main.transform.rotation.eulerAngles.x) >= 75 && camy > 0f) || 
            (Mathf.DeltaAngle(0f,Camera.main.transform.rotation.eulerAngles.x) <= -75f && camy < 0f)))
            cameraAnchor.transform.Rotate(new Vector3(camy * cameraSensitivity, 0f, 0f), Space.Self);

        //move player (and camera anchor)
        float x = InputManager.GetAxis("Horizontal");
        float z = InputManager.GetAxis("Vertical");
        rb.AddForce(transform.rotation * (new Vector3(x, 0f, z)) * speed);
        cameraAnchor.transform.position = transform.position;

        //apply gravity
        rb.AddForce(Vector3.down * gravity * 60f * Time.fixedDeltaTime);
    }

    bool OnGround()
    {
        return true;
    }
}
