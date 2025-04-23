using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float leftBound = -3.99f;
    public float rightBound = 0.04f; 
    public float speed = 3; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if(gameObject.transform.position.x > leftBound)
            {
                float newX = gameObject.transform.position.x - Time.deltaTime * speed;
                gameObject.transform.position = new Vector3(newX, gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (gameObject.transform.position.x < rightBound)
            {
                float newX = gameObject.transform.position.x + Time.deltaTime * speed;
                gameObject.transform.position = new Vector3(newX, gameObject.transform.position.y, gameObject.transform.position.z);
            }

        }
    }
}
