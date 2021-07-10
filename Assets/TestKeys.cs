using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestKeys : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {
        var mouse = Mouse.current;
        var delts = mouse.delta;
        transform.Rotate(0f, 1f * delts.x.ReadValue(), 0f);

        float speed = 0.01f;

        var keyboard = Keyboard.current;

        if (keyboard.leftArrowKey.isPressed)
        {
            transform.Translate(-speed, 0, 0);
        }
        if (keyboard.rightArrowKey.isPressed)
        {
            transform.Translate(speed, 0, 0);

        }
        if (keyboard.upArrowKey.isPressed)
        {
            transform.Translate(0, speed, 0);

        }
        if (keyboard.downArrowKey.isPressed)
        {
            transform.Translate(0, -speed, 0);

        }
        if (keyboard.commaKey.isPressed)
        {
            transform.Translate(0, 0, speed);

        }
        if (keyboard.periodKey.isPressed)
        {
            transform.Translate(0, 0, -speed);
        }

        var gamepad = Gamepad.current;
        if (gamepad == null)
            return;



        if (gamepad.rightTrigger.wasPressedThisFrame)
        {
            Debug.Log("right trigger");
        }

        //Vector2 move = gamepad.leftStick.ReadValue();
        //Debug.Log(move);

    }
}
