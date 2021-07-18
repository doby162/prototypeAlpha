using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirshipLongRangeCannonController : MonoBehaviour
{
    [SerializeField] GameObject Cannon;
    [SerializeField] GameObject CannonController;
    [SerializeField] GameObject CannonRaycastPoint;
    [SerializeField] GameObject CannonRaycastLine;

    float RotationSpeed = 7.5f;

    public float CurrentWeaponCharge = 0;
    float MaxWeaponCharge = 3f;

    [SerializeField] GameObject Projectile;

    [SerializeField] GameObject ProjectileSpawnPoint;

    [SerializeField] GameObject ChargeScaleObject;

    private void Update()
    {
        Cannon.transform.localRotation = Quaternion.Lerp(Cannon.transform.localRotation, new Quaternion(CannonController.transform.localRotation.x, CannonController.transform.localRotation.y, CannonController.transform.localRotation.z, CannonController.transform.localRotation.w), Time.deltaTime * RotationSpeed);

        if (CannonController.GetComponent<SnapGrabbable>().Hand != null)
        {
            if (CannonController.GetComponent<SnapGrabbable>().Hand.PointerFingerTrigger.axis > 0.7f)
            {
                CurrentWeaponCharge += Time.deltaTime;

                if (CurrentWeaponCharge > MaxWeaponCharge)
                {
                    FireWeapon();
                }
            }
        }
        else
        {
            CurrentWeaponCharge = 0;
        }

        ChargeScaleObject.transform.localScale = new Vector3(1, 1, CurrentWeaponCharge / MaxWeaponCharge);
    }

    public void FireWeapon()
    {
        CurrentWeaponCharge = 0;
        Instantiate(Projectile, ProjectileSpawnPoint.transform.position, ProjectileSpawnPoint.transform.rotation);
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(CannonRaycastPoint.transform.position, CannonRaycastPoint.transform.forward, out hit))
        {
            CannonRaycastLine.transform.localScale = new Vector3(1, 1, Vector3.Distance(hit.point, CannonRaycastLine.transform.position));
        }
        else
        {
            CannonRaycastLine.transform.localScale = new Vector3(1, 1, 999f);

        }
    }
}
