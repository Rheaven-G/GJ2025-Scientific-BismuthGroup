using UnityEngine;

public class PeopleManager : MonoBehaviour
{
    #region Properties

    public static PeopleManager Instance;
    
    #endregion

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}