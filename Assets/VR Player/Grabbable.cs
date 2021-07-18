using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public bool Grabbed = false;
    public VRHandScript GrabbedHand;
    public bool CanBeGrabbed = true;

    public void GrabbedByHand(VRHandScript NewHand)
    {
        GrabbedHand = NewHand;
        Grabbed = true;
    }

    public void ReleasedByHand()
    {
        Grabbed = false;
    }

    public void TopButtonPushed()
    {

    }

    public void BottomButtonPushed()
    {

    }
}
