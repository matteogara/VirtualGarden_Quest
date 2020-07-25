using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateFlowers : MonoBehaviour
{
    public int numOfInstances;

    public GameObject flowerPrefab;
    public GameObject terrain;
    public Material[] materials;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 terrainCenter = terrain.GetComponent<Renderer>().bounds.center;
        Vector3 terrainExtents = terrain.GetComponent<Renderer>().bounds.extents;

        for (int i = 0; i < numOfInstances; i++) {
            Vector3 newPos = new Vector3(Random.Range(-terrainExtents.x, terrainExtents.x), 0, Random.Range(-terrainExtents.z, terrainExtents.z));
            newPos += terrainCenter;

            int newMat = Random.Range(0, materials.Length);

            GameObject newFlower = Instantiate(flowerPrefab, newPos, Quaternion.identity);
            newFlower.name = flowerPrefab.name + "_Mat" + newMat;
            newFlower.GetComponent<Renderer>().sharedMaterial = materials[newMat];
            newFlower.GetComponent<Flower>().mat = newMat;
            newFlower.GetComponent<Flower>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
