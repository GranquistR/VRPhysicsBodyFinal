using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class RollerBallLocomote : MonoBehaviour
{
    public InputActionReference playerMove; // Reference to the player's movement input
    public InputActionReference playerRun; // Reference to the player's run input
    public Transform forwardDirection; // Reference to the forward direction of the player (headset/controller)
    public Rigidbody rollerBall; // Reference to the Roller Ball rigidbody

    public float walkSpeed = 1.0f;
    public float runSpeed = 2.0f;

    private void OnEnable()
    {
        playerMove.action.Enable();
        playerRun.action.Enable();
    }

    private void OnDisable()
    {
        playerMove.action.Disable();
        playerRun.action.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rollerBall.inertiaTensorRotation = new Quaternion(1, 1, 1, 1);
        rollerBall.inertiaTensor = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveVelocity = GetControllerMoveVelocity();

        Vector3 desiredAngularVelocity = new Vector3(moveVelocity.z, 0, -moveVelocity.x);

        if (playerRun.action.ReadValue<float>() > 0)
        {
            desiredAngularVelocity *= runSpeed;
        }
        else
        {
            desiredAngularVelocity *= walkSpeed;
        }

        rollerBall.AddTorque(desiredAngularVelocity, ForceMode.VelocityChange);
    }

    private Vector3 GetControllerMoveVelocity()
    {
        Vector3 moveVelocity = new Vector3(playerMove.action.ReadValue<Vector2>().x, 0, playerMove.action.ReadValue<Vector2>().y); //get the input from the player
        moveVelocity = forwardDirection.TransformDirection(moveVelocity); //rotate the input to match the forward direction of the player
        return moveVelocity;
    }

}
