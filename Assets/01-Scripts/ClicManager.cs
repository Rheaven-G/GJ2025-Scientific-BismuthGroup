using UnityEngine;
using UnityEngine.XR;

public class ClicManager : MonoBehaviour
{
    public static ClicManager Instance;

    public Selectable currentTarget;
    public LayerMask layerMask;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        if (InputHandler.Instance.hand == null)
        {
            // We register's mouse position to create a ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hit, 30, layerMask);

            // we get the type of object we collide with
            if (hit.collider && hit.collider.TryGetComponent<Selectable>(out Selectable selection))
            {
                currentTarget = selection;
            }
            else
            {
                currentTarget = null;
            }
        }

        if (currentTarget is not null)
        {
            Debug.Log(currentTarget.name);
            if (Input.GetMouseButtonDown(0))
                currentTarget.OnClick();
        }

    }
}
