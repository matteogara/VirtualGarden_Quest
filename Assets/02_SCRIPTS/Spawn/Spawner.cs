using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public SCENE_MANAGER sceneManager;
    public Generator generator;
    public MovePointer movePointer;

    [Header("Array di scriptable objects, uno per ogni tipo di pianta")]
    public TreeScriptableObject[] treeData;
    public BushScriptableObject[] bushData;
    public FlowerScriptableObject[] flowerData;
    public MushroomScriptableObject[] mushroomData;
    public HerbScriptableObject[] herbData;

    List<GameObject> nearTrees = new List<GameObject>();
    List<GameObject> nearBushes = new List<GameObject>();
    List<GameObject> nearGrass = new List<GameObject>();


    private void Start()
    {
        nearTrees = new List<GameObject>();
        nearBushes = new List<GameObject>();
        nearGrass = new List<GameObject>();
    }


    // Update is called once per frame
    void Update()
    {

        // If player is spawning
        //if (movePointer.validPointer && Input.GetKey(KeyCode.Mouse0)) {
        if (movePointer.validPointer && OVRInput.Get(OVRInput.RawButton.RIndexTrigger)) {

            // If selected, spawn a tree
            if (sceneManager.selection[1] == 0 && nearTrees.Count < 1)
            {
                generator.CreateTree(transform.position, treeData[sceneManager.selection[0]]);
            }

            // If selected, spawn a bush
            if (sceneManager.selection[1] == 1 && nearBushes.Count < 1)
            {
                generator.CreateBush(transform.position, bushData[sceneManager.selection[0]]);
            }

            // If selected, spawn a flower, mushroom or herb
            if (sceneManager.selection[1] == 2 && nearGrass.Count < 1)
            {
                int index = Random.Range(0, 5);

                switch(index)
                {
                    case 0:
                        generator.CreateFlower(transform.position, flowerData[sceneManager.selection[0]]);
                        break;
                    case 1:
                        generator.CreateMushroom(transform.position, mushroomData[sceneManager.selection[0]]);
                        break;
                    default:
                        generator.CreateHerb(transform.position, herbData[sceneManager.selection[0]]);
                        break;
                }
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        GameObject collided = other.transform.parent.gameObject;

        switch (collided.tag)
        {
            case "Spawn_Tree":
                nearTrees.Add(collided);
                break;
            case "Spawn_Bush":
                nearBushes.Add(collided);
                break;
            case "Spawn_Grass":
                nearGrass.Add(collided);
                break;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        GameObject collided = other.transform.parent.gameObject;

        switch (collided.tag)
        {
            case "Spawn_Tree":
                nearTrees.Remove(collided);
                break;
            case "Spawn_Bush":
                nearBushes.Remove(collided);
                break;
            case "Spawn_Grass":
                nearGrass.Remove(collided);
                break;
        }
    }


    public void ResetLists()
    {
        nearTrees = new List<GameObject>();
        nearBushes = new List<GameObject>();
        nearGrass = new List<GameObject>();
    }
}
