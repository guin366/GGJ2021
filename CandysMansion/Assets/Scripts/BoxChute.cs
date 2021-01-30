using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxChute : MonoBehaviour
{
    public bool isActive;
    public GameObject spawnPoint;
    public GameObject boxToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        if(boxToSpawn != null)
        {
            Instantiate(boxToSpawn, spawnPoint.transform.position, Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
