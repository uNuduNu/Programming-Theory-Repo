using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody enemyRb;

    [SerializeField] float speed;
    [SerializeField] int health = 1;


    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        FacePlayer();

        EnemyMove();

        EnemyAttack();
    }

    protected virtual void EnemyMove()
    {
        Vector3 playerDirection = GetPlayerDirection();

        Vector3 movement = playerDirection * speed * Time.deltaTime;
        movement.y = 0;

        transform.position += movement;
    }

    protected virtual void EnemyAttack()
    {

    }

    protected void FacePlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.Log("EnemyController::FacePlayer player missing");
        }

        transform.LookAt(player.transform);
    }

    Vector3 GetPlayerDirection()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.Log("EnemyController::GetPlayerDirection player missing");

            return new Vector3();
        }

        return (player.transform.position - transform.position).normalized;
    }

    public void OnBulletHit(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
