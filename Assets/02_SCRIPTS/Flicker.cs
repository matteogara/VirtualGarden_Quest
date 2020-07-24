using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flicker : MonoBehaviour
{
    RawImage rend;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<RawImage>();

        InvokeRepeating("ToggleVisibility", 0, 0.1f);
    }

    void ToggleVisibility() {
        rend.enabled = !rend.enabled;
    }
}
