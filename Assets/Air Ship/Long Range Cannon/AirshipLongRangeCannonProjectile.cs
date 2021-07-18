using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirshipLongRangeCannonProjectile : MonoBehaviour
{
    [SerializeField] GameObject ExplosionObject;

    private void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * 45f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(ExplosionObject, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
}
