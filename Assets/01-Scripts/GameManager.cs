using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Properties

    public static GameManager Instance;
    int numberOfPeoepleHelped = 0;
    public int phase2Threshold = 10;
    public int phase3Threshold = 30;
    int currentPhase = 0;


    #endregion

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void AddPersonHelped()
    {
        numberOfPeoepleHelped++;
        if (numberOfPeoepleHelped < phase2Threshold)
        {
            currentPhase = 0;
        }
        else if (numberOfPeoepleHelped > phase2Threshold)
        {
            currentPhase = 1;
        }
        else
        {
            currentPhase = 2;
        }
    }

    public int GetCurrentPhase()
    { return currentPhase; }
}
