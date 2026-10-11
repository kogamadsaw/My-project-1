using System.Collections.Generic;
using UnityEngine;

public class PlayerWalking : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] public float speed = 4f;
    [SerializeField] public float jumpForce = 6f;

    private float xAxis;

    public bool onGround;
    public bool canDash = false;

    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    private float dashTimer;
    private float dashDirection;
    public float playerHeight = 2;

    [SerializeField] private Unity.Cinemachine.CinemachineCamera _vCam;

    private static List <PlayerWalking> _movebleCharactersList = new List<PlayerWalking>();
    private static int currentPlayer = 0;

    //надо поставить галочку у персонажа с которого надо накчинать
    public bool isActiveAtStart = false;
    [HideInInspector] public bool isCurrentPlayer;

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
        Debug.Log(_movebleCharactersList.Count);
        Debug.Log(canDash);
    }

    void Start()
    {
        transform.localScale = new Vector3(1, playerHeight, 1);
        if(isActiveAtStart)
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
                transform.localScale = new Vector3(1, playerHeight, 1);
            }
            else if (xAxis > 0)
            {
                transform.localScale = new Vector3(-1, playerHeight, 1);
            }

            if (Input.GetButtonDown("Jump") && onGround)
            {
                Jump();
            }

            if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
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
        this.isCurrentPlayer = true;

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
            _rb.linearVelocity = new Vector2(dashDirection * dashSpeed, 0f);
            Debug.Log(dashTimer);
        }
        else
        {
            _rb.linearVelocity = new Vector2(xAxis * speed, _rb.linearVelocity.y);
        }
    }

    void Dash()
    {
        dashDirection = xAxis != 0 ? Mathf.Sign(xAxis) : (transform.localScale.x > 0 ? 1f : -1f);
        dashTimer = dashDuration;
    }

    void Jump()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);

        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
