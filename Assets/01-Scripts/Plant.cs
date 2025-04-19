using UnityEngine;

public class Plant : Selectable
{

    public override void OnClick()
    {
        //Do Something for the plant
        Debug.Log("Plant Clicked");

        if (InputHandler.Instance.TryTakeObject())
        {
            isHeld = true;
            HideCollider();
        }
        else
        {
            isHeld = false;
            ShowCollider();
        }
    }

    private void HideCollider()
    {
        col.enabled = false;
    }

    private void ShowCollider()
    {
        col.enabled = true;
    }
}
