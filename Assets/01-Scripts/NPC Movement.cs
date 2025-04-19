using UnityEngine;

public class NPCMovement : MonoBehaviour
{

    public GameObject target;

    public float speed = 1.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        transform.position = newPosition;
    }
}
