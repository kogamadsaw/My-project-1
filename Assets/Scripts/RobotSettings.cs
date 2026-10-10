using UnityEngine;

public class RobotSettings : MonoBehaviour
{
    private PlayerWalking _walkingSettings;

    public enum Type
    {
        DEFAULT,
        HEVY,
        FAST
    }

    public Type robotType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _walkingSettings = GetComponent<PlayerWalking>();
        switch (robotType)
        {
            case Type.DEFAULT:
                break;
            case Type.HEVY:
                _walkingSettings.speed = 2;
                _walkingSettings.jumpForce = 2;
                break;

            case Type.FAST:
                _walkingSettings.speed = 8;
                _walkingSettings.jumpForce = 8;
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
