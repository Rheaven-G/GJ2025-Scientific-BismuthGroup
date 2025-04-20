using System.Collections.Generic;
using UnityEngine;
using static NPC;

public class NPC : MonoBehaviour
{
    public enum NPCState
    {
        Arriving,
        SittingBad,
        EffectedByPlant,
        SittingGood,
        Leaving,
        Left
    }
    public enum Emotion
    {
        Sad,
        Anger,
        Afraid,
    }

    public GameObject target;
    public float speed = 1.0f;
    public float leavingSpeedMultiplayer = 1.5f;
    public float arrivingSpeedRandomPersant = 1.5f;
    public float timeToFlipEmotion = 2.0f;
    public Emotion NPCEmotion;
    public float maxTimeToStation = 60;
    public float minTimeToStation = 20;
    //Debug
    public TextMesh debugText;

    private NPCState state = NPCState.Arriving;
    private NPCEmotion myEmotion;
    private List<Plant> effectingPlants = new List<Plant>(32);
    private float currentTimeUnderPlant = 0;
    private float timeToStation = 0;
    public float GetTimeToStation()
    { return timeToStation; }
    public NPCState GetNPCState()
    { return state; }
    void Start()
    {
        timeToStation = Random.Range(minTimeToStation, maxTimeToStation);
        myEmotion = gameObject.GetComponentInChildren<NPCEmotion>();
        myEmotion.SetInitialEmotion(NPCEmotion);
        myEmotion.gameObject.SetActive(false);

        speed = speed * Random.Range(1, arrivingSpeedRandomPersant); //Randomize the Speed a bit

    }

    // Update is called once per frame
    void Update()
    {
        debugText.text = state.ToString();
        switch (state)
        {
            case NPCState.Arriving:
                MoveToSittingPositing();
                break;
            case NPCState.EffectedByPlant:
                timeToStation -= Time.deltaTime;
                currentTimeUnderPlant += Time.deltaTime;
                debugText.text = currentTimeUnderPlant.ToString();
                if (currentTimeUnderPlant > timeToFlipEmotion)
                {
                    NPCFlippedEmotion();
                }
                break;
            case NPCState.SittingBad:
                timeToStation -= Time.deltaTime;
                break;
            case NPCState.Leaving:
                MoveToLeavingPositing();
                break;
        }


    }
    void Move()
    {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        transform.position = newPosition;
    }
    void MoveToSittingPositing()
    {
        Move();
        if ((transform.position - target.transform.position).magnitude < 0.01f)
        {
            NPCSatDown();
        }

    }

    void MoveToLeavingPositing()
    {
        Move();
        if ((transform.position - target.transform.position).magnitude < 0.01f)
        {
            NPCLeft();
        }

    }

    void NPCSatDown()
    {
        SittingPosition sitPos = target.GetComponent<SittingPosition>();
        if (sitPos.occupied == false)
        {
            sitPos.SetAsOccupied();
            state = NPCState.SittingBad;
            myEmotion.gameObject.SetActive(true);
            MetroManager.Instance.AddNPCToPassangers(this);

        }
        else
        {
            target = Spawning.Instance.ChooseARandomSeat();
            if (target == null)
            {
                NPCLeaving();
            }    
        }
    }
    public void NPCLeaving()
    {
        state = NPCState.Leaving;
        speed = speed * leavingSpeedMultiplayer;
        myEmotion.gameObject.SetActive(false);
        target = Spawning.Instance.ChooseRandomSpawnPoint();
    }
    void NPCLeft()
    {
        state = NPCState.Left;
        Destroy(gameObject);
    }
    void NPCEffectdByPlant()
    {
        state = NPCState.EffectedByPlant;
        myEmotion.SetEffectByPlant(NPCEmotion);
    }
    void NPCStopbeingEffectdByPlant()
    {
        state = NPCState.SittingBad;
        myEmotion.SetInitialEmotion(NPCEmotion);
        currentTimeUnderPlant = 0;
    }
    void NPCFlippedEmotion()
    {
        state = NPCState.SittingGood;
        myEmotion.SetGoodEmotion(NPCEmotion);
    }

    void OnTriggerEnter(Collider other)
    {
        if (state == NPCState.SittingBad && other.gameObject.CompareTag("PlantAura"))
        {
            Plant plant = other.GetComponentInParent<Plant>();
            if (NPCEmotion == plant.emotionCurer)
            {
                effectingPlants.Add(plant);
                if (state != NPCState.EffectedByPlant)
                {
                    NPCEffectdByPlant();
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (state == NPCState.EffectedByPlant && other.gameObject.CompareTag("PlantAura"))
        {
            Plant plant = other.GetComponentInParent<Plant>();
            if (NPCEmotion == plant.emotionCurer)
            {
                if(effectingPlants.Find(savedPlant => savedPlant == plant))
                {
                    effectingPlants.Remove(plant);
                }

                if(effectingPlants.Count == 0)
                {
                    NPCStopbeingEffectdByPlant();
                }
            }
        }
    }
}
