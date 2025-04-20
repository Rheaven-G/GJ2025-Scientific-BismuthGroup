using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MetroManager : MonoBehaviour
{
    // So there can only be one MetroManager
    public static MetroManager Instance;

    #region Proterties
    [Header("Metro Properties")]
    public uint[] stopInterval = new uint[] { 24, 64 };
    public uint stationStopDuration = 5;
    public float acceleration = 0.015f;
    public float currentSpeed;
    public float maxSpeed = 3f;
    public int minArrivalPassangers = 3;
    public int maxArrivalPassangers = 7;


    [Header("Interactions")]
    public Material windowMaterials;

    // Private vars
    public bool IsRolling { get; private set; }
    private bool IsStopped = false;

    [Header("Debug")]
    public GameObject metroSpeedIndication;
    #endregion
    private List<NPC> passengers = new List<NPC>(64);

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        StartCoroutine(Drive());
    }

    void Update()
    {
        // Debug :: sphere on screen to know if the metro is rolling
        metroSpeedIndication.gameObject.SetActive(!IsRolling);

        // Update shaderspeed
        windowMaterials.SetFloat("_Speed", currentSpeed);
    }

    void FixedUpdate()
    {
        if (IsRolling)
        {
            Accelerate();
        }
        else if (!IsRolling && currentSpeed > 0)
        {
            SlowDown();
        }
        else // if(IsStopped == false)
        {
            StartCoroutine(Stop());
        }

        // We don't want the metro to keep accelerating
        LimitSpeed();
    }


    // Interactions
    void Accelerate() // Meant for FixedUpdate()
    {
        // This Method adds to the currentSpeed

        currentSpeed += acceleration;
    }

    void SlowDown() // Meant for FixedUpdate()
    {
        // This Method subtracts from the currentSpeed

        currentSpeed -= acceleration;
    }

    void LimitSpeed() // Meant for FixedUpdate()
    {
        // This Method serves to clamp the speed of the train 
        // from 0 to maxSpeed.

        if (currentSpeed > maxSpeed)
            currentSpeed = maxSpeed;
        if (currentSpeed < 0)
            currentSpeed = 0;
    }
    void MakePassengersEnter()
    {
        for (int i = 1; i < Random.Range(minArrivalPassangers, maxArrivalPassangers); i++)
        {
            Spawning.Instance.SpawnNPC();
        }
    }

    void MakePassengersLeave()
    {
        for (int i = 0; i < passengers.Count; i++)
        {
            if (passengers[i].GetNPCState() == NPC.NPCState.SittingGood 
                || passengers[i].GetNPCState() == NPC.NPCState.SittingWaitingToLeave)
            {
                passengers[i].NPCLeaving();
                passengers.RemoveAt(i);
                i--;
            }
            else if (passengers[i].GetTimeToStation() <= 0)
            {
                passengers[i].NPCLeaving();
                passengers.RemoveAt(i);
                i--;
            }

        }
    }
    public void AddNPCToPassangers(NPC npc)
    {
        passengers.Add(npc);
    }


    // Coroutines
    #region Coroutines
    private IEnumerator Drive()
    {
        IsRolling = true;
        IsStopped = false;

        // Deterimines the duration of the segment
        float duration = Random.Range(stopInterval[0], stopInterval[1]);
        
        // We drive the current function for the segment duration...
        yield return new WaitForSeconds(duration);
        
        // At the end of this coroutine, we say we don't want to roll
        // That way, we start breaking the speed !!
        IsRolling = false;
    }
    
    private IEnumerator Stop()
    {
        // At this point, IsRolling is supposed to be false...
        // IsRolling = false;
        IsStopped = true;
        // Waiting for people to enter the metro...

        // We stop the current function for a duration...
        MakePassengersLeave();
        MakePassengersEnter();
        yield return new WaitForSeconds(stationStopDuration);
        
        
        // Everyone is on board, we can drive !
        StartCoroutine(Drive());
    }
    #endregion
}
