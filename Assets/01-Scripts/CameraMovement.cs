using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float leftBound = -4.64f;
    private float rightBound = 1.04f; 
    public float speed = 3; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if(gameObject.transform.position.x > leftBound)
            {
                float newX = gameObject.transform.position.x - Time.deltaTime * speed;
                gameObject.transform.position = new Vector3(newX, gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (gameObject.transform.position.x < rightBound)
            {
                float newX = gameObject.transform.position.x + Time.deltaTime * speed;
                gameObject.transform.position = new Vector3(newX, gameObject.transform.position.y, gameObject.transform.position.z);
            }

        }
    }
}
