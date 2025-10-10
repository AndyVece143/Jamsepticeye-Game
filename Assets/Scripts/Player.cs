using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;
    public float speed;
    public float ghostSpeed;

    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;

    public Animator anim;

    public bool isAngel;
    public bool grounded;
    public bool falling;

    public Vector3 spawnPoint;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isAngel = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAngel)
        {
            AliveMovement();
        }

        if (isAngel)
        {
            GhostMovement();
        }
    }

    private void AliveMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        //Flip Sprite
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }

        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

            grounded = isGrounded();
        
        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            Debug.Log("Jump");
            Jump();
        }

        if (body.linearVelocity.y < 0)
        {
            falling = true;
        }
        else if (body.linearVelocity.y > 0)
        {
            falling = false;
        }

        Physics2D.IgnoreLayerCollision(6, 8, false);

        //Animation
        anim.SetBool("move", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
        anim.SetBool("falling", falling);
        anim.SetBool("angel", isAngel);
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
        grounded = false;
        anim.SetTrigger("jump");
    }

    private void GhostMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * (speed/2), ghostSpeed);

        //Flip Sprite
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }

        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        Physics2D.IgnoreLayerCollision(6,8);
    }

    private void Respawn()
    {
        isAngel = false;
        body.position = spawnPoint;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, Vector2.down, 0.01f, groundLayer);
        return raycastHit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Heaven")
        {
            Debug.Log("Die!");
            Respawn();
        }
    }

}
