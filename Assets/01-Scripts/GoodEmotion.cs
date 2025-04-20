using UnityEngine;

public class GoodEmotion : MonoBehaviour
{
    public GameObject target;
    public float speed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(target.transform.position.x, target.transform.position.y, gameObject.transform.position.z);
        Vector3 newPosition = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        transform.position = newPosition;

        if ((new Vector2(transform.position.x, transform.position.y) - new Vector2(target.transform.position.x, target.transform.position.y)).magnitude < 0.01f)
        {
            Plant plant = target.GetComponent<Plant>();
            plant.PlantGainGoodEmotion();
            Destroy(gameObject);
        }
    }
}
