using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VRHandScript : MonoBehaviour
{
    public SteamVR_Action_Single GrabTrigger;

    float MinimumGrabAmmmount = 0.5f;

    bool GrabbingObject = false;

    public Grabbable GrabbedObject;

    [SerializeField] GameObject TargetPosition;

    private void Update()
    {
        if (GrabTrigger.axis < 0.4f && GrabbingObject && GrabbedObject != null)
        {
            GrabbedObject.ReleasedByHand();
            GrabbedObject.gameObject.AddComponent<Rigidbody>();
            GrabbedObject.GetComponent<Rigidbody>().velocity = gameObject.transform.GetComponent<Rigidbody>().velocity;
        }

        gameObject.GetComponent<Rigidbody>().MovePosition(TargetPosition.transform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        print("dsgj9e0sgn90wehg80             1");
        if (!GrabbingObject)
        {
            other.GetComponent<Renderer>().material.color = Color.red;
            if (other.gameObject.GetComponent<Grabbable>())
            {
                print("dsgj9e0sgn90wehg80             3");
                if (!other.gameObject.GetComponent<Grabbable>().Grabbed)
                {
                    print("dsgj9e0sgn90wehg80             4");
                    if (GrabTrigger.axis > MinimumGrabAmmmount)
                    {
                        print("dsgj9e0sgn90wehg80             5");
                        GrabObject(other.gameObject.GetComponent<Grabbable>());
                    }
                }
            }
        }
    }

    public void GrabObject(Grabbable NewGrabObject)
    {
        print("dsgj9e0sgn90wehg80             6");
        GrabbedObject = NewGrabObject;
        print("dsgj9e0sgn90wehg80             7");
        NewGrabObject.GrabbedByHand();
        print("dsgj9e0sgn90wehg80             8");
        if (NewGrabObject.GetComponent<Rigidbody>())
        {
            print("dsgj9e0sgn90wehg80             9");
            Destroy(NewGrabObject.GetComponent<Rigidbody>());
        }
        print("dsgj9e0sgn90wehg80             10");
        NewGrabObject.transform.parent = gameObject.transform;
    }
}
