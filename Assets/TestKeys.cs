using UnityEngine;
using UnityEngine.InputSystem;

public class TestKeys : MonoBehaviour
{
    public float speed = 1f;

    public int maxFrames = 144;

    public float gravity = -1f;

    public float rotationSpeed = 1f;

    public float drag;

    public Rigidbody bulletTemplate;

    private CharacterController charControl;

    private int charge;

    private Vector3 momentum = new Vector3(0, 0, 0);

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
            transform.Rotate(0f, rotationSpeed * delts.x.ReadValue() * Time.deltaTime, 0f);

            if (keyboard.leftArrowKey.isPressed) transform.Translate(-speed, 0, 0);

            if (keyboard.rightArrowKey.isPressed) transform.Translate(speed, 0, 0);

            if (keyboard.upArrowKey.isPressed) transform.Translate(0, speed, 0);

            if (keyboard.downArrowKey.isPressed) transform.Translate(0, -speed, 0);

            if (keyboard.commaKey.isPressed) transform.Translate(0, 0, speed);

            if (keyboard.periodKey.isPressed) transform.Translate(0, 0, -speed);
        }
        else
        {
            transform.Rotate(0f, rotationSpeed * gamepad.rightStick.x.ReadValue() * Time.deltaTime, 0f);
            // all this nonsense allows the char controller to enforce walls
            // see page 57 of unity in action
            var deltaX = gamepad.leftStick.x.ReadValue() * -speed;
            var deltaZ = gamepad.leftStick.y.ReadValue() * -speed;
            var movement = new Vector3(deltaX, charControl.isGrounded ? 0f : gravity, deltaZ);
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            momentum = momentum + movement;
            // momentum is a bit broken in this instance-
            // if you drive up a ramp, at no point do you have a positive Y vector
            momentum *= 1f - drag;
            charControl.Move(momentum);

            if (charge > 0 && !gamepad.rightTrigger.isPressed)
            {
                var position = transform.position;
                position.y++;
                Rigidbody shot;
                shot = Instantiate(bulletTemplate, position, new Quaternion(0f, 0f, 0f, 0f));
                var backup = Vector3.back;
                backup.y = 0.6f;
                shot.velocity = transform.TransformDirection(backup * charge);
                charge = 0;
            }
            else if (gamepad.rightTrigger.isPressed && charge < 100)
            {
                charge += 1;
            }
        }
    }
}