using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableRot : MonoBehaviour
{
    public float vel;

    float rot;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, vel * Time.deltaTime, 0));
    }
}
