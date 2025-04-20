using UnityEngine;

public class Selectable : MonoBehaviour
{
    public bool isHeld = false;
    protected Collider col;
    protected Rigidbody ridgBody;
    protected Vector3 originalPos;

    private void Awake()
    {
        col = gameObject.GetComponent<Collider>();
        ridgBody = gameObject.GetComponent<Rigidbody>();
    }
    public virtual void OnClick()
    {
        Debug.Log("Selectable.OnClick()");
    }
}
