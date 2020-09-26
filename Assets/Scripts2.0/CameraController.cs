using System;
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
        Vector3 position = target.position;
        if (Math.Abs(target.position.x % 20) > 10f)
        {
            position.x = position.x - position.x % 10;
        }
        
        position.z = -10f;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
        /*Vector3 position = target.position;
        position.y = 0f;
        position.z = -10f;
        //transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);*/
    }
}
