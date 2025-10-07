using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public int speed = 2;
    public Rigidbody rb;
    public int dSpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      rb = GetComponent<Rigidbody>();
      rb.useGravity = false;

        dSpeed = speed;

    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector3(-speed,0,0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CarMovement otherCar = collision.gameObject.GetComponent<CarMovement>();

        if (otherCar != null && otherCar.speed == 0) // ramte en bil, der står stille
        {
            WholeTunnel tunnel = FindObjectOfType<WholeTunnel>();
            if (tunnel != null)
            {
                tunnel.OnCarCrash();
            }
        }
    }
}
