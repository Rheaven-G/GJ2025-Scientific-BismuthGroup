using System.Collections.Generic;
using UnityEngine;

public class NPCEmotion : Selectable
{


    public Material[] SadMaterials = null;
    public Material[] AngerMaterials = null;
    public Material[] AfraidMaterials = null;

    private Dictionary<NPC.Emotion, Material[]> emotionMap  = new Dictionary<NPC.Emotion, Material[]>();
    private MeshRenderer mesh = null;
    private NPC npc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        emotionMap[NPC.Emotion.Sad] = SadMaterials;
        emotionMap[NPC.Emotion.Anger] = AngerMaterials;
        emotionMap[NPC.Emotion.Afraid] = AfraidMaterials;
        mesh = gameObject.GetComponent<MeshRenderer>();
        npc = gameObject.GetComponentInParent<NPC>();

    }
    public void SetInitialEmotion(NPC.Emotion emotion)
    {
        mesh.material = emotionMap[emotion][0];
    }
    public void SetEffectByPlant(NPC.Emotion emotion)
    {
        mesh.material = emotionMap[emotion][1];
    }
    public void SetGoodEmotion(NPC.Emotion emotion)
    {
        mesh.material = emotionMap[emotion][2];
    }
    public override void OnClick()
    {
        if (npc != null && npc.GetNPCState() == NPC.NPCState.SittingGood)
        {
            npc.NPCGiveEmotionToPlant();
        }

    }
}
