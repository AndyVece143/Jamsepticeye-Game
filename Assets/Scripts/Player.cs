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

    public bool isGhost;
    

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGhost = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGhost)
        {
            AliveMovement();
        }

        if (isGhost)
        {
            GhostMovement();
        }
    }

    private void AliveMovement()
    {
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);

        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            Debug.Log("Jump");
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
        }

        Physics2D.IgnoreLayerCollision(6, 8, false);
    }

    private void GhostMovement()
    {
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * (speed/2), ghostSpeed);
        Physics2D.IgnoreLayerCollision(6,8);
    }

    private void CheckCollision()
    {

    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }


}
