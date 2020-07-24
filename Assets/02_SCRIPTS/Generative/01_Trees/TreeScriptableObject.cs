using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TreeData", menuName = "ScriptableObjects/TreeScriptableObject", order = 1)]
public class TreeScriptableObject : ScriptableObject
{
    [Header("Master scale")]
    public float masterScale = 1;

    [Header("Models")]
    public List<GameObject> trunk = new List<GameObject>();
    public List<float> trunkHeights = new List<float>();
    public GameObject brench;
    public GameObject foliage;

    [Header("Materials")]
    public Material trMat;
    public Material folMat;

    [Header("Trunk settings")]
    public float trMinR = .7f;
    public float trMaxR = 1.2f;
    public float trMinH = .7f;
    public float trMaxH = 1.2f;

    [Header("Brenches settings")]
    public int brMinNum = 0;
    public int brMaxNum = 2;
    public int brMinH = 2;
    public int brMaxH = 6;
    public float brMinScale = 0.8f;
    public float brMaxScale = 1.3f;
    public bool largerBrenchesBelow = true;

    [Header("Foliage settings")]
    public float folMinScale = .5f;
    public float folMaxScale = 1f;
    public float brFolPropScale = 1f;
    public Vector3 folOffset = new Vector3(0, 0, 0);

    [Header("Collider settings")]
    public float minCollScale = 1f;
    public float maxCollScale = 2.5f;
}
