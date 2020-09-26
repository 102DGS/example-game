using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Monster : Unit
{
    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet bullet = collider.GetComponent<Bullet>();
        Unit unit = collider.GetComponent<Unit>();

        if (bullet)
        {
            RecieveDamage();
        }
        else if (unit && unit is Character)
        {
            unit.RecieveDamage(unit.transform.position.x - transform.position.x);
        }
    }
}
