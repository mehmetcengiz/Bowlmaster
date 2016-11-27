﻿using UnityEngine;
using System.Collections;

public class ActionMaster : MonoBehaviour {

    public enum Action {Tidy,Reset,EndTurn,EndGame };

    private int[] bowls = new int[21];
    private int bowl = 1;

    public Action Bowl(int pins){

        if(pins<0 || pins > 10){ throw new UnityException("Invalid pins.");}

        bowls[bowl - 1] = pins;

        if (bowl == 21){
            return Action.EndGame;
        }

        //Hanld last-frame special cases.
        if(bowl >= 19 && Bowl21Awarded()) {
            bowl++;
            return Action.Reset;
        }else if(bowl == 20 && !Bowl21Awarded()){
            return Action.EndGame;
        }

        if (pins == 10){
            bowl += 2;
            return Action.EndTurn;
        }

        if (bowl % 2 != 0){ // Mid frame (or last frame)
            bowl += 1;
            return Action.Tidy;
        }else if(bowl %2 == 0) { //End of frame
            bowl += 1;
            return Action.EndTurn;
        }

        throw new UnityException("Not sure what action to return! ");
    }

    private bool Bowl21Awarded(){
        return (bowls[19 - 1] + bowls[20 - 1] >= 10);
    }

}
