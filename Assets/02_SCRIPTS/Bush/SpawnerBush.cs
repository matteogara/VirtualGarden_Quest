using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBush : MonoBehaviour
{
    public float maxDist;
    public BushGenerator generator;

    Vector3 mousePos;
    Plane plane = new Plane(Vector3.up, 0f);
    Transform cam;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = new Vector3(0, 10, 0);

        // If player stands still
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // If player can move around
        Ray ray = new Ray(cam.position, cam.forward);

        // If using oculus quest
        // Raycast dall'asse forward del controller

        float distanceToPlane;

        if (plane.Raycast(ray, out distanceToPlane)) {
            if (distanceToPlane < maxDist)
            mousePos = ray.GetPoint(distanceToPlane);

            if (Input.GetKey(KeyCode.Mouse0)) {
                //generator.CreateBush(mousePos);
            }
        }

        transform.position = mousePos;
    }
}
