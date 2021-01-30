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
        StartCoroutine(SpawnBox());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnBox()
    {
        while(true)
        {
            Instantiate(boxToSpawn, spawnPoint.transform.position, Random.rotation);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
