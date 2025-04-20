using UnityEngine;

public class Plant : Selectable
{
    public NPC.Emotion emotionCurer = NPC.Emotion.Sad;
    private GameObject plantAura;
    private int plantLevel = 1;
    public float levelScaleMulti = 0.1f;

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
}
