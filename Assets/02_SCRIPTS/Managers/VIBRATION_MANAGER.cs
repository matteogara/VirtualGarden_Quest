using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIBRATION_MANAGER : MonoBehaviour
{
    public static VIBRATION_MANAGER singleton;


    // Start is called before the first frame update
    void Start()
    {
        if (singleton && singleton != this)
            Destroy(this);
        else
            singleton = this;
    }


    public void TriggerVibration(int millis, OVRInput.Controller controller) 
    {
        StopAllCoroutines();
        OVRInput.SetControllerVibration(0.8f, 1, controller);
        StartCoroutine(StopVibration(millis / 1000, controller));
    }


    IEnumerator StopVibration(float _time, OVRInput.Controller _contr) {
        yield return new WaitForSeconds(_time);
        OVRInput.SetControllerVibration(0, 0, _contr);
    }
}
