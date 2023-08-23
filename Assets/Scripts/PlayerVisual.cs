using System;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private const string IS_MOVE_UP = "IsMoveUp";
    private const string IS_MOVE_DOWN = "IsMoveDown";
    private const string IS_MOVE_SIDE = "IsMoveSide";
    private const string SPEED = "Speed";
    private const string HEALTH = "Health";


    private FixedJoystick joystick;
    private JoystickDirectionState joystickDirectionState;

    private SpriteRenderer spriteRenderer;
    private float moveSpeed;
    private Player player;
    private JoystickDirectionState.JoystickState joystickState;
    private Animator animator;

    public event Action OnDeathAnimationEnd;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        animator = GetComponent<Animator>();
        joystick = FindObjectOfType<FixedJoystick>();
        joystickDirectionState = FindObjectOfType<JoystickDirectionState>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        player.OnHit += Player_OnHit;
    }

    private void OnAnimationEnd() 
    {
        OnDeathAnimationEnd?.Invoke();
    }

    private void Player_OnHit()
    {
        float health = player.health;
        animator.SetFloat(HEALTH, health);
    }

    private void Update()
    {
        joystickState = joystickDirectionState.GetJoystickState();
        moveSpeed = player.GetMoveSpeed();

        animator.SetFloat(SPEED, moveSpeed);

        switch (joystickState) 
        {
            case JoystickDirectionState.JoystickState.Up:
                animator.SetBool(IS_MOVE_UP, true);
                animator.SetBool(IS_MOVE_DOWN, false);
                animator.SetBool(IS_MOVE_SIDE, false);
                break;

            case JoystickDirectionState.JoystickState.Down:
                animator.SetBool(IS_MOVE_UP, false);
                animator.SetBool(IS_MOVE_DOWN, true);
                animator.SetBool(IS_MOVE_SIDE, false);
                break;

            case JoystickDirectionState.JoystickState.Left:
                animator.SetBool(IS_MOVE_UP, false);
                animator.SetBool(IS_MOVE_DOWN, false);
                animator.SetBool(IS_MOVE_SIDE, true);
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                break;

            case JoystickDirectionState.JoystickState.Right:
                animator.SetBool(IS_MOVE_UP, false);
                animator.SetBool(IS_MOVE_DOWN, false);
                animator.SetBool(IS_MOVE_SIDE, true);
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                break;
        }
    }
}
