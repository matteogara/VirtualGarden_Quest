using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree3Generator : MonoBehaviour
{
    [Header("Models")]
    public GameObject trunk;
    public GameObject foliage_down;

    [Header("Materials")]
    public Material trMat;
    public Material folMat;


    [Header("Foliage settings")]
    public Vector3 folOffset = new Vector3(-1.5f, 0, 1.45f);

    private float count;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            CreateTree(transform.position);
        }
    }


    public GameObject CreateTree(Vector3 _pos) {
        // Create object
        GameObject _tree = new GameObject("Tree_" + count);
        count++;

        // Create trunk
        var _trunk = Instantiate(trunk, Vector3.zero, Quaternion.Euler(-90, Random.Range(0, 360), 0));
        _trunk.GetComponent<MeshRenderer>().sharedMaterial = trMat;
        _trunk.transform.parent = _tree.transform;

        // Create trunk foliage
        var _trFolDown = Instantiate(foliage_down, Vector3.zero, Quaternion.Euler(-90, 0, 0));
        _trFolDown.GetComponent<MeshRenderer>().sharedMaterial = folMat;
        _trFolDown.transform.parent = _trunk.transform;
        _trFolDown.transform.localPosition = new Vector3(0, 0, 8f);

        // Set position
        _tree.transform.position = _pos;

        return _tree;
    }
}
