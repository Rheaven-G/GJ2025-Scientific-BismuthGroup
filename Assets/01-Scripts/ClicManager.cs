using UnityEngine;

public class ClicManager : MonoBehaviour
{
    public static ClicManager Instance;

    public Selectable currentTarget;
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        // We register's mouse position to create a ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, 30);
        
        // we get the type of object we collide with
        if (hit.collider.TryGetComponent<Selectable>(out Selectable selection))
        {
            currentTarget = selection;
        }
        
        if (currentTarget is not null)
            if (Input.GetMouseButtonDown(0))
                currentTarget.OnClick();
    }
}
