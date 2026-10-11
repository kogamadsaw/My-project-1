using UnityEngine;

public class RobotSettings : MonoBehaviour
{
    private PlayerWalking _walkingSettings;

    public enum Type
    {
        DEFAULT,
        HEAVY,
        FAST,
        IT_PEDIA
    }

    public Type robotType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _walkingSettings = GetComponent<PlayerWalking>();
        switch (robotType)
        {
            case Type.DEFAULT:
                _walkingSettings.speed = 4;
                _walkingSettings.jumpForce = 4;
                break;
            case Type.HEAVY:
                _walkingSettings.speed = 2;
                _walkingSettings.jumpForce = 2;
                break;

            case Type.FAST:
                _walkingSettings.speed = 8;
                _walkingSettings.jumpForce = 8;
                _walkingSettings.canDash = true;
                break;

            case Type.IT_PEDIA:
                _walkingSettings.playerHeight = 1;
                _walkingSettings.speed = 6;
                _walkingSettings.jumpForce = 6;
                
                break;

            default:
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
