using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour {

    public Text standingDisplay;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    standingDisplay.text = CountStanding().ToString();
	}

    public int CountStanding() {
        int countOfStandingPins =0;
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            if (pin.IsStanding()) {
                countOfStandingPins++;
            }
        }
        return countOfStandingPins;
    }
}
