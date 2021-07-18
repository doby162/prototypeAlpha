using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarCanon : MonoBehaviour
{
    public Rigidbody bulletTemplate;

    private bool fireButtonIsPressed = false;
    private int charge = 0;

    [SerializeField] GameObject BulletSpawnPoint;

    // Update is called once per frame
    void Update()
    {
        // Charge the canon
        if (fireButtonIsPressed && charge < 100)
        {
            charge += 1;
        }
    }

    public void OnFireButtonInput(InputAction.CallbackContext ctx)
    {
        Debug.Log("Fire Button interaction happened");
        // InputAction buttonValue = ctx.action.GetType();
        // Debug.Log(ctx.action.ReadValue<float>());

        bool fireButtonWasPressed = fireButtonIsPressed;

        fireButtonIsPressed = ctx.action.ReadValue<float>() == 1f;

        /* Fire - on RELEASE of "Fire" button (Right Trigger on xBox controller) */
        if (fireButtonWasPressed && !fireButtonIsPressed && charge > 0)
        {
            var position = transform.position;
            position.y++;
            Rigidbody shot;
            shot = Instantiate(bulletTemplate, BulletSpawnPoint.transform.position, new Quaternion(0f, 0f, 0f, 0f));
            var forwardUp = Vector3.forward;
            forwardUp.y = 0.6f;
            shot.velocity = transform.TransformDirection(forwardUp * charge);
            Debug.Log(charge);
            charge = 0;
        }
    }
}