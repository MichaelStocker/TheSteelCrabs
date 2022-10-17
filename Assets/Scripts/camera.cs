using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField] int sensHor = 600;
    [SerializeField] int sensVert = 600;

    [SerializeField] int lockVertMin;
    [SerializeField] int lockVertMax;

    [SerializeField] bool invert;

    float xRotation;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        sensHor = gameManager.instance.sensHor;
        sensVert = gameManager.instance.sensVert;
        
        // Get the Input
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensHor;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensVert;

        if (invert) xRotation += mouseY;
        else xRotation -= mouseY;
        // Clamp Rotation
        xRotation = Mathf.Clamp(xRotation, lockVertMin, lockVertMax);

        // Camera Rotation Along X Axis
        transform.localRotation = Quaternion.Euler(xRotation,0, 0);

        // Rotate the Player
        transform.parent.Rotate(Vector3.up * mouseX);

    }
}
