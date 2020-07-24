using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PickUpInstr : MonoBehaviour{

    [SerializeField]
    private PickUp pickUpValues;

    private Text pickUpText;
    private Text placeBackText;

    private bool inPickArea;
    private bool pickedUp;

    private Vector3 textPosition;

    void Start(){
        pickUpText = GameObject.Find("PickUpText").GetComponent<Text>();
        placeBackText = GameObject.Find("PlaceBackText").GetComponent<Text>();

        pickUpText.text = "";
        placeBackText.text = "";
    }

    void Update(){
        this.inPickArea = pickUpValues.inPickArea;
        this.pickedUp = pickUpValues.pickedUp;

        pickUpText.transform.position = textPosition;
        placeBackText.transform.position = textPosition;

        pickUpText.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        placeBackText.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);

        if (this.inPickArea == true){
            textPosition = pickUpValues.initialFlowerPosition + new Vector3(0, 1.5f, 0);
            pickUpText.text = "Press E \nto pick it up";
            placeBackText.text = "";
        } else if (this.inPickArea == false && pickedUp == false){
            pickUpText.text = "";
            placeBackText.text = "";
        }

        if (pickedUp == true){
            placeBackText.text = "Press Q \nto place it back";
            pickUpText.text = "";
        }
    }

}
