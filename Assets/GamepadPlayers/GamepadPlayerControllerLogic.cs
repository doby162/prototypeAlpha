using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = System.Object;

public class GamepadPlayerControllerLogic : MonoBehaviour
{
    public float speed = 1f;
    public float gravity = -1f;
    public float rotationSpeed = 1f;
    public float drag;

    private Vector3 momentum = new Vector3(0, 0, 0);
    private Vector2 movementInput;
    private Rigidbody carRigidBody;
    private int charge;

    // Buttons
    private bool jumpButtonHeld = false;
    private bool jumpButtonReleased = false;

    [SerializeField] WheelCollider Wheel1;
    [SerializeField] WheelCollider Wheel2;
    [SerializeField] WheelCollider Wheel3;
    [SerializeField] WheelCollider Wheel4;

    private void Start()
    {
        carRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        /* Rotate the "car" so that it drives like a car. No strafing. */
        //transform.Rotate(0f, rotationSpeed * movementInput.x * Time.deltaTime, 0f);

        /* Add force to the car so it goes . . . forward? */

        /* Dash - on RELEASE of "Dash" button (A on xBox controller)*/
        // Stuck in the middle, because it wants to modify momentum before it is applied to the car
        // if (charge > 0 && gamepad.aButton.wasReleasedThisFrame)
        // {
        //     deltaZ += charge * 3;
        //     charge = 0;
        // }


        /* Jump - on RELEASE of "Jump" button (X on xBox controller)*/
        // Stuck in the middle, because it wants to modify momentum before it is applied to the car
        // if (charge > 0 && jumpButtonReleased)
        // {
        //     Debug.Log("Jump now");
        //     jumpButtonReleased = false;
        //     momentum.y += charge / 100;
        //     charge = 0;
        // }

        /* "Charge" - If any of the action buttons is held down, "charge" to get ready for release */
        // if (gamepad.rightTrigger.isPressed || gamepad.xButton.isPressed ||
        //     gamepad.aButton.isPressed && charge < 100)
        // {
        //     charge += 1;
        // }
        if (jumpButtonHeld && charge < 100)
        {
            Debug.Log("Jump charge +1");
            charge += 1;
        }


        Wheel1.motorTorque = movementInput.y * speed;
        Wheel2.motorTorque = movementInput.y * speed;
        Wheel3.motorTorque = movementInput.y * speed;
        Wheel4.motorTorque = movementInput.y * speed;

        Wheel1.steerAngle = movementInput.x * 45f;
        Wheel2.steerAngle = movementInput.x * 45f;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    public void OnJumpButtonInput(InputAction.CallbackContext ctx)
    {
        Debug.Log("Jump Button interaction happened");
        // InputAction buttonValue = ctx.action.GetType();
        Debug.Log(ctx.action.ReadValue<float>());


        // jumpButtonHeld = ctx.performed;
        // jumpButtonReleased = ctx.canceled;
    }
}