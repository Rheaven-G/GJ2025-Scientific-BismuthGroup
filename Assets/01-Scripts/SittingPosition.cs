using UnityEngine;

public class SittingPosition : MonoBehaviour
{
    [System.NonSerialized]
    public bool occupied = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAsOccupied()
    {
        occupied = true;
    }
}
