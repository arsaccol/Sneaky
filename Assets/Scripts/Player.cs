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
        
    }

    private void LateUpdate()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        moveCamera(x, -y);

    }

    private void moveCamera(float x, float y)
    {
        //m_CameraTransform.RotateAround(this.transform.position, Vector3.down, x * m_CameraSensitivity);
        transform.RotateAround(this.transform.position, Vector3.up, x * m_CameraSensitivity);
        m_CameraTransform.RotateAround(transform.position, transform.right, y * m_CameraSensitivity);

    }
}
