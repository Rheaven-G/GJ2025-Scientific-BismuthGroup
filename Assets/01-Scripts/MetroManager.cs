using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MetroManager : MonoBehaviour
{
    public enum MetroState
    {
        SpeedingUp,
        SlowingDown,
        Going,
        Stopped
    }
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
    public int additionPerLevel = 2;


    [Header("Interactions")]
    public Material windowMaterials;

    // Private vars
    public bool IsRolling { get; private set; }
    private bool IsStopped = false;

    [Header("Debug")]
    public GameObject metroSpeedIndication;
    #endregion
    private List<NPC> passengers = new List<NPC>(64);

    public MetroState state = MetroState.Stopped;
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

        Debug.Log(state.ToString());
    }

    void FixedUpdate()
    {
        if (IsRolling)
        {
            if(currentSpeed < maxSpeed)
            {
                Accelerate();
                ChangeMetroState(MetroState.SpeedingUp);
            }
            else
            {
                ChangeMetroState(MetroState.Going);
            }    
        }
        else if (!IsRolling && currentSpeed > 0)
        {
            SlowDown();
            ChangeMetroState(MetroState.SlowingDown);
        }
        else if(!IsStopped)
        {
            ChangeMetroState(MetroState.Stopped);
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
        {
            maxSpeed = currentSpeed;
        }
        if (currentSpeed < 0)
        {
            currentSpeed = 0;
        }
    }
    void ChangeMetroState(MetroState newState)
    {
        if (newState == state)
        { return; }
        state = newState;

        if (state == MetroState.SlowingDown)
        {
            AudioMaster.Instance.StopMetro();
        }
        else if (state == MetroState.SpeedingUp)
        {
            AudioMaster.Instance.ResumeMetro();
        }

        

    }

    void MakePassengersEnter()
    {
        int min = (additionPerLevel * GameManager.Instance.GetCurrentPhase()) + minArrivalPassangers;
        int max = (additionPerLevel * GameManager.Instance.GetCurrentPhase()) + maxArrivalPassangers;
        for (int i = 0; i < Random.Range(min, max); i++)
        {
            Spawning.Instance.SpawnNPC();
        }
    }

    void MakePassengersLeave()
    {
        Debug.Log(passengers.Count);
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
        IsRolling = false;
        // Waiting for people to enter the metro...

        // We stop the current function for a duration...
        MakePassengersLeave();

        if (GameManager.Instance.timeIsUp == true)
        {
            yield return new WaitForSeconds(stationStopDuration);
            GameManager.Instance.GameOver();
        }
        else
        {
            MakePassengersEnter();

            yield return new WaitForSeconds(stationStopDuration);

            // Everyone is on board, we can drive !
            StartCoroutine(Drive());
        }
    }    

    #endregion
}
