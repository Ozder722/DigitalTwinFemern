using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public int speed = 2;
    public Rigidbody rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      rb = GetComponent<Rigidbody>();
      rb.useGravity = false;

    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector3(-speed,0,0);
    }
}
