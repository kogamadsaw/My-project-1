using UnityEngine;

public class RobotSettings : MonoBehaviour
{

    //Speed variables
    [HideInInspector] public float speed;

    //Jump variables
    [HideInInspector] public float jumpForce;

    //Dash variables
    [HideInInspector] public bool canDash = false;
    [HideInInspector] public float dashSpeed;
    [HideInInspector] public float dashDuration;

    //Height variables
    [HideInInspector] public float playerHeight;

    //Player sprite
    [SerializeField] private SpriteRenderer _sprite;

    [SerializeField] private Sprite _itPediaSprite;
    [SerializeField] private Sprite _BigRobot;
    [SerializeField] private Sprite _FastRobot;
    [SerializeField] private Sprite _PenguinJpg;

    public enum robotType
    {
        Default,
        Heavy,
        Fast,
        IT_Pedia
    }

    [Header("Robot type")]
    public robotType robot;

    private void Awake()
    {
        switch (robot)
        {
            case robotType.Default:
                speed = 4;
                jumpForce = 8;

                canDash = false;

                playerHeight = 2f;
                _sprite.sprite = _PenguinJpg;
                break;
            case robotType.Heavy:
                speed = 2;
                jumpForce = 4;

                canDash = false;

                playerHeight = 2f;
                _sprite.sprite = _BigRobot;
                break;

            case robotType.Fast:
                speed = 8;
                jumpForce = 9;

                canDash = true;
                dashSpeed = 20f;
                dashDuration = 0.2f;

                playerHeight = 2f;
                _sprite.sprite = _FastRobot;
                break;

            case robotType.IT_Pedia:
                speed = 6;
                jumpForce = 8;

                canDash = false;

                playerHeight = 1f;
                _sprite.sprite = _itPediaSprite;
                break;

            default:
                break;
        }

    }

        void Start()
    {
        
    }

    void Update()
    {
        
    }
}
