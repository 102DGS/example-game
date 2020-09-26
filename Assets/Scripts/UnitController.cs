using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public float speed = 0.1f;
    SpriteRenderer sprite;
    Rigidbody2D rb;
    bool isGrounded;
    // Start is called before the first frame update
    void Awake()
    {
       
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        isGrounded = true;
    }

    void Start()
    {

        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        isGrounded = true;
    }
    void Update()
    {
        if (Input.GetButton("Horizontal")) { RunH(); }
        if (Input.GetKeyDown(KeyCode.Space)) { Jump(); }
        
    }
    void RunH()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        sprite.flipX = direction.x > 0;
    }
    void Jump()
    {
        Debug.Log("Jump");
        if (isGrounded)
        {
            Vector2 jump = new Vector2(transform.up.x, transform.up.y + 3);
            rb.AddForce(jump, ForceMode2D.Impulse);
            isGrounded = false;
            Debug.Log("Jump true");
        }
        else
        {
            isGrounded = true;
        }

    }
}
