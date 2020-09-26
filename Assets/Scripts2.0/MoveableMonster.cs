using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveableMonster : Monster
{
    [SerializeField]
    private float speed = 2f;

    private Vector3 direction;

    private SpriteRenderer sprite;

    protected override void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    protected override void Start()
    {
        direction = transform.right;
    }

    protected override void Update()
    {
        Move();
    }

    private void Kill(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit is Character)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.5f)
            {
                RecieveDamage();
            }
            else
            {
                unit.RecieveDamage(unit.transform.position.x - transform.position.x);
            }
        }
    }

    private void Move()
    {
        Collider2D[] collidersBlocks = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.5f + transform.right * direction.x * 0.5f, 0.001f);
        Collider2D[] collidersPit = Physics2D.OverlapCircleAll(transform.position - transform.up * 0.5f + transform.right * direction.x * 0.5f, 0.001f);

        foreach (var collider in collidersBlocks)
        {
            Kill(collider);
        }
        if (collidersBlocks.Length > 1f || collidersPit.Length == 0f)
        {
            sprite.flipX = !sprite.flipX;
            direction *= -1f;
        }

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
}
