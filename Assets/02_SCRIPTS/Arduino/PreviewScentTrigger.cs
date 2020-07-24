using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PreviewScentTrigger : MonoBehaviour {

    [SerializeField]
    private SCENE_MANAGER modality;

    public ArduinoEvent send;
    public ArduinoEvent InOnExit;

    //[HideInInspector] public string previewScent;
    [HideInInspector] public string noPreviewScent;
    [HideInInspector] public string currentPreviewScent;

    [Header("Smell Debug")]
    public SmellDebug smellDebug;

    private bool creativeMode;
    private string previousPreviewScent = "b";
    

    private GameObject previewObj;

    void Update(){
        this.creativeMode = modality.creativeMode;
        
    }

    private void OnTriggerEnter(Collider other){

        /*if (this.creativeMode == true && (other.gameObject.transform.parent.tag == "Spawn_Tree" || other.gameObject.transform.parent.tag == "Spawn_Bush" || other.gameObject.transform.parent.tag == "Spawn_Grass")) {

            previewObj = other.gameObject;
            currentPreviewScent = previewObj.GetComponentInParent<ColorGrabber>().arduinoColor;
            Debug.Log("PREVIEW: colore corrente rilevato = " + currentPreviewScent);

            if(currentPreviewScent != previousPreviewScent){
                TriggerScent();
            }
           
        }*/

        if (this.creativeMode == true && other.gameObject.transform.parent.name.StartsWith("Mini")){

            previewObj = other.gameObject;
            currentPreviewScent = previewObj.GetComponentInParent<ColorGrabber>().arduinoColor;
            Debug.Log("PREVIEW: colore corrente rilevato = " + currentPreviewScent);

            if (currentPreviewScent != previousPreviewScent)
            {
                TriggerScent();
            }

        }

    }

    private void OnTriggerExit(Collider other){

        /* if (other.gameObject.transform.parent.tag == "Spawn_Tree" || other.gameObject.transform.parent.tag == "Spawn_Bush" || other.gameObject.transform.parent.tag == "Spawn_Grass"){

             ScentsOff();

         }*/

        if (other.gameObject.transform.parent.name.StartsWith("Mini")){

            ScentsOff();

        }
    }

    private void TriggerScent(){
        noPreviewScent = previousPreviewScent.ToUpper();
        InOnExit.Invoke(noPreviewScent);

        send.Invoke(currentPreviewScent);
        Debug.Log("PREVIEW: in area e profumo attivato");

        previousPreviewScent = currentPreviewScent;

        // Turn on smell debug led
        smellDebug.TurnOnLed(currentPreviewScent);
    }

    private void ScentsOff(){
        noPreviewScent = currentPreviewScent.ToUpper();
        InOnExit.Invoke(noPreviewScent);
        Debug.Log("PREVIEW: profumo disattivato");

        // Turn off all smell debug leds
        smellDebug.TurnOffLeds();
    }
}

