using UnityEngine;

public class JoystickDirectionState : MonoBehaviour
{
    public enum JoystickState
    {
        Left,
        Right,
        Up,
        Down,
    }

    private FixedJoystick joystick;
    private JoystickState joystickState;
    private float XPos;
    private float YPos;

    private void Awake()
    {
        joystick = GetComponent<FixedJoystick>();
    }

    private void Start()
    {
        joystickState = JoystickState.Down;
    }

    private void Update()
    {
        XPos = joystick.Horizontal;
        YPos = joystick.Vertical;

        if (XPos <= -0.75f && YPos > -0.75f && YPos < 0.75f) 
        {
            joystickState = JoystickState.Left;
        }
        if (XPos >= 0.75f && YPos > -0.75f && YPos < 0.75f)
        {
            joystickState = JoystickState.Right;
        }
        if (XPos > -0.75f && XPos < 0.75f && YPos >= 0.75f)
        {
            joystickState = JoystickState.Up;
        }
        if (XPos > -0.75f && XPos < 0.75f && YPos <= -0.75f)
        {
            joystickState = JoystickState.Down;
        }
    }

    public JoystickState GetJoystickState() 
    {
        return joystickState;
    }

}
