using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed = 6f;
    [SerializeField]
    private Transform target;

    private void Awake()
    {
        if (!target)
        {
            target = FindObjectOfType<Character>().transform;
        }
    }

    private void Update()
    {
        if (target.position.x > 5f)
        {
            Vector3 position = target.position;
            position.y = transform.position.y;
            position.z = -10f;
            transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
        }
        /*Vector3 position = target.position;
        position.y = 0f;
        position.z = -10f;
        //transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);*/
    }
}
