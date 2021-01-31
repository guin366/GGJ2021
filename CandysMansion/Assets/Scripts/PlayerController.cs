using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController singleton;
    public GameObject cameraAnchorPrefab;
    GameObject cameraAnchor;

//    public GameObject camera;
    public float speed;
    public float cameraSensitivity;
    public Vector2 drag = new Vector2(10f, 0f);
    Rigidbody rb;
    public float jumpVel;
    public float gravity = 20f;

    public GameObject pauseMenuPrefab;
    GameObject pauseMenu;
    CapsuleCollider coll;
    float cameraOffset = 0f;
    int floorMask;
    int boxMask;
    int wallMask;

    // Start is called before the first frame update
    void Awake()
    {
        singleton = this;
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();
        floorMask = LayerMask.NameToLayer("Floor");
        boxMask = LayerMask.NameToLayer("Box");
        wallMask = LayerMask.NameToLayer("Wall");
    }

    private void OnEnable()
    {
        //there can only be one
        foreach (Camera camera in FindObjectsOfType<Camera>())
        {
            Destroy(camera.gameObject);
        }
        cameraAnchor = Instantiate(cameraAnchorPrefab, transform.position, Quaternion.identity);
        cameraAnchor.transform.Rotate(0f, transform.rotation.y, 0f, Space.World);
        cameraOffset = Vector3.Distance(cameraAnchor.GetComponentInChildren<Camera>().transform.position,
            transform.position);

        pauseMenu = Instantiate(pauseMenuPrefab);
        pauseMenu.GetComponentInChildren<Button>().onClick.AddListener(() => Application.Quit());
        pauseMenu.SetActive(false);
    }

    private void OnDisable()
    {
        Destroy(cameraAnchor);
    }

    private void Update()
    {
        //perform jump if on ground
        if (InputManager.GetButtonDown("Jump") && OnGround())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpVel, rb.velocity.z);
        }

        //hide and center mouse while playing
        if (Time.timeScale > 0f)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        //pause game
        if (InputManager.GetButtonDown("Pause"))
        {
            if (pauseMenu.activeInHierarchy)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
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
        transform.Rotate(0f, camx *cameraSensitivity, 0f);
        GameObject camera = Camera.main.gameObject;
        cameraAnchor.transform.Rotate(new Vector3(0f, camx*cameraSensitivity, 0f), Space.World);
        if (!((Mathf.DeltaAngle(0f, camera.transform.rotation.eulerAngles.x) >= 75 && camy > 0f) || 
           (Mathf.DeltaAngle(0f,camera.transform.rotation.eulerAngles.x) <= -75f && camy < 0f)))
            cameraAnchor.transform.Rotate(new Vector3(camy * cameraSensitivity, 0f, 0f), Space.Self);

        //zoom camera in if it's hitting something
        RaycastHit[] hits = new RaycastHit[2];
        Vector3 dir = (camera.transform.position - transform.position).normalized;
        Camera.main.transform.position = transform.position + dir * cameraOffset;
        if (Physics.RaycastNonAlloc(camera.transform.position, -dir, hits,
            Vector3.Distance(camera.transform.position, transform.position), 
            1<<wallMask|1<<floorMask) > 0)
        {
            Camera.main.transform.position -= dir * (hits[0].distance + 3f);
        }
        
        //move player (and camera anchor)
        float x = InputManager.GetAxis("Horizontal");
        float z = InputManager.GetAxis("Vertical");
        rb.AddForce(transform.rotation * (new Vector3(x, 0f, z)) * speed);
        cameraAnchor.transform.position = transform.position;

        //apply gravity
        rb.AddForce(Vector3.down * gravity * 60f * Time.fixedDeltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (GetComponent<Collider>().bounds.extents.y + 1f));
    }

    bool OnGround()
    {
        print(transform.position + Vector3.down * coll.bounds.extents.y);
        bool check = Physics.Raycast(transform.position,
            Vector3.down,
            coll.bounds.extents.y + 1f);
        print(check);
        return check;
    }
}
