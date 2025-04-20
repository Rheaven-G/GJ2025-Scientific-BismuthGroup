using UnityEngine;

public class ChromaManager : MonoBehaviour
{
    public Camera Camera;
    public Material fullscreenShader;

    public Plant redPlant;
    public Plant greenPlant;
    public Plant bluePlant;

    void Awake()
    {
        Camera = FindFirstObjectByType<Camera>().GetComponent<Camera>();
    }
    

    void Update()
    {

    }
}
