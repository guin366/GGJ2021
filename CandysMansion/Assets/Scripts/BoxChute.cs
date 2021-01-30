using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxChute : MonoBehaviour
{
    public bool isActive;
    public GameObject spawnPoint;
    public GameObject boxToSpawn;
    public float spawnCD = 0.5f;

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
            GameObject spawnedObject = Instantiate(boxToSpawn, spawnPoint.transform.position, Random.rotation);
            Rigidbody spawnedRb = spawnedObject.GetComponent<Rigidbody>();
            spawnedRb.AddTorque(new Vector3(Random.Range(0.0f, 100.0f), Random.Range(0.0f, 100.0f), Random.Range(0.0f, 100.0f)));
            yield return new WaitForSeconds(spawnCD);

        }
    }
}
