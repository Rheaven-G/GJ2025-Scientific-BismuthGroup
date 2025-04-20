using System.Collections.Generic;
using UnityEngine;

public class Plant : Selectable
{
    public NPC.Emotion emotionCurer = NPC.Emotion.Sad;
    private GameObject plantAura;
    private int plantLevel = 1;
    public float levelScaleMulti = 0.1f;
    private List<NPC> effectNPCs = new List<NPC>();
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("PlantAura"))
            {
                plantAura = child.gameObject;
            }
        }
    }

    public override void OnClick()
    {
        if (InputHandler.Instance.TryTakeObject())
        {
            isHeld = true;
            originalPos = gameObject.transform.position;
            HideCollider();
            PlantPickedUp();
        }
        else if (InputHandler.Instance.TryPlaceObject(gameObject))
        {
            if (InputHandler.Instance.IsBelowShelves(gameObject))
            {
                gameObject.transform.position = originalPos;
            }
            isHeld = false;
            ShowCollider();
        }
    }

    private void HideCollider()
    {
        col.enabled = false;
        ridgBody.useGravity = false;
        plantAura.SetActive(false);
    }

    private void ShowCollider()
    {
        col.enabled = true;
        ridgBody.useGravity = true;
        plantAura.SetActive(true);
    }

    public void PlantGainGoodEmotion()
    {
        if (plantLevel < 3)
        {
            plantLevel++;
        }
        plantAura.transform.localScale += plantAura.transform.localScale * (levelScaleMulti* plantLevel);

    }
    public void PlantPickedUp()
    {
        foreach (NPC npc in effectNPCs)
        {
            npc.OnPlantPickup(this);
        }
        effectNPCs.Clear();
    }
    public int GetPlantLevel()
    {
        return plantLevel;
    }

    public void AddEffectedNPC(NPC npc)
    {
        effectNPCs.Add(npc);
    }
}
