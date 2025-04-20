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
        // Calculus
        
        
        // Positionning
        fullscreenShader.SetVector("_1_Coordinates", redPlant.transform.position);
        fullscreenShader.SetVector("_2_Coordinates", greenPlant.transform.position);
        fullscreenShader.SetVector("_2_Coordinates", bluePlant.transform.position);
     
        // Intensity levels
        fullscreenShader.SetFloat("_1_Intensity", redPlant.GetPlantLevel() * 1);
        fullscreenShader.SetFloat("_2_Intensity", redPlant.GetPlantLevel() * 1);
        fullscreenShader.SetFloat("_3_Intensity", redPlant.GetPlantLevel() * 1);
    }
}
