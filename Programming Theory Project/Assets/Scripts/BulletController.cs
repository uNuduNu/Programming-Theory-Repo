using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody bulletRb;

    [SerializeField] float force = 8.0f;
    [SerializeField] int bulletDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (null == bulletRb)
        {
            bulletRb = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 10 || transform.position.x < -10 || 
            transform.position.z > 10 || transform.position.z < -10)
        {
            Destroy(gameObject);
        }
        
    }

    public void Shoot(Vector3 direction)
    {
        if (null == bulletRb)
        {
            bulletRb = GetComponent<Rigidbody>();
        }

        bulletRb.AddForce(direction * force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().Hit(bulletDamage);
            Destroy(gameObject);
        }
    }
}
