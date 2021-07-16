using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody bulletRb;

    [SerializeField] float force = 8.0f;
    [SerializeField] int bulletDamage = 1;
    [SerializeField] float levelBounds = 50.0f;
    [SerializeField] float destructionDelay = 2.0f;

    bool hasHitObject = false; // don't allow multihits since we don't delete bullets instantly

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
        if (transform.position.x > levelBounds || transform.position.x < -levelBounds || 
            transform.position.z > levelBounds || transform.position.z < -levelBounds)
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
        if (true == hasHitObject)
        {
            return;       
        }

        hasHitObject = true;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().OnBulletHit(bulletDamage);

            GameManager.Instance.AddScore();
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().OnBulletHit(bulletDamage);
        }

        Invoke("DestroyBullet", destructionDelay);
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
