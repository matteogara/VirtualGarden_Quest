using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellDebug : MonoBehaviour
{
    public GameObject[] leds;


    public void TurnOffLeds() {
        if (leds.Length == 5) {
            foreach (GameObject led in leds) {
                led.SetActive(false);
            }
        }
    }


    public void TurnOnLed(string _scentCode) {
        if (leds.Length == 5) {
            TurnOffLeds();
            leds[GetColIndex(_scentCode)].SetActive(true);
        }
    }


    private int GetColIndex(string _letter)
    {
        switch (_letter)
        {
            case "b":
                return 0;
            case "g":
                return 1;
            case "y":
                return 2;
            case "w":
                return 3;
            case "r":
                return 4;
            default:
                return 0;
        }
    }
}
