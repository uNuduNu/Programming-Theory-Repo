using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    [SerializeField] float turningSpeed = 150.0f;

    public GameObject bulletPrefab;
    public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horInput = Input.GetAxis("Horizontal");

        transform.Rotate(new Vector3(0.0f, horInput * turningSpeed * Time.deltaTime));

        if ( Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, targetObject.transform.position.z), transform.rotation);

            bullet.GetComponent<BulletController>().Shoot((targetObject.transform.position - transform.position).normalized);
        }
    }
}
