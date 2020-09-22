using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerModel;
    [SerializeField]
    private GameObject playerCamera;

    private float moveSpeed;
    private float horizSens;
    private float vertSens;
    private float vertCamMoveSpeed;
    private float zoomLevel;

	private void Start()
	{
        // Cursor.lockState = CursorLockMode.Locked;
        moveSpeed = 10.0f;
        horizSens = 5.0f;
        vertSens = 0.5f;
        zoomLevel = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        playerControls();
        cameraControls();
        UIControls();
    }

    // Movement and other player model specific controls
    void playerControls()
    {
        if (Input.GetKey("w"))
        {
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
        }

        if (Input.GetKey("s"))
        {
            transform.position -= transform.forward * Time.deltaTime * moveSpeed;
        }

        if (Input.GetKey("a"))
        {
            transform.position -= transform.right * Time.deltaTime * moveSpeed;
        }

        if (Input.GetKey("d"))
        {
            transform.position += transform.right * Time.deltaTime * moveSpeed;
        }
    }

    
    void cameraControls()
    {
        // Turn the whole player object when moving the mouse horizontally
        transform.Rotate(0, Input.GetAxis("Mouse X") * horizSens, 0);

        // Constrain the camera's vertical position relative to the player
        if (((playerCamera.transform.position.y - playerModel.transform.position.y) < (5) && Input.GetAxis("Mouse Y") > 0) || ((playerCamera.transform.position.y - playerModel.transform.position.y) > 2 && Input.GetAxis("Mouse Y") < 0))
        {
            // Change the camera's position when moving the mouse vertically
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y + Input.GetAxis("Mouse Y") * vertSens, playerCamera.transform.position.z * zoomLevel);
        }
        
        // Allow the player to zoom in and out with the scroll wheel
        if (zoomLevel > 1 && Input.mouseScrollDelta.y < 0 || zoomLevel < 5 && Input.mouseScrollDelta.y > 0)
		{
            zoomLevel -= Input.mouseScrollDelta.y;
		}

        // Focus the camera on the playerModel
        playerCamera.transform.LookAt(playerModel.transform.position);
    }

    void UIControls()
    {
        if (Input.GetKeyDown("space"))
		{
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
			else
			{
                Cursor.lockState = CursorLockMode.Locked;
            }
		}
    }
}

