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
            originalPos = gameObject.transform.position;
            HideCollider();
        }
        else if (InputHandler.Instance.TryPlaceObject(gameObject))
        {
            if(InputHandler.Instance.IsBelowShelves(gameObject))
            {
                gameObject.transform.position = originalPos;    
            }
            isHeld = false;
            ShowCollider();
        }
    }

    private void HideCollider()
    {
        col.enabled = false;
        ridgBody.useGravity = false;
    }

    private void ShowCollider()
    {
        col.enabled = true;
        ridgBody.useGravity = true;
    }
}
