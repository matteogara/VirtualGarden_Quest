using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Benchmark : MonoBehaviour
{
    public TreeGenerator generator;
    public GameObject world;
    public Text txt;
    public float targetFPS = 60;
    public float minTrees = 1000;
    public float batchSize = 20;

    float avg = 0;
    bool isFluid = true;
    float nTrees = 0;
    float fpsCounter;

    List<GameObject> batchBianco = new List<GameObject>();
    List<GameObject> batchAzzurro = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFluid) {
            avg += ((Time.deltaTime / Time.timeScale) - avg) * 0.03f;
            float fps = (1F / avg);

            if (fps < targetFPS && nTrees > minTrees) {
                isFluid = false;

                txt.text = "Max number of trees: " + nTrees.ToString();
            } else {
                fpsCounter++;

                if (fpsCounter > 4) {
                    var newPlant = generator.CreateTree(new Vector3(0, 0, 0));
                    //newPlant.transform.parent = world.transform;

                    //MeshRenderer[] meshes = newPlant.GetComponentsInChildren<MeshRenderer>();
                    //foreach (MeshRenderer mesh in meshes)
                    //{
                    //    if (mesh.sharedMaterial.name == "Bianco (Instance)")
                    //    {
                    //        mesh.gameObject.isStatic = true;
                    //        batchBianco.Add(mesh.gameObject);
                    //    }

                    //    if (mesh.sharedMaterial.name == "Azzurro (Instance)") {
                    //        mesh.gameObject.isStatic = true;
                    //        batchAzzurro.Add(mesh.gameObject);
                    //    }
                    //}

                    //if (batchBianco.Count >= batchSize) {
                    //    GameObject[] arr = batchBianco.ToArray();
                    //    StaticBatchingUtility.Combine(arr, world);
                    //    batchBianco = new List<GameObject>();
                    //    Debug.Log("STATIC BATCH BIANCO");
                    //}

                    //if (batchAzzurro.Count >= batchSize)
                    //{
                    //    GameObject[] arr = batchAzzurro.ToArray();
                    //    StaticBatchingUtility.Combine(arr, world);
                    //    batchAzzurro = new List<GameObject>();
                    //    Debug.Log("STATIC BATCH AZZURRO");
                    //}

                    nTrees++;
                    fpsCounter = 0;
                }

                txt.text = fps.ToString();
            }
        }
    }
}
