using UnityEngine;

public class DebugMesh : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MeshRenderer myMesh = gameObject.GetComponent<MeshRenderer>();
        myMesh.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
