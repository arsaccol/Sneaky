using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Transform m_cameraTransform;
    public float m_CameraSensitity;

    public float m_MotionSpeed = 10f;

    private bool m_seen;


    // Start is called before the first frame update
    void Start()
    {
        m_cameraTransform = transform.GetComponentInChildren<Camera>().transform;
        m_cameraTransform.GetComponent<Camera>().fieldOfView = 80f;
        m_CameraSensitity = 200f;

        m_seen = false;
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        toggle_seen();
        wasd_motion(v, h);
    }

    private void LateUpdate()
    {
        float x = Input.GetAxis("Mouse X") * Time.deltaTime;
        float y = -Input.GetAxis("Mouse Y") * Time.deltaTime;

        move_camera(x, y);
    }

    private void wasd_motion(float Vertical, float Horizontal)
    {
        var cameraPositionProjectedOntoFloor = Vector3.ProjectOnPlane(m_cameraTransform.position, Vector3.up);
        var playerPositionProjectedOntoFloor = Vector3.ProjectOnPlane(transform.position, Vector3.up);

        var differenceVector = (cameraPositionProjectedOntoFloor - playerPositionProjectedOntoFloor).normalized;

        var forwardMotion = differenceVector * -Vertical * m_MotionSpeed * Time.deltaTime;
        var sidewaysMotion = Vector3.Cross(differenceVector, Vector3.up) * Horizontal * m_MotionSpeed * Time.deltaTime; 
        transform.position += forwardMotion + sidewaysMotion;
    }

    private void toggle_seen()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            m_seen = !m_seen;
            foreach(var animatorObj in FindObjectsOfType<Animator>())
            {
                var animatorComponent = animatorObj.GetComponent<Animator>();

                animatorComponent.SetBool("IsSeeingPlayer", m_seen);
            }
        }
    }

    private void move_camera(float X, float Y)
    {
        Vector3 cameraOffset = m_cameraTransform.position - transform.position;

        Quaternion cameraXRotation = Quaternion.AngleAxis(X * m_CameraSensitity, transform.up);
        Quaternion cameraYRotation = Quaternion.AngleAxis(Y * m_CameraSensitity, m_cameraTransform.right);
        Vector3 newCameraPosition = cameraXRotation * cameraYRotation * cameraOffset;

        m_cameraTransform.position = newCameraPosition;
        newCameraPosition += transform.position;

        m_cameraTransform.position = newCameraPosition;
        m_cameraTransform.LookAt(transform.position);
    }
}
