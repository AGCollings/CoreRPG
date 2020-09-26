﻿using System.Collections;
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
    private float camMinDistance;
    private float camMaxDistance;

	private void Start()
	{
        Cursor.lockState = CursorLockMode.Locked;
        moveSpeed = 10.0f;
        horizSens = 1.0f;
        vertSens = 1.0f;
        camMinDistance = 2.0f;
        camMaxDistance = 10.0f;
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
            transform.position += playerModel.transform.forward * Time.deltaTime * moveSpeed;
        }

        if (Input.GetKey("s"))
        {
            transform.position -= playerModel.transform.forward * Time.deltaTime * moveSpeed;
        }

        if (Input.GetKey("a"))
        {
            transform.position -= playerModel.transform.right * Time.deltaTime * moveSpeed;
        }

        if (Input.GetKey("d"))
        {
            transform.position += playerModel.transform.right * Time.deltaTime * moveSpeed;
        }
    }


    void cameraControls()
    {
        // Turn the whole player object when moving the mouse horizontally
        //transform.Rotate(0, Input.GetAxis("Mouse X") * horizSens, 0);

        playerCamera.transform.RotateAround(playerModel.transform.position, playerModel.transform.up, Input.GetAxis("Mouse X") * vertSens);

        // Focus the camera on the playerModel
        playerCamera.transform.LookAt(playerModel.transform.position);

        // If the player holds right click, lock the player's direction to forward
        if (Input.GetMouseButton(1))
		{
            playerModel.transform.LookAt(2 * playerModel.transform.position - playerCamera.transform.position);
            // Lock rotation to y axis only
            playerModel.transform.rotation = new Quaternion(0, playerModel.transform.rotation.y, 0, playerModel.transform.rotation.w);
		}

        // Constrain vertical camera movement to between directly above the player model and before the camera enters the ground
        if (((playerCamera.transform.position.y > playerModel.transform.position.y + 1) && Input.GetAxis("Mouse Y") > 0) || ((Vector3.Angle(playerCamera.transform.forward, playerModel.transform.forward) < 75) && Input.GetAxis("Mouse Y") < 0))
        {
            // If the player moves the mouse vertically, rotate around the player around it's horizontal axis
            playerCamera.transform.RotateAround(playerModel.transform.position, playerModel.transform.right, -Input.GetAxis("Mouse Y") * vertSens);
        }

        // Allow zoom constrained within the max and min cam distances
        if ((Vector3.Distance(playerModel.transform.position, playerCamera.transform.position) > camMinDistance && Input.mouseScrollDelta.y > 0) || (Vector3.Distance(playerModel.transform.position, playerCamera.transform.position) < camMaxDistance && Input.mouseScrollDelta.y < 0))
		{
            playerCamera.transform.localPosition += (playerCamera.transform.TransformDirection(Vector3.forward) * Input.mouseScrollDelta.y);
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

