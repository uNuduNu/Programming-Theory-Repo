using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
// parent
public abstract class Enemy : MonoBehaviour
{
    // ENCAPSULATION
    private float mSpeed = 3.0f;
    public float speed
    {
        get { return mSpeed; }
        set
        {
            if (value < 0.0f)
            {
                Debug.LogError("Don't set negative speed!");
            }
            else
            {
                mSpeed = value;
            }
        }
    }

    private int mHealth = 1;
    public int health
    {
        get { return mHealth; }
        set
        {
            if (value < 0)
            {
                Debug.LogError("Don't set negative health!");
            }
            else
            {
                mHealth = value;
            }
        }
    }

    // No need to set outside of class
    protected Rigidbody enemyRb { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();

        EnemySetup();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // implement in inherited class
    protected virtual void Move()
    {
    }

    protected virtual void EnemySetup()
    {
    }

    // Common
    public void OnBulletHit(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


    // ABSTRACTION
    protected void FacePlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.Log("Enemy::FacePlayer player missing");
        }

        transform.LookAt(player.transform);
    }

    protected Vector3 GetPlayerDirection()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.Log("Enemy::GetPlayerDirection player missing");

            return new Vector3();
        }

        return (player.transform.position - transform.position).normalized;
    }

    protected float GetDistanceToPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.Log("Enemy::GetDistanceToPlayer player missing");

            return new float();
        }

        return (player.transform.position - transform.position).magnitude;
    }

}
