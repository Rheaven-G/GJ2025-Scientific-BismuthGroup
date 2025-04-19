using UnityEngine;

public class Selectable : MonoBehaviour
{
    public bool isHeld = false;
    protected Collider col;

    public virtual void OnClick()
    {
        Debug.Log("Selectable.OnClick()");
    }
}
