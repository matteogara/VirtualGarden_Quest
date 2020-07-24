using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BushData", menuName = "ScriptableObjects/BushScriptableObject", order = 1)]
public class BushScriptableObject : ScriptableObject
{
    [Header("Master scale")]
    public float masterScale = 1;

    [Header("Models")]
    public GameObject shrubs;

    [Header("Materials")]
    public Material shrubsMat;

    [Header("Shrubs settings")]
    public int shrubsMinNum = 1;
    public int shribsMaxNum = 5;
    public int shrMaxDist = 3;
    public float minScale = 0.4f;
    public float maxScale = 0.6f;
    public bool largerShrubsAtCenter = true;

    [Header("Collider settings")]
    public float minCollScale = 1f;
    public float maxCollScale = 2.5f;
}
