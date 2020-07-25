using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualNose : MonoBehaviour
{
    public int numberOfScents = 4;
    public float radius = 4f;

    public static float[] scentIntesities;

    private SphereCollider myCollider;
    private List<Collider> closeSmellables = new List<Collider>();


    // Start is called before the first frame update
    void Start()
    {
        scentIntesities = new float[numberOfScents];
        for (int i = 0; i < numberOfScents; i++) {
            scentIntesities[i] = 0f;
        }

        myCollider = gameObject.AddComponent<SphereCollider>();
        myCollider.center = Vector3.zero;
        myCollider.radius = radius;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < numberOfScents; i++)
        {
            scentIntesities[i] = 0f;
        }

        foreach (Collider smellable in closeSmellables) {
            Flower _flower = smellable.gameObject.GetComponent<Flower>();
            scentIntesities[_flower.mat] += _flower.intensity;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Flower" && !closeSmellables.Contains(other)) {
            closeSmellables.Add(other);
            other.gameObject.GetComponent<Flower>().enabled = true;
        }

        Debug.Log("TriggerEnter!");
    }


    private void OnTriggerExit(Collider other)
    {
        if (closeSmellables.Contains(other)) {
            closeSmellables.Remove(other);
            other.gameObject.GetComponent<Flower>().DisableScript();
        }
    }
}
