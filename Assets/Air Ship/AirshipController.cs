using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirshipController : MonoBehaviour
{
    [SerializeField] GameObject SpeedSlider;
    [SerializeField] GameObject DirectionIndicator;

    float MovementSpeedModifier = 10f;
    float RotationSpeedModifier = 100f;

    [SerializeField] GameObject RotationSlider;

    private void Update()
    {
        gameObject.transform.position += DirectionIndicator.transform.forward.normalized * SpeedSlider.transform.localPosition.z * MovementSpeedModifier * Time.deltaTime;

        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y + (RotationSlider.transform.localPosition.x * Time.deltaTime * RotationSpeedModifier), gameObject.transform.eulerAngles.z);

        //gameObject.GetComponent<Rigidbody>().MovePosition(TargetPositionObject.transform.position);
        //gameObject.GetComponent<Rigidbody>().MoveRotation(TargetPositionObject.transform.rotation);

        float SnapValue = 0.05f;
        if ((RotationSlider.transform.localPosition.x < SnapValue && RotationSlider.transform.localPosition.x > 0) && (RotationSlider.transform.localPosition.x > -SnapValue && RotationSlider.transform.localPosition.x < 0))
        {
            RotationSlider.transform.localPosition = Vector3.zero;
        }
    }
}
