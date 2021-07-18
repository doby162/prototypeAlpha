using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VRHandScript : MonoBehaviour
{
    public SteamVR_Action_Single GrabTrigger;
    public SteamVR_Action_Boolean TopButton;
    public SteamVR_Action_Boolean BottomButton;
    public SteamVR_Action_Single PointerFingerTrigger;

    float MinimumGrabAmmmount = 0.5f;

    bool GrabbingObject = false;

    public Grabbable GrabbedObject;

    public GameObject TargetPosition;

    bool LastTopButtonState = false;

    bool LastBottomButtonState = false;

    bool DebugMode = false;

    public bool UpdatePosition = true;

    public bool CanGrabObjects = true;

    bool SnapGrabbingObject = false;

    public SnapGrabbable SnapGrabbedObject;

    [SerializeField] AirshipController Airship;

    [SerializeField] Rigidbody RigidbodyFollowerPortion;

    private void Start()
    {
        gameObject.transform.parent = null;
        RigidbodyFollowerPortion.transform.parent = Airship.transform;
    }

    private void Update()
    {
        if (GrabTrigger.axis < MinimumGrabAmmmount - 0.1f)
        {
            if (GrabbingObject && GrabbedObject != null)
            {
                ReleaseGrabbedObject();
            }
            else
            {
                if (SnapGrabbingObject && SnapGrabbedObject != null)
                {
                    ReleaseSnapGrabObject();
                }
            }
        }

        if (TopButton.state && !LastTopButtonState)
        {
            TopButtonPressed();
        }

        LastTopButtonState = TopButton.state;

        if (BottomButton.state && !LastBottomButtonState)
        {
            BottomButtonPushed();
        }

        LastBottomButtonState = BottomButton.state;

        if (UpdatePosition)
        {
            RigidbodyFollowerPortion.MovePosition(TargetPosition.transform.position);
            RigidbodyFollowerPortion.MoveRotation(TargetPosition.transform.rotation);

            gameObject.transform.position = TargetPosition.transform.position;
            gameObject.transform.rotation = TargetPosition.transform.rotation;
        }
    }

    public void ReleaseSnapGrabObject()
    {
        SnapGrabbedObject.ReleasedByHand();
        UpdatePosition = true;
        SnapGrabbingObject = false;
        SnapGrabbedObject = null;

        gameObject.transform.position = TargetPosition.transform.position;
        gameObject.transform.rotation = TargetPosition.transform.rotation;
    }

    public void TopButtonPressed()
    {
        if (GrabbingObject && GrabbedObject != null)
        {
            if (GrabbedObject.GetComponent<VRPlayerGrenade>())
            {
                GrabbedObject.GetComponent<VRPlayerGrenade>().Activate();
            }
        }
    }

    public void BottomButtonPushed()
    {
        if (GrabbingObject && GrabbedObject != null)
        {

        }
    }

    public void ReleaseGrabbedObject()
    {
        GrabbedObject.ReleasedByHand();

        GrabbedObject.transform.parent = Airship.gameObject.transform;

        GrabbedObject.gameObject.AddComponent<Rigidbody>();

        GrabbedObject.GetComponent<Rigidbody>().velocity = RigidbodyFollowerPortion.velocity + Airship.GetComponent<Rigidbody>().velocity;

        GrabbedObject = null;
        GrabbingObject = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!GrabbingObject)
        {
            if (GrabTrigger.axis > MinimumGrabAmmmount)
            {
                if (DebugMode)
                {
                    other.GetComponent<Renderer>().material.color = Color.red;
                }
                if (other.gameObject.GetComponent<Grabbable>())
                {
                    if (!other.gameObject.GetComponent<Grabbable>().Grabbed)
                    {
                        if (other.gameObject.GetComponent<Grabbable>().CanBeGrabbed)
                        {
                            GrabObject(other.gameObject.GetComponent<Grabbable>());
                        }
                    }
                }
                else
                {
                    if (other.GetComponent<SnapGrabbable>())
                    {
                        if (other.GetComponent<SnapGrabbable>().Hand == null)
                        {
                            other.GetComponent<SnapGrabbable>().GrabbedByHand(this);
                            SnapGrabbingObject = true;
                            SnapGrabbedObject = other.GetComponent<SnapGrabbable>();
                        }
                    }
                }
            }
        }
    }

    public void GrabObject(Grabbable NewGrabObject)
    {
        GrabbedObject = NewGrabObject;
        NewGrabObject.GrabbedByHand(this);
        GrabbingObject = true;
        if (NewGrabObject.GetComponent<Rigidbody>())
        {
            Destroy(NewGrabObject.GetComponent<Rigidbody>());
        }
        NewGrabObject.transform.parent = gameObject.transform;
    }
}
