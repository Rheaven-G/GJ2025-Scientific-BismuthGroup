using UnityEngine;

public class PlantManager : MonoBehaviour
{
    #region Properties

    public static PlantManager Instance;

    #endregion

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}