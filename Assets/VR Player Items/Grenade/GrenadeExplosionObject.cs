using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosionObject : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 10f);
        Explode();
    }

    public float radius = 50.0F;
    public float power = 1.0F;

    void Explode()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.attachedRigidbody;

            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F, ForceMode.Impulse);
            }
        }
    }
}