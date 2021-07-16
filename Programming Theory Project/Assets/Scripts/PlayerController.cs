using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    [SerializeField] float turningSpeed = 150.0f;
    [SerializeField] float speed = 4.0f;


    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject targetObject;

    [SerializeField] int health = 4;
    [SerializeField] float shootingDelay = 0.5f;


    private Quaternion startRotation;
    private float startY;

    private bool standing = true;
    private bool readyToShoot = true;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        startRotation = transform.rotation;
        startY = transform.position.y;

        GameManager.Instance.UpdateHealth(health);
    }

    void FixedUpdate()
    {
        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");

        if (false == standing && verInput > 0.0)
        {
            // Stop moving, stand up
            playerRb.velocity = new Vector3();
            playerRb.angularVelocity = new Vector3();

            transform.rotation = startRotation;
            transform.position = new Vector3(transform.position.x, startY, transform.position.z);

            standing = true;
        }
        else
        {
            transform.Rotate(new Vector3(0.0f, horInput * turningSpeed * Time.deltaTime));

            Vector3 movement = verInput * (targetObject.transform.position - transform.position).normalized * speed * Time.deltaTime;
            movement.y = 0;

            transform.position += movement;
        }
    }

    void Update()
    { 
        if (true == standing && true == readyToShoot)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                readyToShoot = false;

                GameObject bullet = Instantiate(bulletPrefab, targetObject.transform.position, transform.rotation);

                bullet.GetComponent<BulletController>().Shoot((targetObject.transform.position - transform.position).normalized);

                Invoke("ReloadingGun", shootingDelay);
            }
        }
    }

    private void ReloadingGun()
    {
        readyToShoot = true;
    }

    public void OnBulletHit(int damage)
    {
        health -= damage;

        GameManager.Instance.UpdateHealth(health);

        if (health <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    public void OnEnemyHit(float force, Vector3 direction)
    {
        health -= 1;

        GameManager.Instance.UpdateHealth(health);

        if (health <= 0)
        {
            GameManager.Instance.GameOver();
        }

        playerRb.AddForce(direction * force, ForceMode.Impulse);

        standing = false;
    }
}
