using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorFeeder : MonoBehaviour
{
    public Generator generator;

    public enum TypeOfVegetation {Tree, Bush, Flower, Mushroom, Herb}
    [Header("Choose type of vegetation to spawn")]
    public TypeOfVegetation typeOfVegetation;

    [Header("Put here scriptable objects (only the one corresponding to the choosen type will be used")]
    public TreeScriptableObject treeData;
    public BushScriptableObject bushData;
    public FlowerScriptableObject flowerData;
    public MushroomScriptableObject mushroomData;
    public HerbScriptableObject herbData;

    private GameObject generated;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (generated != null) {
                Destroy(generated);
            }

            if (typeOfVegetation == TypeOfVegetation.Tree) generated = generator.CreateTree(transform.position, treeData);
            if (typeOfVegetation == TypeOfVegetation.Bush) generated = generator.CreateBush(transform.position, bushData);
            if (typeOfVegetation == TypeOfVegetation.Flower) generated = generator.CreateFlower(transform.position, flowerData);
            if (typeOfVegetation == TypeOfVegetation.Mushroom) generated = generator.CreateMushroom(transform.position, mushroomData);
            if (typeOfVegetation == TypeOfVegetation.Herb) generated = generator.CreateHerb(transform.position, herbData);
        }
    }
}
