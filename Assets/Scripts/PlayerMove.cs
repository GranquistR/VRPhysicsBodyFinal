using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Player Speed")]
    public float maxSpeed = 1f; // Max speed of the player
    public float speedMultiplier = 500f; // Multiplier for speed
    public float airMultiplier = 0.25f; // Multiplier for air movement
    [ReadOnly(true)][SerializeField] private float currentSpeed = 0f; // Current speed of the player (DEBUG)

    [Header("References")]
    public InputActionReference playerMove;
    public Transform lookDirection; // Camera direction
    public GameObject playerBody; //Player body transform. Used for ground detection
    private Rigidbody rb;

    [Header("Ground Detection")]
    [SerializeField] private bool isGrounded = false; // Is the player grounded?

    [Header("Jump")]
    public float jumpForce = 500f; // Jump force
    public InputActionReference playerJump; // Jump input
    public bool jumpReleased = false; // Is the jump button released?

    void OnEnable()
    {
        playerMove.action.Enable();
        playerJump.action.Enable();
    }

    void OnDisable()
    {
        playerMove.action.Disable();
        playerJump.action.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundDetection();
        ForceMove();
        SpeedLimit();
        GroundDrag();
        ForceJump();
    }

    private void GroundDetection()
    {
        RaycastHit hit;
        bool wasGrounded = isGrounded;

        Vector3 playerFeet = new Vector3(playerBody.transform.position.x, playerBody.transform.position.y - (playerBody.GetComponent<CapsuleCollider>().height / 2) + 0.05f, playerBody.transform.position.z);
        playerFeet += playerBody.GetComponent<CapsuleCollider>().center;

        isGrounded = Physics.Raycast(playerFeet, Vector3.down, out hit, 0.1f);
        //Debuf ray
        Debug.DrawRay(playerFeet, Vector3.down * 0.1f, Color.red);
    }

    private void ForceMove()
    {
        Vector3 move = new Vector3(playerMove.action.ReadValue<Vector2>().x, 0, playerMove.action.ReadValue<Vector2>().y);
        Quaternion look = Quaternion.Euler(0, lookDirection.eulerAngles.y, 0);

        move = look * move;

        if (isGrounded)
        {
            rb.AddForce(move.normalized * maxSpeed * speedMultiplier, ForceMode.Force);
        }
        else
        {
            rb.AddForce(move.normalized * maxSpeed * speedMultiplier * airMultiplier, ForceMode.Force);
        }

        currentSpeed = rb.velocity.magnitude;
    }

    private void SpeedLimit()
    {
        Vector3 velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (velocity.magnitude > maxSpeed)
        {
            Vector3 limit = velocity.normalized * maxSpeed;
            rb.velocity = new Vector3(limit.x, rb.velocity.y, limit.z);
        }
    }

    private void GroundDrag()
    {
        if (isGrounded)
        {
            rb.drag = 5f;
            rb.useGravity = false;
        }
        else
        {
            rb.drag = 0f;
            rb.useGravity = true;
        }
    }

    private void ForceJump()
    {
        if (playerJump.action.ReadValue<float>() > 0)
        {
            if (isGrounded && jumpReleased)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            jumpReleased = false;
        }
        else
        {
            jumpReleased = true; //prevents multiple jumps before the game registers leaving the ground
        }
    }
}