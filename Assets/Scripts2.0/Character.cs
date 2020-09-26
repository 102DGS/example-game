using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit
{
    [SerializeField]
    private int lives = 5;
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private float jumpForce = 15f;

    private float dir = 1f;
    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;
    private float startBullet;

    private bool isGrounded = false;

    private Bullet bullet;
    
    public CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        bullet = Resources.Load<Bullet>("Bullet");
        startBullet = Time.time;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (isGrounded)
        {
            State = CharState.Idle;
        }

        if (Input.GetButton("Fire1") && Time.time - startBullet > 0.5f)
        {
            Shoot();
            startBullet = Time.time;
        }

        if (Input.GetButton("Horizontal"))
        {
            Run();
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Run()
    {
        dir = Input.GetAxis("Horizontal");
        Vector3 direction = transform.right * dir;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        sprite.flipX = direction.x < 0f;

        if (isGrounded)
        {
            State = CharState.Run;
        }
    }

    private void Jump()
    {        
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Shoot()
    {
        Vector3 position = transform.position; 
        position.y += 0.8f;
        position.x += dir * 1f;


        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0f : 1.0f);
    }

    public override void RecieveDamage(float opponentPosition = 1f)
    {
        lives--;

        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.right * (opponentPosition < 0f ? -1f : 1f) * 8f, ForceMode2D.Impulse);

        Debug.Log(lives);
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = colliders.Length > 1;
        if (!isGrounded)
        {
            State = CharState.Jump;
        }
    }
}


public enum CharState
{
    Idle,
    Run,
    Jump
}
