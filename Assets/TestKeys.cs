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
            Debug.Log("get a gamepad, or fill in this section");
        }
        else
        {
            transform.Rotate(0f, rotationSpeed * gamepad.leftStick.x.ReadValue() * Time.deltaTime, 0f);
            // all this nonsense allows the char controller to enforce walls
            // see page 57 of unity in action
            var deltaZ = gamepad.leftStick.y.ReadValue() * speed;
            var movement = new Vector3(0f, charControl.isGrounded ? 0f : gravity, deltaZ);
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            momentum = momentum + movement;
            
            if (charge > 0 && gamepad.xButton.wasReleasedThisFrame)
            {
                momentum.y += charge / 100;
                charge = 0;
            }
            
            momentum *= 1f - drag;
            charControl.Move(momentum);

            if (charge > 0 && gamepad.rightTrigger.wasReleasedThisFrame)
            {
                var position = transform.position;
                position.y++;
                Rigidbody shot;
                shot = Instantiate(bulletTemplate, position, new Quaternion(0f, 0f, 0f, 0f));
                var forwardup = Vector3.forward;
                forwardup.y = 0.6f;
                shot.velocity = transform.TransformDirection(forwardup * charge);
                charge = 0;
            }
            else if (gamepad.rightTrigger.isPressed || gamepad.xButton.isPressed && charge < 100)
            {
                charge += 1;
            }
        }
    }
}