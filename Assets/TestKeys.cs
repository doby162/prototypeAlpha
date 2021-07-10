using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestKeys : MonoBehaviour
{
    public float speed = 0.1f;

    public int maxFrames = 144;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        Application.targetFrameRate = 144; // the maximum reasonable value
    }

    // Update is called once per frame
    void Update()
    {
        var mouse = Mouse.current;
        var delts = mouse.delta;
        transform.Rotate(0f, 1f * delts.x.ReadValue(), 0f);
        
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
