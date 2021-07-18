using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VRPlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 MovementJoystickInput;
    public SteamVR_Action_Boolean MovementJoystickPressed;

    private float MovementJoystickDeadZone = 0.1f;
    private float MovementSpeed = 3f;

    [SerializeField] GameObject PlayerMovementDirectionObject;
    [SerializeField] GameObject PlayerHead;

    public SteamVR_Action_Vector2 RotationJoystickInput;

    bool AwaitingRotationJoystickResetLEFT = false;
    bool AwaitingRotationJoystickResetRIGHT = false;

    [SerializeField] GameObject PlayerObject;

    float SnapRotationDegrees = 45f;

    float RotationJoystickDeadZone = 0.75f;

    [SerializeField] GameObject CameraOffsetObject;

    [SerializeField] GameObject LeftHandObject;
    [SerializeField] GameObject RightHandObject;

    [SerializeField] float MaxMovementBoundary;

    [SerializeField] GameObject LeftHandFollowerObject;
    [SerializeField] GameObject RightHandFollowerObject;

    [SerializeField] GameObject Airship;

    private void Awake()
    {
        gameObject.transform.localPosition = new Vector3(Mathf.Clamp(gameObject.transform.localPosition.x, -MaxMovementBoundary, MaxMovementBoundary), gameObject.transform.localPosition.y, Mathf.Clamp(gameObject.transform.localPosition.z, -MaxMovementBoundary, MaxMovementBoundary));
    }

    private void Start()
    {
        gameObject.transform.localPosition = new Vector3(Mathf.Clamp(gameObject.transform.localPosition.x, -MaxMovementBoundary, MaxMovementBoundary), gameObject.transform.localPosition.y, Mathf.Clamp(gameObject.transform.localPosition.z, -MaxMovementBoundary, MaxMovementBoundary));
    }

    void Update()
    {
        PlayerMovementDirectionObject.transform.eulerAngles = new Vector3(0, PlayerHead.transform.eulerAngles.y, 0);

        if (MovementJoystickInput.axis.x > MovementJoystickDeadZone || MovementJoystickInput.axis.x < -MovementJoystickDeadZone)
        {
            gameObject.transform.position += PlayerMovementDirectionObject.transform.right.normalized * MovementSpeed * Time.deltaTime * MovementJoystickInput.axis.x;
        }
        if (MovementJoystickInput.axis.y > MovementJoystickDeadZone || MovementJoystickInput.axis.y < -MovementJoystickDeadZone)
        {
            gameObject.transform.position += PlayerMovementDirectionObject.transform.forward.normalized * MovementSpeed * Time.deltaTime * MovementJoystickInput.axis.y;
        }

        gameObject.transform.localPosition = new Vector3(Mathf.Clamp(gameObject.transform.localPosition.x, -MaxMovementBoundary, MaxMovementBoundary), gameObject.transform.localPosition.y, Mathf.Clamp(gameObject.transform.localPosition.z, -MaxMovementBoundary, MaxMovementBoundary));

        if (RotationJoystickInput.axis.x < -RotationJoystickDeadZone && !AwaitingRotationJoystickResetLEFT)
        {
            LeftHandObject.transform.parent = null;
            RightHandObject.transform.parent = null;

            AwaitingRotationJoystickResetLEFT = true;

            CameraOffsetObject.transform.parent = null;
            PlayerObject.transform.position = new Vector3(PlayerHead.transform.position.x, PlayerObject.transform.position.y, PlayerHead.transform.position.z);
            CameraOffsetObject.transform.parent = PlayerObject.transform;
            LeftHandObject.transform.parent = PlayerObject.transform;
            RightHandObject.transform.parent = PlayerObject.transform;
            PlayerObject.transform.eulerAngles = new Vector3(0, PlayerObject.transform.eulerAngles.y - SnapRotationDegrees, 0);


        }
        if (RotationJoystickInput.axis.x > -RotationJoystickDeadZone && AwaitingRotationJoystickResetLEFT)
        {
            AwaitingRotationJoystickResetLEFT = false;
        }



        if (RotationJoystickInput.axis.x > RotationJoystickDeadZone && !AwaitingRotationJoystickResetRIGHT)
        {
            LeftHandObject.transform.parent = null;
            RightHandObject.transform.parent = null;

            AwaitingRotationJoystickResetRIGHT = true;

            CameraOffsetObject.transform.parent = null;
            PlayerObject.transform.position = new Vector3(PlayerHead.transform.position.x, PlayerObject.transform.position.y, PlayerHead.transform.position.z);
            CameraOffsetObject.transform.parent = PlayerObject.transform;
            LeftHandObject.transform.parent = PlayerObject.transform;
            RightHandObject.transform.parent = PlayerObject.transform;
            PlayerObject.transform.eulerAngles = new Vector3(0, PlayerObject.transform.eulerAngles.y + SnapRotationDegrees, 0);


        }
        if (RotationJoystickInput.axis.x < RotationJoystickDeadZone && AwaitingRotationJoystickResetRIGHT)
        {
            AwaitingRotationJoystickResetRIGHT = false;
        }




        LeftHandObject.transform.parent = null;
        RightHandObject.transform.parent = null;

        CameraOffsetObject.transform.parent = null;
        PlayerObject.transform.position = new Vector3(PlayerHead.transform.position.x, PlayerObject.transform.position.y, PlayerHead.transform.position.z);
        CameraOffsetObject.transform.parent = PlayerObject.transform;
        LeftHandObject.transform.parent = PlayerObject.transform;
        RightHandObject.transform.parent = PlayerObject.transform;
    }
}
