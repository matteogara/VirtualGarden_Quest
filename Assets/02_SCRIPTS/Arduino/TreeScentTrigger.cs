using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreeScentTrigger : MonoBehaviour {

    [SerializeField]
    private PickUp pickUpValues;

    [SerializeField]
    private SCENE_MANAGER modality;

    public ArduinoEvent send;
    public ArduinoEvent InOnExit;

    [HideInInspector] public string treeScent;
    [HideInInspector] public string noTreeScent;

    [Header("Smell Debug")]
    public SmellDebug smellDebug;

    private bool toPlaceBack;
    private bool pickedUp;
    private bool enteredWhilePickedUp = false;
    private bool creativeMode;

    private GameObject treeObj;

    void Update(){
        this.creativeMode = modality.creativeMode;
        this.pickedUp = pickUpValues.pickedUp;
        
        this.toPlaceBack = pickUpValues.toPlaceBack;
        //Debug.Log("toplaceback tree: " + this.toPlaceBack);

        if(this.enteredWhilePickedUp == true && this.toPlaceBack == false){
            TriggerScent();
        }
    }

    private void OnTriggerEnter(Collider other){

        if (this.creativeMode == false && (other.gameObject.transform.parent.tag == "Spawn_Tree" || other.gameObject.transform.parent.tag == "Spawn_Bush")){

            treeObj = other.gameObject;

            if (this.pickedUp == true && this.toPlaceBack ==true){
                enteredWhilePickedUp = true;
            }

            if (this.toPlaceBack == false){
                TriggerScent();
            }
        }
    }

    private void OnTriggerExit(Collider other){
        enteredWhilePickedUp = false;

        if (other.gameObject.transform.parent.tag == "Spawn_Tree" || other.gameObject.transform.parent.tag == "Spawn_Bush"){
            noTreeScent = treeScent.ToUpper();
            InOnExit.Invoke(noTreeScent);
            Debug.Log("ALBERO/BUSH: fuori area e profumo disttivato");

            // Turn off all smell debug leds
            if (smellDebug != null) smellDebug.TurnOffLeds();
        }
    }

    private void TriggerScent(){
        treeScent = treeObj.GetComponentInParent<ColorGrabber>().arduinoColor;
        Debug.Log("ALBERO/BUSH: colore rilevato = " + treeScent);

        send.Invoke(treeScent);
        Debug.Log("ALBERO/BUSH: in area e profumo attivato");

        // Turn on smell debug led
        if (smellDebug != null) smellDebug.TurnOnLed(treeScent);
    }
}

