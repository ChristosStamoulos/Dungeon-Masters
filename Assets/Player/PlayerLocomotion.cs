using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{

    private InputManager inputManager;

    [SerializeField]
    private Transform cameraObject;

    private Rigidbody player;

    private Animator animator;

    public float movementSpeed = 7f;
    public float rotationSpeed = 15f;

    private Vector3 moveDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        player = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;

        if (player == null)
            Debug.LogError("Rigidbody component not found on " + gameObject.name);
    }

    public void HandlePlayer()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {

        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection += cameraObject.right * inputManager.horizontalInput;
        //moveDirection += Quaternion.AngleAxis(cameraObject.rotation.eulerAngles.y, Vector3.up) * moveDirection;
        moveDirection.y = 0f;

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            animator.SetBool("IsMoving", true);
            moveDirection.Normalize();
            moveDirection *= movementSpeed;
            player.linearVelocity = new Vector3(moveDirection.x, player.linearVelocity.y, moveDirection.z);
        }
        else
        {
            animator.SetBool("IsMoving", false);
            player.linearVelocity = new Vector3(0, player.linearVelocity.y, 0);
        }
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = cameraObject.forward * inputManager.verticalInput + cameraObject.right * inputManager.horizontalInput;

        targetDirection.y = 0f;

        if (targetDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection.normalized);
            Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }
    }
}
