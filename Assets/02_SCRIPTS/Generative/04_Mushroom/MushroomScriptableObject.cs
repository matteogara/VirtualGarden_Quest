using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MushroomData", menuName = "ScriptableObjects/MushroomScriptableObject", order = 1)]
public class MushroomScriptableObject : ScriptableObject
{
    [Header("Master scale")]
    public float masterScale = 1;

    [Header("Models")]
    public List<GameObject> body = new List<GameObject>();
    public List<float> bodyHeights = new List<float>();
    public List<GameObject> head = new List<GameObject>();

    [Header("Materials")]
    public Material bodyMat;
    public Material headMat;

    [Header("body settings")]
    public float bodyMinScale = 0.5f;
    public float bodyMaxScale = 1f;

    [Header("head settings")]
    public float headMinScale = 1f;
    public float headMaxScale = 1.5f;

    [Header("Collider settings")]
    public float minCollScale = 1f;
    public float maxCollScale = 2.5f;
}
