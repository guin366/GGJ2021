using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour
{
    public float currentTime = 100;
    public GameObject trigger;
    public Text text;
    public bool active = true;
    public Loader.Scene currentScene;
    public Loader.Scene nextScene;

    void Update()
    {
        if(active)
        {
            if(currentTime > 0)
            {
               currentTime -= Time.deltaTime; 
            }
        }
        text.text = currentTime.ToString("0.00");

        if(currentTime <= 0f)
        {
            //text.text = "The package is now lost forever!";
            Loader.Load(currentScene);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(active && other.tag.Equals("target"))
        {
            active = false;
            Loader.Load(nextScene);
        }
    }
}
