using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public float currentTime;
    public GameObject trigger;
    private bool active;

    void Update()
    {
        if(active)
        {
            if(currentTime > 0)
            {
               currentTime -= Time.deltaTime; 
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(active)
        {
            active = false;
            //check if box is held by player
            //win
        }
        else
        {
            active = true;
        }
    }
}
