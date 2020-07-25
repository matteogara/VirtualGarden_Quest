using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGenerator : MonoBehaviour
{
    [Header("Master scale")]
    public float masterScale = 1;

    [Header("Models")]
    public List<GameObject> stems = new List<GameObject>();
    public List<float> stemsHeights = new List<float>();
    public List<float> stemsWidth = new List<float>();
    public List<GameObject> corollas = new List<GameObject>();

    [Header("Materials")]
    public Material stemMat;
    public Material corollaMat;

    private float count;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            DeleteFlower();
            CreateFlower(transform.position);
        }

    }


    public void CreateFlower(Vector3 _pos) {
        // Create object
        GameObject _flower = new GameObject("flower_" + count);
        count++;

        // Create stem
        int stemIndex = Random.Range(0, stems.Count);
        var _stem = Instantiate(stems[stemIndex], Vector3.zero, Quaternion.Euler(-90, 0, 0));
        _stem.GetComponent<MeshRenderer>().sharedMaterial = stemMat;
        _stem.transform.parent = _flower.transform;

        // Create corolla
        int corollaIndex = Random.Range(0, corollas.Count);
        var _corolla = Instantiate(corollas[corollaIndex], Vector3.zero, Quaternion.Euler(-90, 0, 0));
        _corolla.GetComponent<MeshRenderer>().sharedMaterial = corollaMat;

        _corolla.transform.parent = _stem.transform;
        _corolla.transform.localPosition = new Vector3(stemsWidth[stemIndex], 0, stemsHeights[stemIndex]);

        // Set position
        _flower.transform.position = _pos;

        // Set master scale
        _flower.transform.localScale *= masterScale;
    }


    void DeleteFlower() {
        GameObject _flower = GameObject.Find("flower_" + (count - 1));
        if (_flower != null) {
            Destroy(_flower);
        }
    }
}
