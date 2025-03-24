using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private Rigidbody rb;

    void Start()
    {
        rb.linearVelocity = transform.right * speed;
    }
}
