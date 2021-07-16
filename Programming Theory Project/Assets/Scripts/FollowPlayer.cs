using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 offset = new Vector3(0, 10, 0);

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
