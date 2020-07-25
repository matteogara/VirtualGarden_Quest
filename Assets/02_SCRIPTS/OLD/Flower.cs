using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    private Transform fieldTr;
    private Transform playerTr;

    float factor = 9.9f;

    [HideInInspector]
    public int mat = 0;
    [HideInInspector]
    public float intensity;

    private LineRenderer line;


    // Start is called before the first frame update
    void Start()
    {
        playerTr = GameObject.Find("Player").transform;
        fieldTr = transform.GetChild(0);

        line = this.gameObject.AddComponent<LineRenderer>();
        line.startWidth = 0.04f;
        line.positionCount = 2;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);
        line.sharedMaterial = gameObject.GetComponent<Renderer>().sharedMaterial;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float newScale = Vector3.Distance(transform.position, playerTr.position) * factor;
        fieldTr.localScale = new Vector3(newScale, 0.1f, newScale);

        intensity = Mathf.Clamp(1 - (newScale / 20.0f), 0, 1.0f);
        fieldTr.gameObject.GetComponent<Renderer>().sharedMaterial.color = new Color(1.0f, 1.0f, 1.0f, intensity);

        line.SetPosition(1, playerTr.position);
    }


    public void DisableScript()
    {
        fieldTr.localScale = Vector3.zero;
        line.SetPosition(1, transform.position);
        this.enabled = false;
    }
}
