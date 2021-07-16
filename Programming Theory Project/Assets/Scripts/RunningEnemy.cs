using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
// child
public class RunningEnemy : Enemy
{
    // POLYMORPHISM
    protected override void Move()
    {
        FacePlayer();

        Vector3 playerDirection = GetPlayerDirection();

        Vector3 movement = playerDirection * speed * Time.deltaTime;
        movement.y = 0;

        transform.position += movement;
    }

    protected override void EnemySetup()
    {
    }
}
