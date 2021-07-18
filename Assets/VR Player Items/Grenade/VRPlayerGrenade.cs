using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerGrenade : MonoBehaviour
{
    bool Activated = false;

    [SerializeField] GameObject GrenadeExplosionObject;

    [SerializeField] GameObject Light1;
    [SerializeField] GameObject Light2;
    [SerializeField] GameObject Light3;
    [SerializeField] GameObject Light4;
    [SerializeField] GameObject Light5;

    private void Start()
    {
        Light1.SetActive(false);
        Light2.SetActive(false);
        Light3.SetActive(false);
        Light4.SetActive(false);
        Light5.SetActive(false);
    }

    public void Activate()
    {
        if (!Activated)
        {
            Activated = true;
            StartCoroutine(DetonationSequence());
        }
    }

    public void Detonate()
    {
        gameObject.GetComponent<Grabbable>().CanBeGrabbed = false;
        if (gameObject.GetComponent<Grabbable>().Grabbed)
        {
            gameObject.GetComponent<Grabbable>().GrabbedHand.ReleaseGrabbedObject();
        }

        Instantiate(GrenadeExplosionObject, gameObject.transform.position, gameObject.transform.rotation);

        Destroy(gameObject);
    }

    IEnumerator DetonationSequence()
    {
        Light1.SetActive(true);
        yield return new WaitForSeconds(1f);
        Light2.SetActive(true);
        yield return new WaitForSeconds(1f);
        Light3.SetActive(true);
        yield return new WaitForSeconds(1f);
        Light4.SetActive(true);
        yield return new WaitForSeconds(1f);
        Light5.SetActive(true);
        yield return new WaitForSeconds(1f);

        Detonate();
    }
}
