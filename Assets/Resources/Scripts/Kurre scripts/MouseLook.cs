using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    float mouseX;
    float mouseY;

    public float mouseSensitivity = 100f;

    public Transform player;

    float xRotation;

   

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, -0f);
        player.Rotate(Vector3.up * mouseY);

    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
