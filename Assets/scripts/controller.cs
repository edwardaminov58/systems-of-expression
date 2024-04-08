using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    protected CharacterController characterController;

    protected Vector3 movement;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        if (characterController == null)
            throw new UnityException("No Character Controller attached to capsule.");
    }

    void Update()
    {

        if (characterController.isGrounded == true)
        {
            movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            movement = transform.TransformDirection(movement);
            movement *= 6.0f;

            if (Input.GetKey(KeyCode.Space) == true)
                movement.y = 10.0f;
        }

        movement.y -= 20.0f * Time.deltaTime;

        characterController.Move(movement * Time.deltaTime);
    }
}