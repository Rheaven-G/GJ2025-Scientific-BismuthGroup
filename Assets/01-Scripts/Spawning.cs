using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawning : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform spawnL;
    public Transform spawnR;
    public GameObject[] positions;
    public GameObject newObjectL;


    public int spawnNumber = 2;
    private int randomNumber = 0;


    void Start()
    {
        GenerateNumber();
        newObjectL = Instantiate<GameObject>(prefabToSpawn, spawnL.position, Quaternion.identity);
        newObjectL.GetComponent<NPCMovement>().target = positions[randomNumber];
        //GameObject newObjectR = Instantiate<GameObject>(prefabToSpawn, spawnR.position, Quaternion.identity);
    }

    void Update()
    {
    }

    void GenerateNumber()
    {
        randomNumber = Random.Range(0, 2);
    }

    void Spawn()
    {

    }

}
