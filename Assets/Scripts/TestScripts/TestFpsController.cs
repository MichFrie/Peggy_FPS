using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFpsController : MonoBehaviour
{
    public GameObject cam;
    Rigidbody rb;
    CapsuleCollider capsule;
    float speed = 0.2f;

    float mouseCameraSensitivityX = 4.0f;
    float mouseCameraSensitivityY = 4.0f;

    Quaternion cameraRotation;
    Quaternion characterRotation;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        capsule = this.GetComponent<CapsuleCollider>();

        cameraRotation = cam.transform.localRotation;
        characterRotation = this.transform.localRotation;
    }


    private void FixedUpdate()
    {
        CharMovement();
        TryJump();
        MouseLook();
    }

    void CharMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        transform.position += new Vector3(x, 0, z) * speed;
    }

    void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
            rb.AddForce(0, 400, 0);
    }

    void MouseLook()
    {

        float yRotation = Input.GetAxis("Mouse X") * mouseCameraSensitivityX;
        float xRotation = Input.GetAxis("Mouse Y") * mouseCameraSensitivityY;

        cameraRotation *= Quaternion.Euler(-xRotation, 0, 0);
        characterRotation *= Quaternion.Euler(0, yRotation, 0);

        this.transform.localRotation = characterRotation;
        cam.transform.localRotation = cameraRotation;

    }

    bool isGrounded()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, capsule.radius, Vector3.down, out hit, (capsule.height / 2f) - capsule.radius + 0.1f))
        {
            return true;
        }
        return false;
    }
}
