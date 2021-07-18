using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShipTriggerField : MonoBehaviour
{
    [SerializeField] Rigidbody AirshipRigidbody;

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>() && !other.GetComponent<VRHandScript>())
        {
            other.transform.parent = null;
        }
    }
}
