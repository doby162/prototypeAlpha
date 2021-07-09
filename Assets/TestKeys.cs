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
        Vector3 pos = this.transform.position;

        var keyboard = Keyboard.current;
        if (keyboard == null)
            Debug.Log("no keyboard?");

        if (keyboard.leftArrowKey.wasPressedThisFrame)
        {
            pos.x--;
        }
        if (keyboard.rightArrowKey.wasPressedThisFrame)
        {
            pos.x++;
        }
        if (keyboard.upArrowKey.wasPressedThisFrame)
        {
            pos.y++;
        }
        if (keyboard.downArrowKey.wasPressedThisFrame)
        {
            pos.y--;
        }
        if (keyboard.commaKey.wasPressedThisFrame)
        {
            pos.z++;
        }
        if (keyboard.periodKey.wasPressedThisFrame)
        {
            pos.z--;
        }

        this.transform.position = pos;


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
