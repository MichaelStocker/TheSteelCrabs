using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    public int damage;
    public int bulletSpeed;
    public int destroyTime;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.forward * bulletSpeed;
        Destroy(gameObject,destroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
