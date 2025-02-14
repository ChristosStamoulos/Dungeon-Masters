using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine; // Import Cinemachine

public class PlayerLocomotion : MonoBehaviour
{
    private InputManager inputManager;
    private Rigidbody player;
    private Animator animator;
    private Vector3 moveDirection;
    private AudioSource audioSource;

    [SerializeField] private Transform cameraObject;
    [SerializeField] private CinemachineFreeLook cinemachineCamera; 

    public float movementSpeed = 7f;
    public float rotationSpeed = 15f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        player = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        cameraObject = Camera.main.transform;

        if (player == null)
            Debug.LogError("Rigidbody component not found on " + gameObject.name);

        
        if (cinemachineCamera == null)
            cinemachineCamera = FindObjectOfType<CinemachineFreeLook>();
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
        moveDirection.y = 0f;

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            animator.SetBool("IsMoving", true);
            moveDirection.Normalize();
            moveDirection *= movementSpeed;
            player.linearVelocity = new Vector3(moveDirection.x, player.linearVelocity.y, moveDirection.z);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            animator.SetBool("IsMoving", false);
            audioSource.Stop();
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
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        
        if (cinemachineCamera != null)
        {
            float targetYaw = transform.eulerAngles.y;
            cinemachineCamera.m_XAxis.Value = Mathf.LerpAngle(cinemachineCamera.m_XAxis.Value, targetYaw, Time.deltaTime * 2f);
        }
    }
}

}