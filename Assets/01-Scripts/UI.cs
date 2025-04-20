using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Slider progressBar;
    public Image fillImage;

    public NPC npc; // Reference to the NPC script
    public int score = 0;

    private Color startColor = Color.blue;
    private Color endColor = new Color(1f, 0.5f, 0f); // orange

    void Start()
    {
        if (npc == null)
        {
            npc = GetComponentInParent<NPC>(); // auto-assign if not set
        }
    }

    private void Update()
    {
        if (npc == null) return;

        // Show the bar only in relevant NPC states
        bool isVisible = npc.GetNPCState() == NPC.NPCState.SittingBad
                      || npc.GetNPCState() == NPC.NPCState.EffectedByPlant
                      || npc.GetNPCState() == NPC.NPCState.SittingGood;

        gameObject.SetActive(isVisible);

        if (!isVisible) return;

        // Update bar value and color
        progressBar.value = score;
        float t = Mathf.InverseLerp(0, 100, score);
        fillImage.color = Color.Lerp(startColor, endColor, t);

        // Demo: Space key to increase score (you can remove this later)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (score < 100)
            {
                score++;
            }
        }
    }
}
