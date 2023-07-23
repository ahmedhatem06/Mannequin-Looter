using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAndRotation : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;

    //[SerializeField] private AnimatorController _animatorController;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    private Rigidbody _rigidbody;

    private Vector3 _moveVector;

    private Player player;
    private bool canMove = false;
    private bool isPlayerMoving = false;
    private RigidbodyConstraints playerDefaultRigidbodyConstraints;
    private RigidbodyConstraints playerIdleRigidbodyConstraints;

    private void CanMove()
    {
        canMove = true;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        player = GetComponent<Player>();

        playerDefaultRigidbodyConstraints = _rigidbody.constraints;
        playerIdleRigidbodyConstraints = RigidbodyConstraints.FreezeAll;

        Tutorial.instance.tutorialDone += CanMove;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (canMove)
        {
            _moveVector = Vector3.zero;
            _moveVector.x = _joystick.Horizontal * _moveSpeed * Time.deltaTime;
            _moveVector.z = _joystick.Vertical * _moveSpeed * Time.deltaTime;

            if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            {
                Vector3 direction = Vector3.RotateTowards(transform.forward, _moveVector, _rotateSpeed * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(direction);
                isPlayerMoving = true;
                ChangeRigidbodyFreezeStatus(true);
                player.playerAnimation.PlayerMoving();
            }

            else if (_joystick.Horizontal == 0 && _joystick.Vertical == 0)
            {
                isPlayerMoving = false;
                ChangeRigidbodyFreezeStatus(false);
                player.playerAnimation.PlayerIdle();
            }

            _rigidbody.MovePosition(_rigidbody.position + _moveVector);
        }
    }

    private void ChangeRigidbodyFreezeStatus(bool status)
    {
        if (status)
        {
            _rigidbody.constraints = playerDefaultRigidbodyConstraints;
        }
        else
        {
            _rigidbody.constraints = playerIdleRigidbodyConstraints;
        }
    }

    public bool PlayerMovementStatus()
    {
        return isPlayerMoving;
    }

    public void PlayerCantMove()
    {
        canMove = false;
    }
}