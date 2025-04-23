using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static NPC;

public class NPC : MonoBehaviour
{
    public enum NPCState
    {
        Arriving,
        SittingBad,
        EffectedByPlant,
        SittingGood,
        SittingWaitingToLeave,
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
    public GameObject goodEmotionPerfab;
    public float speed = 1.0f;
    public float leavingSpeedMultiplayer = 1.5f;
    public float arrivingSpeedRandomPersant = 1.5f;
    public float timeToFlipEmotion = 2.0f;
    public Emotion NPCEmotion;
    public float maxTimeToStation = 60;
    public float minTimeToStation = 20;
    //Debug
    public TextMesh debugText;

    public Animator anim;
    public bool isWalking = false;

    private NPCState state = NPCState.Arriving;
    private NPCEmotion myEmotion;
    private List<Plant> effectingPlants = new List<Plant>(32);
    private float currentTimeUnderPlant = 0;
    private float timeToStation = 0;
    private Collider emotionCollider;
    private UI bar;
    private SittingPosition seat;
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
        emotionCollider = gameObject.GetComponentInChildren<Collider>();
        emotionCollider.enabled = false; //

        bar = gameObject.GetComponentInChildren<UI>();


    }
    void fillBar()
    {
        //debugText.text = currentTimeUnderPlant.ToString();
        float presentageToflip = Mathf.InverseLerp(0, timeToFlipEmotion, currentTimeUnderPlant)*100;
        bar.score = presentageToflip;
    }
    // Update is called once per frame
    void Update()
    {
        debugText.text = state.ToString();
        switch (state)
        {
            case NPCState.Arriving:
                isWalking = true;
                anim.SetBool("walking", isWalking);
                MoveToSittingPositing();
                break;
            case NPCState.EffectedByPlant:
                timeToStation -= Time.deltaTime;
                currentTimeUnderPlant += Time.deltaTime;
                fillBar();
                if (currentTimeUnderPlant > timeToFlipEmotion)
                {
                    NPCFlippedEmotion();
                }
                break;
            case NPCState.SittingBad:
                timeToStation -= Time.deltaTime;
                break;
            case NPCState.SittingWaitingToLeave:
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
        seat = target.GetComponent<SittingPosition>();
        if (seat.occupied == false)
        {
            seat.SetIsOccupied(true);
            state = NPCState.SittingBad;
            myEmotion.gameObject.SetActive(true);
            MetroManager.Instance.AddNPCToPassangers(this);
            emotionCollider.enabled = true;
            bar.gameObject.SetActive(true);

            isWalking = false;
            anim.SetBool("walking", isWalking);

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
    public void NPCGiveEmotionToPlant()
    {
        state = NPCState.SittingWaitingToLeave;
        //Spawn Good Emotions
        foreach (Plant plant in effectingPlants)
        {
            Vector3 pos = new Vector3(myEmotion.transform.position.x, myEmotion.transform.position.y, myEmotion.transform.position.z - 5.0f);
            GameObject obj = Instantiate<GameObject>(goodEmotionPerfab, pos, Quaternion.identity);
            GoodEmotion goodEmotion = obj.GetComponent<GoodEmotion>();
            goodEmotion.target = plant.gameObject;
        }
        
    }
    public void NPCLeaving()
    {
        seat.SetIsOccupied(false);
        state = NPCState.Leaving;
        speed = speed * leavingSpeedMultiplayer;
        myEmotion.gameObject.SetActive(false);
        target = Spawning.Instance.ChooseRandomSpawnPoint();
        emotionCollider.enabled = false;

        isWalking = true;
        anim.SetBool("walking", isWalking);
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
        GameManager.Instance.AddPersonHelped();
        AudioMaster.Instance.PoeopleFlipEmotion();
    }
    public void OnPlantPickup(Plant plant)
    {
        HandleLeftPlatAura(plant);
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
                    plant.AddEffectedNPC(this);
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (state == NPCState.EffectedByPlant && other.gameObject.CompareTag("PlantAura"))
        {
            Plant plant = other.GetComponentInParent<Plant>();
            HandleLeftPlatAura(plant);
        }
    }

    void HandleLeftPlatAura(Plant plant)
    {
        if (NPCEmotion == plant.emotionCurer)
        {
            if (effectingPlants.Find(savedPlant => savedPlant == plant))
            {
                effectingPlants.Remove(plant);
            }

            if (effectingPlants.Count == 0 && state == NPCState.EffectedByPlant)
            {
                NPCStopbeingEffectdByPlant();
            }
        }
    }
}
