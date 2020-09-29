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
    private float jumpForce = 1f;

    private float dir = 1f;
    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;
    private float startBullet;

    private bool isGrounded = false;

    private Bullet bullet;
    
   

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

        
        if (Input.GetButtonDown("Fire1"))
        {
            Punch();
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
        if (isGrounded)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        sprite.flipX = direction.x < 0f;

       
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
        
        isGrounded = checkTag(Physics2D.OverlapCircleAll(transform.position - transform.up * 0.5f , transform.right.x),"Ground");
    }
    private void Punch()
    {
        var sprite = GetComponentInChildren<SpriteRenderer>();
        var pos = sprite.flipX ? 1 : -1;
        var colliders = Physics2D.OverlapCircleAll(transform.position - transform.right * 0.5f * pos, 1f);
        foreach (var collider in colliders)
        {
            if (collider.tag == "Enemy") Destroy(collider.gameObject);
        }
        
    }
    public bool checkTag(Collider2D[] colliders, string tag)
    {
        foreach (var collider in colliders)
        {
            if (collider.tag == tag || collider.tag == "Enemy") return true;
        }
        return false;
    }
}





