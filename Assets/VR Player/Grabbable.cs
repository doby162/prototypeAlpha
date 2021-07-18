using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public bool Grabbed = false;

    public void GrabbedByHand() {
        Grabbed = true;
    }

    public void ReleasedByHand() {
        Grabbed = false;
    }
}
