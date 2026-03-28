using UnityEngine;

// Attach this to your Player GameObject.
// Requires: Rigidbody2D component on the same GameObject.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float MoveSpeed   = 5f;
    public float JumpForce   = 10f;

    [Header("Ground Check")]
    public Transform GroundCheck;       // create an empty child GameObject at feet, drag it here
    public float     GroundCheckRadius = 0.1f;
    public LayerMask GroundLayer;       // set this to your "Ground" layer in the Inspector

    private Rigidbody2D _rb;
    private bool        _isGrounded;
    private float       _horizontalInput;
    private bool        _facingRight = true;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.freezeRotation = true;  // stop Eleanor spinning on collision
    }

    void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal"); // A/D or Arrow keys

        // Ground check — small overlap circle at feet
        _isGrounded = Physics2D.OverlapCircle(
            GroundCheck ? GroundCheck.position : transform.position,
            GroundCheckRadius,
            GroundLayer
        );

        // Jump
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, JumpForce);
        }

        // Flip sprite direction
        if (_horizontalInput > 0 && !_facingRight) Flip();
        if (_horizontalInput < 0 &&  _facingRight) Flip();
    }

    void FixedUpdate()
    {
        // Move — FixedUpdate for physics consistency (same as C++ fixed timestep)
        _rb.velocity = new Vector2(_horizontalInput * MoveSpeed, _rb.velocity.y);
    }

    void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // Draw the ground check radius in the Scene view (like a debug gizmo)
    void OnDrawGizmosSelected()
    {
        if (GroundCheck == null) return;
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(GroundCheck.position, GroundCheckRadius);
    }
}
