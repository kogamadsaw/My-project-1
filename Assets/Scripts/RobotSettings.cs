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
                break;
            case robotType.Heavy:
                speed = 2;
                jumpForce = 4;

                canDash = false;

                playerHeight = 2f;
                break;

            case robotType.Fast:
                speed = 8;
                jumpForce = 9;

                canDash = true;
                dashSpeed = 20f;
                dashDuration = 0.2f;

                playerHeight = 2f;
                break;

            case robotType.IT_Pedia:
                speed = 6;
                jumpForce = 8;

                canDash = false;

                playerHeight = 1f;
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
