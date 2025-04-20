using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawning : MonoBehaviour
{
    public static Spawning Instance;

    public GameObject prefabToSpawn;
    public Transform spawnL;
    public Transform spawnR;
    public GameObject sittingPositions;
    private SittingPosition[] positions;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {
        positions = sittingPositions.GetComponentsInChildren<SittingPosition>();
    }

    void Update()
    {
    }
    bool AreThereAvailableSeats()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            if (positions[i].occupied == false)
            {
                return true;
            }
        }
        return false;
    }
    public GameObject ChooseRandomSpawnPoint()
    {
        int randomNumber = Random.Range(0, 1);
        if (randomNumber == 0)
        {
            return spawnL.gameObject;
        }
        else
        {
            return spawnL.gameObject;
        }
    }
    public GameObject ChooseARandomSeat()
    {
        while (true)
        {
            if(AreThereAvailableSeats())
            {
                int randomNumber = Random.Range(0, positions.Length);
                if (positions[randomNumber].occupied == false)
                {
                    return positions[randomNumber].gameObject;
                }
            }
            else
            { 
                return null;
            }
        }
    }

    public void SpawnNPC()
    {
        GameObject seat = ChooseARandomSeat();
        if (seat != null)
        {
            GameObject spawnlocaton = ChooseRandomSpawnPoint();
            GameObject obj = Instantiate<GameObject>(prefabToSpawn, spawnlocaton.transform.position, Quaternion.identity);
            NPC npc = obj.GetComponent<NPC>();
            
            npc.target = seat;
        }

    }

}
