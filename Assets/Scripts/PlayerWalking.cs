using UnityEngine;

public class PlayerWalking : MonoBehaviour
{
    Rigidbody2D _rb;

    [SerializeField] float _speed = 10f;
    [SerializeField] float _jumpForce = 10f;

    float xAxis;

    [SerializeField] private bool _onGround;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] Transform _groundCheck;
    [SerializeField] LayerMask _groundLayer;

    [SerializeField] CircleCollider2D _foundRobots;

    private Puzzle _puzzleScript;
    private bool _canAct;
    

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        if(_foundRobots == null) Debug.LogError("Add CircleCollider2D to " + gameObject);
        if (_rb == null) Debug.LogError("Add Rigidbody2D to " + gameObject);

        _canAct = false;
    }

    private void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && _onGround)
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.E) && _canAct)
        {
            _puzzleScript.StartPuzzle();
        }
    }

    void FixedUpdate()
    {
        _onGround = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);

        _rb.linearVelocity = new Vector2(xAxis * _speed, _rb.linearVelocity.y);
    }

    void Jump()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);

        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void OnDrawGizmosSelected()
    {
        if (_groundCheck != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Robot"))
        {
            Debug.Log("Robot entered ");

            _puzzleScript = collision.GetComponent<Puzzle>();
            _puzzleScript.player = gameObject;
            Debug.Log(_puzzleScript.player);
            _canAct = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Robot"))
        {
            if(_puzzleScript.canDelete == true)
            {
                _puzzleScript.player = null;
                _puzzleScript = null;
                _canAct = false;
            }
        }
    }
}
