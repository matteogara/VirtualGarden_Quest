using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastLine : MonoBehaviour
{
    public Material[] mats;

    LineRenderer line;


    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
    }


    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.forward * 12 + transform.position);
    }


    public void ChangeLineCol(int _index) {
        line.material = mats[_index];
    }
}
