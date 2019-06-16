using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform m_CameraTransform;
    public float m_CameraFOV = 45f;
    public float m_CameraSensitivity = 10f;

    // Start is called before the first frame update
    void Start()
    {
        m_CameraTransform = GameObject.Find("PlayerCamera").transform;
        

        
    }

    // Update is called once per frame
    private void Update()
    {
        float motionAxisHorizontal = Input.GetAxis("Horizontal");
        float motionAxisVertical = Input.GetAxis("Vertical");

        movePlayer(motionAxisHorizontal, motionAxisVertical);
    }

    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        moveCamera(mouseX, -mouseY);

    }

    private void movePlayer(float horizontalInputAxis, float verticalInputAxis)
    {
        movePlayerForwardMotion(verticalInputAxis);
        movePlayerStrafeMotion(horizontalInputAxis);
    }


    private void movePlayerForwardMotion(float verticalInputAxis)
    {
        var cameraProjectedForwardDirection = Vector3.ProjectOnPlane(m_CameraTransform.forward, Vector3.up).normalized;
        transform.position += cameraProjectedForwardDirection * verticalInputAxis;
    }


    private void movePlayerStrafeMotion(float horizontalInputAxis)
    {
        var cameraProjectedRightDirection = Vector3.ProjectOnPlane(m_CameraTransform.right, Vector3.up).normalized;
        transform.position += cameraProjectedRightDirection * horizontalInputAxis;
    }



    private void moveCamera(float x, float y)
    {
        //m_CameraTransform.RotateAround(this.transform.position, Vector3.down, x * m_CameraSensitivity);
        transform.RotateAround(this.transform.position, Vector3.up, x * m_CameraSensitivity);
        m_CameraTransform.RotateAround(transform.position, transform.right, y * m_CameraSensitivity);

    }
}
