using UnityEngine;

public class ChromaManager : MonoBehaviour
{
    public Camera Camera;
    public Material fullscreenShader;

    public Plant redPlant;
    public Plant greenPlant;
    public Plant bluePlant;
    public float theratio = 1f;
    public float theoffset = 0.1f;

    void Awake()
    {
        Camera = FindFirstObjectByType<Camera>().GetComponent<Camera>();
    }
    

    void Update()
    {
        // Calculus
        Vector3 camPos = Camera.transform.position;
        Vector3 redPos = redPlant.transform.position;
        Vector3 greenPos = greenPlant.transform.position;
        Vector3 bluePos = bluePlant.transform.position;

        Vector3[] oklist = { redPos, greenPos, bluePos };
        for (int i = 0; i < oklist.Length; i++)
        {
            oklist[i] -= camPos;
            oklist[i] *= theratio;
            // oklist[i] = oklist[i].normalized;

            oklist[i].x += theoffset;
            oklist[i].y = -0.15f;
            oklist[i].z = 0f;
        }
        
        // Positionning
        fullscreenShader.SetVector(
            "_1_Coordinates", 
            oklist[0] + new Vector3(
                theoffset * redPlant.GetPlantLevel(), 
                0f, 0f)
            );
        fullscreenShader.SetVector(
            "_2_Coordinates", 
            oklist[1] + new Vector3(
                theoffset * greenPlant.GetPlantLevel(), 
                0f, 0f)
            );
        fullscreenShader.SetVector(
            "_3_Coordinates", 
            oklist[2] + new Vector3(
                theoffset * bluePlant.GetPlantLevel(), 
                0f, 0f)
            );
     
        // Intensity levels
        fullscreenShader.SetFloat(
            "_1_Intensity", 
            redPlant.GetPlantLevel() * 0.33f
            );
        fullscreenShader.SetFloat(
            "_2_Intensity", 
            greenPlant.GetPlantLevel() * 0.33f
            );
        fullscreenShader.SetFloat(
            "_3_Intensity", 
            bluePlant.GetPlantLevel() * 0.33f
            );
    }
}
