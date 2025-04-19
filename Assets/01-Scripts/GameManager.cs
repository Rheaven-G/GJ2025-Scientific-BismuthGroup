using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Properties

    public static GameManager Instance;

    #endregion

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
