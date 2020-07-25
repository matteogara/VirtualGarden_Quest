using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBatchingTest : MonoBehaviour
{
    public GameObject cube;
    public GameObject world;

    List<GameObject> batch = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var newCube = Instantiate(cube, Vector3.zero, Quaternion.identity);
        newCube.transform.parent = world.transform;

        MeshRenderer[] meshes = newCube.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in meshes) {
            mesh.gameObject.isStatic = true;
            batch.Add(mesh.gameObject);
        }
        if (newCube.GetComponent<MeshRenderer>()) {
            newCube.isStatic = true;
            batch.Add(newCube);
        }

        if (batch.Count < 100) {
            Debug.Log(batch.Count);
        } else {
            GameObject[] batchArr = batch.ToArray();
            StaticBatchingUtility.Combine(batchArr, world);
            batch = new List<GameObject>();
            Debug.Log("STATIC BATCHING");
        }
    }
}
