using UnityEngine;

public class LockPosition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y != 0.1f) {
            transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        }
    }
}
