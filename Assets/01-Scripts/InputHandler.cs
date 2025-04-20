using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance;

    #region Properties
    public GameObject hand = null; // null if no held object
    public GameObject shelves = null; // To make sure we calculate the Y position of the shelves

    #endregion
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        if (hand != null)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,0));
            pos.z = hand.transform.position.z;
            hand.transform.position = pos;
        }
    }

    public bool TryTakeObject()
    {
        if (hand == null)
        {
            hand = ClicManager.Instance.currentTarget.GameObject();
            return true;
        }
        return false;
    }


    public bool TryPlaceObject(GameObject obj)
    {       
        if (hand != null && obj == hand) // Make sure we place the object above the shelfs
        {
            hand = null;
            return true;
        }
        return false;
    }

    public bool IsBelowShelves(GameObject obj)
    {
        Vector3 objPos = Camera.main.WorldToScreenPoint(obj.transform.position);
        Vector3 shelvePos = Camera.main.WorldToScreenPoint(shelves.transform.position);

        if (objPos.y < shelvePos.y) 
        {
            return true;
        }
        return false;
    }
}
