using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointer : MonoBehaviour
{
    public Transform raycaster;
    public float maxDist;
    public DrawColoredAreas painter;
    public LayerMask layerMask;

    Vector3 mousePos;
    //Transform cam;
    RaycastHit _hit;

    [HideInInspector]
    public bool validPointer;


    // Start is called before the first frame update
    void Start()
    {
        //cam = Camera.main.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mousePos = new Vector3(0, 10, 0);


        // If player stands still
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // If player can move around
        Ray ray = new Ray(raycaster.position, raycaster.forward);

        // If using oculus quest
        // Raycast dall'asse forward del controller



        // If spawn is possible
        if (Physics.Raycast(ray, out _hit, maxDist, layerMask))
        {
            validPointer = true;
            mousePos = _hit.point;

            //if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
            if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) || OVRInput.Get(OVRInput.RawButton.RHandTrigger))
            {
                painter.Draw(_hit.textureCoord);
            }
        } else {
            validPointer = false;
        }

        transform.position = mousePos + new Vector3(0, 0.001f, 0);
    }
}
