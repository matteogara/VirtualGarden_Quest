using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HerbData", menuName = "ScriptableObjects/HerbScriptableObject", order = 1)]
public class HerbScriptableObject : ScriptableObject
{
    [Header("Master scale")]
    public float masterScale = 1;

    [Header("Models")]
    public GameObject tussock;

    [Header("Materials")]
    public Material tussockMat;

    [Header("Tussocks settings")]
    public int tussMinNum = 3;
    public int tussMaxNum = 10;
    public int tussMaxDist = 10;
    public float minScale = 0.4f;
    public float maxScale = 0.6f;

    [Header("Collider settings")]
    public float minCollScale = 0.8f;
    public float maxCollScale = 1.2f;
}
