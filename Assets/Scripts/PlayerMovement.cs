using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 7f;
    public float diagonalMoveSpeed = 3.5f;

    private float horizontalInput;
    private float verticalInput;
    private float sprintInput;

    private Rigidbody playerRB;
    private Vector3 moveInput;

    private float currentSpeed;

    private void Awake()
    {
        if (playerRB == null)
        {
            playerRB = GetComponent<Rigidbody>();
        }
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        sprintInput = Input.GetKey(KeyCode.LeftShift) ? 1 : 0;

        moveInput = new Vector3(horizontalInput, 0 ,verticalInput).normalized;

        if (sprintInput < 1)
        {
            currentSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, Time.fixedDeltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(sprintSpeed, moveSpeed, Time.fixedDeltaTime);
        }
    }

    void HandleMovement()
    {
        Vector3 targetPosition = playerRB.position + moveInput * currentSpeed * Time.fixedDeltaTime;
        playerRB.MovePosition(targetPosition);

        if (moveInput != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput, Vector3.up);
            playerRB.MoveRotation(targetRotation);
        }
    }
}
