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

	private void Start()
	{
        Cursor.lockState = CursorLockMode.Locked;
        moveSpeed = 10.0f;
        horizSens = 1.0f;
        vertSens = 1.0f;
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

        // Focus the camera on the playerModel
        playerCamera.transform.LookAt(playerModel.transform.position);

        // Constrain vertical camera movement to somewhere between directly above the player model and before the camera enters the ground
        if (((playerCamera.transform.position.y > playerModel.transform.position.y + 1) && Input.GetAxis("Mouse Y") > 0) || ((Vector3.Angle(playerCamera.transform.forward, playerModel.transform.forward) < 75) && Input.GetAxis("Mouse Y") < 0))
        {
            // If the player moves the mouse vertically, rotate around the player around it's horizontal axis
            playerCamera.transform.RotateAround(playerModel.transform.position, playerModel.transform.right, -Input.GetAxis("Mouse Y") * vertSens);
        }
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

