using Unity.VisualScripting;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance;

    #region Properties
    public GameObject hand; // null if no held object

    #endregion
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        if (hand is not null)
        {
            hand.transform.position = Input.mousePosition;
        }
    }

    public bool TryTakeObject()
    {
        if (hand is null)
        {
            hand = ClicManager.Instance.currentTarget.GameObject();
            return true;
        }
        return false;
    }


    public bool TryPlaceObject()
    {
        if (hand is null)
        {
            return false;
        }
        hand.transform.position = Input.mousePosition;
        return true;
    }
}
