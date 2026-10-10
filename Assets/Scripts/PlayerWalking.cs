using System.Collections.Generic;
using UnityEngine;

public class PlayerWalking : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] public float speed = 4f;
    [SerializeField] public float jumpForce = 6f;

    private float xAxis;

    public bool onGround;

    [SerializeField] private Unity.Cinemachine.CinemachineCamera _vCam;

    private static List <PlayerWalking> _movebleCharactersList = new List<PlayerWalking>();
    private static int currentPlayer = 0;

    //надо поставить галочку у персонажа с которого надо накчинать
    [SerializeField] private bool _isActiveAtStart = false;
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
    }

    void Start()
    {
        if(_isActiveAtStart)
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

            if (Input.GetButtonDown("Jump") && onGround)
            {
                Jump();
            }
        }
    }

    public void SwitchToRobot(PlayerWalking target)
    {
        this.isCurrentPlayer = false;
        this.xAxis = 0f;

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
        _rb.linearVelocity = new Vector2(xAxis * speed, _rb.linearVelocity.y);
    }

    void Jump()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);

        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
