using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    public MovePointer movePointer;
    public SCENE_MANAGER sceneManager;
    public Spawner spawner;


    private void OnTriggerEnter(Collider other)
    {

        if (movePointer.validPointer) {

            //if (Input.GetKey(KeyCode.Mouse0)) {
            if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger)) {
                int tagNum = int.Parse(other.tag.Split('_')[1]);
                if (tagNum != sceneManager.selection[0])
                {
                    Destroy(other.transform.parent.gameObject);
                    spawner.ResetLists();
                }
            }

            //if (Input.GetKey(KeyCode.Mouse1)) {
            if (OVRInput.Get(OVRInput.RawButton.RHandTrigger)) {
                Destroy(other.transform.parent.gameObject);
                spawner.ResetLists();
            }
        }
    }
}
