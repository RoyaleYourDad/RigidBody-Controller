using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyController4 : MonoBehaviour {
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float sensitivity;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float moveForce;
    [SerializeField] private float maxMoveSpeed;

    [SerializeField] private Transform checkSphere;
    [SerializeField] private float checkSphereRadius;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float jumpForce;

    [SerializeField] private float gravityScale;

    private bool grounded;
    private float pitch;
    private float yaw;

    private void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        yaw += Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        pitch -= Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.localRotation = Quaternion.Euler(0, yaw, 0);
        mainCamera.localRotation = Quaternion.Euler(pitch, 0, 0);

        grounded = Physics.CheckSphere(checkSphere.position, checkSphereRadius, groundLayerMask);
        if(Input.GetKeyDown(KeyCode.Space) && grounded) {
            rigidBody.AddForce(new Vector3(0, jumpForce, 0));
        }
    }

    private void FixedUpdate() {
        rigidBody.AddRelativeForce(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * moveForce);

        Vector3 vel = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
        rigidBody.AddForce(-vel * (moveForce / maxMoveSpeed), ForceMode.Acceleration);

        rigidBody.AddForce(Physics.gravity.y * Vector3.up * gravityScale, ForceMode.Acceleration);
    }
}