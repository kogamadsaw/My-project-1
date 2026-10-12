using System.Collections.Generic;
using UnityEngine;

public class PlayerWalking : MonoBehaviour
{
    private RobotSettings _robotSettings;

    private Rigidbody2D _rb;

    //input axis variable
    public float xAxis;

    //is player on ground
    public bool onGround;

    //dash variables
    private float dashTimer;
    private float dashDirection;

    [Header("Camera")]
    [SerializeField] private Unity.Cinemachine.CinemachineCamera _vCam;

    [Header("Check the box next to the character you want to start with.")]
    public bool isActiveAtStart = false;
    [HideInInspector] public bool isCurrentPlayer;
    private static List<PlayerWalking> _movebleCharactersList = new List<PlayerWalking>();
    private static int currentPlayer = 0;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null) Debug.LogError("Add Rigidbody2D to " + gameObject);

        if (_movebleCharactersList.Count > 0 && _movebleCharactersList[0] == null)
        {
            _movebleCharactersList.Clear();
            currentPlayer = 0;
        }

        _movebleCharactersList.Add(this);
    }

    void Start()
    {
        _robotSettings = GetComponent<RobotSettings>();

        transform.localScale = new Vector3(1, _robotSettings.playerHeight, 1);

        Collider2D myCollider = GetComponent<Collider2D>();
        if (myCollider != null)
        {
            PlayerWalking[] allRobot = FindObjectsByType<PlayerWalking>();
            foreach (var robot in allRobot)
            {
                if (robot != this)
                {
                    Collider2D otherCollider = robot.GetComponent<Collider2D>();
                    if (otherCollider != null)
                    {
                        Physics2D.IgnoreCollision(myCollider, otherCollider, true);
                    }
                }
            }
        }

        if (isActiveAtStart)
        {
            currentPlayer = _movebleCharactersList.IndexOf(this);
            ActivateCharacter();
        }
        else
        {
            isCurrentPlayer = false;
        }
    }

    private void Update()
    {
        if(isCurrentPlayer)
        {
            xAxis = Input.GetAxisRaw("Horizontal");

            if(xAxis < 0)
            {
                transform.localScale = new Vector3(1, _robotSettings.playerHeight, 1);
            }
            else if (xAxis > 0)
            {
                transform.localScale = new Vector3(-1, _robotSettings.playerHeight, 1);
            }

            if (Input.GetButtonDown("Jump") && onGround)
            {
                Jump();
            }

            if(Input.GetKeyDown(KeyCode.LeftShift) && _robotSettings.canDash)
            {
                Dash();
            }
        }
    }

    public void SwitchToRobot(PlayerWalking target)
    {
        this.xAxis = 0f;

        foreach (var character in _movebleCharactersList)
        {
            if (character != null) character.isCurrentPlayer = false;
        }

        Input.ResetInputAxes(); 

        int targetRobot = _movebleCharactersList.IndexOf(target);

        if(targetRobot != -1)
        {
            currentPlayer = targetRobot;
            target.ActivateCharacter();
        }
    }

    public void ActivateCharacter()
    {
        isCurrentPlayer = true;

        if (_vCam != null)
        {
            _vCam.Follow = this.transform;
            _vCam.LookAt = this.transform;
        }
        else
        {
            var mainVCam = FindAnyObjectByType<Unity.Cinemachine.CinemachineCamera>();

            if (mainVCam != null)
            {
                mainVCam.Follow = this.transform;
                mainVCam.LookAt = this.transform;
            }
        }
    }

    void FixedUpdate()
    {
        if (dashTimer > 0)
        {
            dashTimer -= Time.fixedDeltaTime;
            _rb.linearVelocity = new Vector2(dashDirection * _robotSettings.dashSpeed, 0f);
        }
        else
        {
            _rb.linearVelocity = new Vector2(xAxis * _robotSettings.speed, _rb.linearVelocity.y);
        }
    }

    void Dash()
    {
        dashDirection = xAxis != 0 ? Mathf.Sign(xAxis) : (transform.localScale.x > 0 ? 1f : -1f);
        dashTimer = _robotSettings.dashDuration;
    }

    void Jump()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);

        _rb.AddForce(Vector2.up * _robotSettings.jumpForce, ForceMode2D.Impulse);
    }
}
