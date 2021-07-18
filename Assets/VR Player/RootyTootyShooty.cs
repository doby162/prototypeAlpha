using System.Collections;
using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.OpenXR.Features.Interactions;
using UnityEngine.XR.OpenXR.Input;
using Valve.VR;

public class RootyTootyShooty : MonoBehaviour
{
    public SteamVR_Action_Boolean trigger;
    public Rigidbody bulletTemplate;
    [SerializeField] GameObject PlayerMovementDirectionObject;

    private int charge = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger.state)
        {
            charge++;
        }
        else if (!trigger.state && charge > 0)
        {
            // var position = transform.position;
            var position = PlayerMovementDirectionObject.transform.position;
            position.y++;
            Rigidbody shot;
            shot = Instantiate(bulletTemplate, position, new Quaternion(0f, 0f, 0f, 0f));
            var forwardup = PlayerMovementDirectionObject.transform.forward;
            shot.velocity = transform.TransformDirection(forwardup * charge);
            charge = 0;
        }
    }
}