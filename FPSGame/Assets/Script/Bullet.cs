using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float speed = 30f;
    public float lifeTime = 3f;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
        rb.linearVelocity = transform.forward * speed;  

    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

}
