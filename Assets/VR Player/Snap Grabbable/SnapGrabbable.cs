using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapGrabbable : MonoBehaviour
{
    public bool CanBeGrabbed = true;
    public bool Grabbed = false;
    public VRHandScript Hand;

    public GameObject MovePortion;

    public float MaxX;
    public float MinX;

    public float MaxY;
    public float MinY;

    public bool LimitRotation = false;

    public float MaxZ;
    public float MinZ;

    public float MaxRotX;
    public float MinRotX;

    public float MaxRotY;
    public float MinRotY;

    public float MaxRotZ;
    public float MinRotZ;



    public void GrabbedByHand(VRHandScript NewHand)
    {
        Hand = NewHand;
        Hand.UpdatePosition = false;
    }

    public void ReleasedByHand()
    {
        Hand = null;
    }

    private void Update()
    {
        if (Hand != null)
        {
            MovePortion.transform.position = Hand.TargetPosition.transform.position;
            MovePortion.transform.localPosition = new Vector3(Mathf.Clamp(MovePortion.transform.localPosition.x, MinX, MaxX), Mathf.Clamp(MovePortion.transform.localPosition.y, MinY, MaxY), Mathf.Clamp(MovePortion.transform.localPosition.z, MinZ, MaxZ));
            Hand.transform.position = MovePortion.transform.position;

            MovePortion.transform.rotation = Hand.TargetPosition.transform.rotation;
            if (LimitRotation)
            {
                MovePortion.transform.localEulerAngles = new Vector3(Mathf.Clamp(MovePortion.transform.localEulerAngles.x, MinRotX, MaxRotX), Mathf.Clamp(MovePortion.transform.localEulerAngles.y, MinRotY, MaxRotY), Mathf.Clamp(MovePortion.transform.localEulerAngles.z, MinRotZ, MaxRotZ));
            }
            Hand.transform.rotation = MovePortion.transform.rotation;


            if (Vector3.Distance(MovePortion.transform.position, Hand.TargetPosition.transform.position) > 0.5f)
            {
                Hand.ReleaseSnapGrabObject();
            }
        }
    }
}
