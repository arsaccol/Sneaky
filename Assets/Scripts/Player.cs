using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform CameraTransform;
    public float CameraFOV = 45f;
    public float CameraSensitivity = 10f;
    public float MovementSpeed = 4f;
    private CharacterController characterController;


    void Start()
    {
        CameraTransform = GameObject.Find("PlayerCamera").transform;
        characterController = GetComponent<CharacterController>();
        
    }


    private void Update()
    {
        movePlayer();
    }

    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Camera Motion X");
        float mouseY = Input.GetAxis("Camera Motion Y");

        moveCamera(mouseX, -mouseY);

    }


    private void movePlayer()
    {
        float horizontalInputAxis = Input.GetAxis("Horizontal");
        float verticalInputAxis = Input.GetAxis("Vertical");



        movePlayerForwardMotion(verticalInputAxis);
        movePlayerStrafeMotion(horizontalInputAxis);
    }


    private void movePlayerForwardMotion(float verticalInputAxis)
    {
        var cameraProjectedForwardDirection = Vector3.ProjectOnPlane(CameraTransform.forward, Vector3.up).normalized;
        characterController.SimpleMove(cameraProjectedForwardDirection * verticalInputAxis * MovementSpeed);
    }


    private void movePlayerStrafeMotion(float horizontalInputAxis)
    {
        var cameraProjectedRightDirection = Vector3.ProjectOnPlane(CameraTransform.right, Vector3.up).normalized;
        characterController.SimpleMove(cameraProjectedRightDirection * horizontalInputAxis * MovementSpeed);
    }


    private void moveCamera(float x, float y)
    {
        transform.RotateAround(this.transform.position, Vector3.up, x * CameraSensitivity);
        CameraTransform.RotateAround(transform.position, transform.right, y * CameraSensitivity);
    }
}
