using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
// child
public class ShootingEnemy : Enemy
{
    [SerializeField] float shootingDelay = 3.0f;    // Shoot every 3 seconds
    [SerializeField] float moveToDistance = 5.0f;

    public GameObject bulletPrefab;
    public GameObject targetObject;

    private float startDelay = 1.0f;

    // POLYMORPHISM
    protected override void Move()
    {
        FacePlayer();

        if (moveToDistance < GetDistanceToPlayer())
        {
            Vector3 playerDirection = GetPlayerDirection();

            Vector3 movement = playerDirection * speed * Time.deltaTime;
            movement.y = 0;

            transform.position += movement;
        }
    }

    // POLYMORPHISM
    protected override void EnemySetup()
    {
        InvokeRepeating("ShootAtPlayer", startDelay, shootingDelay);
    }

    void ShootAtPlayer()
    {
        if (moveToDistance >= GetDistanceToPlayer())
        {
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, targetObject.transform.position.z), transform.rotation);

            bullet.GetComponent<BulletController>().Shoot((targetObject.transform.position - transform.position).normalized);
        }
    }
}
