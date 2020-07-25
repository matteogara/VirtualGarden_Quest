using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrabbable : OVRGrabbable
{
    public SCENE_MANAGER sceneManager;

    Vector3 posBU;
    Quaternion rotBU;


    private void Start()
    {
        sceneManager = GameObject.Find("SCENE MANAGER").GetComponent<SCENE_MANAGER>();
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        if (!sceneManager.creativeMode)
        {
            posBU = transform.position;
            rotBU = transform.rotation;
            base.GrabBegin(hand, grabPoint);
        }
    }


    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        if (!sceneManager.creativeMode)
        {
            base.GrabEnd(linearVelocity, angularVelocity);
            transform.position = posBU;
            transform.rotation = rotBU;
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }
}
