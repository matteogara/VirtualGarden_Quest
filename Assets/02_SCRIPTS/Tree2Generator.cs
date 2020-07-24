using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree2Generator : MonoBehaviour
{
    [Header("Models")]
    public GameObject trunk;
    public GameObject brench;
    public GameObject foliage_down;

    [Header("Materials")]
    public Material trMat;
    public Material folMat;

    [Header("Trunk settings")]
    // public float trMinScale = .7f;
    // public float trMaxScale = 1.2f;

    [Header("Brenches settings")]
    public int brMinH = 2;
    public int brMaxH = 6;
    // public float brMinScale = .8f;
    // public float brMaxScale = .3f;
    public bool largerBrenchesBelow = true;

    [Header("Foliage settings")]
    // public float folMinScale = .5f;
    // public float folMaxScale = 1f;
    public Vector3 folOffset = new Vector3(-1.5f, 0, 1.45f);

    private float count;
    private int brDeltaH;


    // Start is called before the first frame update
    void Start()
    {
        brDeltaH = Mathf.Abs(brMaxH - brMinH);
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
        // float _trR = Random.Range(trMinScale, trMaxScale);
        // float _trH = Random.Range(trMinScale, trMaxScale);
        // _trunk.transform.localScale = new Vector3(_trR, _trR, _trH);

        // Create trunk foliage
        var _trFolDown = Instantiate(foliage_down, Vector3.zero, Quaternion.Euler(-90, 0, 0));
        _trFolDown.GetComponent<MeshRenderer>().sharedMaterial = folMat;
        // float _folR = Random.Range(folMinScale, folMaxScale);
        // float _folH = Random.Range(folMinScale, folMaxScale) / _trH;
        // _trFolDown.transform.localScale = new Vector3(_folR, _folR, _folH);
        _trFolDown.transform.parent = _trunk.transform;
        _trFolDown.transform.localPosition = new Vector3(0, 0, 8f);


        // Create brenches
        float _brenchNum = Random.Range(1, 3);
        for (int i = 0; i < _brenchNum; i++) {
            var _brench = Instantiate(brench, Vector3.zero, Quaternion.Euler(-90, Random.Range(0, 360), 90));
            _brench.GetComponent<MeshRenderer>().sharedMaterial = trMat;
            float _z = Random.Range(brMinH, brMaxH);
            float s;
            // if (largerBrenchesBelow)
            // {
            //     s = (Random.Range(brMinScale, brMaxScale) + (brMaxH - _z) / brDeltaH) / 2;
            // }
            // else
            // {
            //     s = Random.Range(brMinScale, brMaxScale);
            // }
            // _brench.transform.localScale = new Vector3(s, s, s);
            _brench.transform.parent = _trunk.transform;
            _brench.transform.localPosition = new Vector3(0, 0, _z);

            // Create brench foliage
            _trFolDown = Instantiate(foliage_down, Vector3.zero, Quaternion.Euler(-90, 0, 0));
            _trFolDown.GetComponent<MeshRenderer>().sharedMaterial = folMat;
            // _folR = Random.Range(folMinScale, folMaxScale);
            // _folH = Random.Range(folMinScale, folMaxScale) / _trH;
            // _trFolDown.transform.localScale = new Vector3(_folR, _folR, _folH);
            _trFolDown.transform.parent = _brench.transform;
            _trFolDown.transform.localPosition = folOffset;
        }

        // Set position
        _tree.transform.position = _pos;

        return _tree;
    }
}
