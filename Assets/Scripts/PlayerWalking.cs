using UnityEngine;

public class PlayerWalking : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _jumpForce = 10f;

    private float xAxis;

    public bool onGround;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null) Debug.LogError("Add Rigidbody2D to " + gameObject);
    }

    private void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && onGround)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(xAxis * _speed, _rb.linearVelocity.y);
    }

    void Jump()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);

        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}
