using UnityEngine;

public class ChromaManager : MonoBehaviour
{
    public Material fullscreenShader;
    
    public GameObject redPlantCoordinate;
    public GameObject greenPlantCoordinate;
    public GameObject bluePlantCoordinate;

    public Plant redPlantIntensity;
    public Plant greenPlantIntensity;
    public Plant bluePlantIntensity;


    void Update()
    {
        fullscreenShader.SetVector("_1_Coordinates", redPlantCoordinate.transform.position);
        fullscreenShader.SetVector("_2_Coordinates", greenPlantCoordinate.transform.position);
        fullscreenShader.SetVector("_2_Coordinates", bluePlantCoordinate.transform.position);
     
        fullscreenShader.SetVector("_1_Intensity", redPlantIntensity.s);
        fullscreenShader.SetVector("_2_Intensity", redPlantIntensity.s);
        fullscreenShader.SetVector("_3_Intensity", redPlantIntensity.s);
        
        
    }
}
