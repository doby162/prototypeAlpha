using UnityEngine;
using UnityEngine.InputSystem;

public class TestKeys : MonoBehaviour
{
    public float speed = 0.1f;

    public int maxFrames = 144;

    public float gravity = -.1f;

    private CharacterController charControl;

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("start");
        Application.targetFrameRate = 144; // the maximum reasonable value
        charControl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        var mouse = Mouse.current;
        var gamepad = Gamepad.current; // specifically an xbox controller
        var keyboard = Keyboard.current;

        if (gamepad == null)
        {
            //keyboard and mouse controls
            var delts = mouse.delta;
            transform.Rotate(0f, 1f * delts.x.ReadValue(), 0f);

            if (keyboard.leftArrowKey.isPressed) transform.Translate(-speed, 0, 0);

            if (keyboard.rightArrowKey.isPressed) transform.Translate(speed, 0, 0);

            if (keyboard.upArrowKey.isPressed) transform.Translate(0, speed, 0);

            if (keyboard.downArrowKey.isPressed) transform.Translate(0, -speed, 0);

            if (keyboard.commaKey.isPressed) transform.Translate(0, 0, speed);

            if (keyboard.periodKey.isPressed) transform.Translate(0, 0, -speed);
        }
        else
        {
            transform.Rotate(0f, 1f * gamepad.rightStick.x.ReadValue(), 0f);
            // transform.Translate(gamepad.leftStick.x.ReadValue() * -speed, 0, gamepad.leftStick.y.ReadValue() * -speed);
            // all this nonsense allows the char controller to enforce walls
            // see page 57 of unity in action
            var deltaX = gamepad.leftStick.x.ReadValue() * -speed;
            var deltaZ = gamepad.leftStick.y.ReadValue() * -speed;
            var movement = new Vector3(deltaX, gravity, deltaZ);
            // movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            charControl.Move(movement);
        }
    }
}