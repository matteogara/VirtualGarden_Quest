using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongestScent : MonoBehaviour
{
    public Transform[] graphs;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int highestBar = 0;
        float maxScale = 0;

        for (int i = 0; i < graphs.Length; i++) {
            if (graphs[i].localScale.z > maxScale) {
                maxScale = graphs[i].localScale.z;
                highestBar = i;
            }
        }

        transform.localPosition = new Vector3(6f + highestBar * 0.5f, 0, -4.8f);
    }
}
