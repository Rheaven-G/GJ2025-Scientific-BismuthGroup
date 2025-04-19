using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawning : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform spawnL;
    public Transform spawnR;


    void Start()
    {
        GameObject newObjectL = Instantiate<GameObject>(prefabToSpawn, spawnL.position, Quaternion.identity);
        GameObject newObjectR = Instantiate<GameObject>(prefabToSpawn, spawnR.position, Quaternion.identity);
    }

    void Update()
    {
        
    }
}
