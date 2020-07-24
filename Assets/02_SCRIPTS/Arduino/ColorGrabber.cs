using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorGrabber : MonoBehaviour {
    public string arduinoColor;
    private float greenComponent;

    void Start(){

        float blueGreenComponent = 0.5647059f;
        float greenGreenComponent = 0.7764706f;
        float yellowGreenComponent = 0.6821074f;
        float yellowGreenComponentBush = 0.682353f;
        float orangeGreenComponent = 0.439802f; 
        float orangeGreenComponentBush = 0.4392157f;
        float redGreenComponent = 0.39102f; 
        float redGreenComponentBush = 0.3921569f;

        List<MeshRenderer> rendererList = new List<MeshRenderer>();
        GetComponentsInChildren<MeshRenderer>(false, rendererList);

        if (rendererList != null){
                        
            foreach (MeshRenderer rendererPart in rendererList){
                
                greenComponent = rendererPart.sharedMaterial.GetColor("_Color").g;
                //Debug.Log("elenco lista: " + rendererPart.ToString() + " colore: " + greenComponent);

                if (Mathf.Approximately(greenComponent, blueGreenComponent)){
                    arduinoColor = "b";
                    break;
                 } else if (Mathf.Approximately(greenComponent, greenGreenComponent)){
                    arduinoColor = "g";
                    break;
                } else if (Mathf.Approximately(greenComponent, yellowGreenComponent) || Mathf.Approximately(greenComponent, yellowGreenComponentBush)){
                    arduinoColor = "y";
                    break;
                } else if (Mathf.Approximately(greenComponent, orangeGreenComponent) || Mathf.Approximately(greenComponent, orangeGreenComponentBush))
                {
                    arduinoColor = "w";
                    break;
                } else if (Mathf.Approximately(greenComponent, redGreenComponent) || Mathf.Approximately(greenComponent, redGreenComponentBush))
                {
                    arduinoColor = "r";
                    break;
                }
            }

        }
    }
}

