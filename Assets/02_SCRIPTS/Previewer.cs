using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Previewer : MonoBehaviour
{
    public SCENE_MANAGER sceneManager;
    public Generator generator;

    [Header("Preview settings")]
    public Vector3 offset;
    public float[] previewScales;

    [Header("Array di scriptable objects, uno per ogni tipo di pianta")]
    public TreeScriptableObject[] treeData;
    public BushScriptableObject[] bushData;
    public FlowerScriptableObject[] flowerData;
    public MushroomScriptableObject[] mushroomData;
    public HerbScriptableObject[] herbData;

    private GameObject generated;


    public void UpdatePreview()
    { //

        if (generated != null)
        {
            Destroy(generated);
        }


        // If selected, spawn a tree
        if (sceneManager.selection[1] == 0)
        {
            generated = generator.CreateTree(transform.position, treeData[sceneManager.selection[0]]);
            generated.name = "Mini tree";
            generated.transform.parent = transform;
            generated.transform.localRotation = Quaternion.Euler(-90, 0, 0);
            generated.transform.localScale = Vector3.one * previewScales[0];

            if (sceneManager.selection[0] == 2) {
                generated.transform.localScale *= 0.6f;
            }
        }

        // If selected, spawn a bush
        if (sceneManager.selection[1] == 1)
        {
            generated = generator.CreateBush(transform.position, bushData[sceneManager.selection[0]]);
            generated.name = "Mini bush";
            generated.transform.parent = transform;
            generated.transform.localRotation = Quaternion.Euler(-90, 0, 0);
            generated.transform.localScale = Vector3.one * previewScales[1];
        }

        // If selected, spawn a flower, mushroom or herb
        if (sceneManager.selection[1] == 2)
        {
            GameObject generatedHerb = generator.CreateHerb(offset, herbData[sceneManager.selection[0]]);
            GameObject generatedFlower = generator.CreateFlower(Quaternion.AngleAxis(120, Vector3.up) * offset, flowerData[sceneManager.selection[0]]);
            GameObject generatedMushroom = generator.CreateMushroom(Quaternion.AngleAxis(240, Vector3.up) * offset, mushroomData[sceneManager.selection[0]]);

            generated = new GameObject("Mini grass");
            generatedFlower.transform.parent = generated.transform;
            generatedMushroom.transform.parent = generated.transform;
            generatedHerb.transform.parent = generated.transform;

            generated.transform.parent = transform;
            generated.transform.localPosition = new Vector3(0, -0.07f, 0);
            //generated.transform.localRotation = Quaternion.identity;
            generated.transform.localRotation = Quaternion.Euler(-90, 0, 0);
            generated.transform.localScale = Vector3.one * previewScales[2];
        }

        ChangeMaterials(generated, sceneManager.selection[0]);
    }


    void ChangeMaterials(GameObject _obj, int _index) {

        MeshRenderer[] renderers = _obj.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer rend in renderers) {
            string matName = rend.sharedMaterial.name.Split(' ')[1];

            switch (matName) {
                case "DOWN": 
                    rend.sharedMaterial = mushroomData[_index].bodyMat;
                    break;
                default:
                    rend.sharedMaterial = mushroomData[_index].headMat;
                    break;
            }
        }
    }
}