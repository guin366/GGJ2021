using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{

    private Rigidbody rb;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
         if(rb.velocity.magnitude >= 2f && !audio.isPlaying)
        {
            audio.Play();
        }
    }
}
