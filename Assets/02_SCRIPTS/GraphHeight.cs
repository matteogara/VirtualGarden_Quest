using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphHeight : MonoBehaviour
{
    public int scentNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1.0f, 1.0f, VirtualNose.scentIntesities[scentNum] * 1.0f);
    }
}
