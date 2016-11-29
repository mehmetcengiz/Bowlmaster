using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionMaster : MonoBehaviour {

    public enum Action {Tidy,Reset,EndTurn,EndGame };

    private int[] bowls = new int[21];
    private int bowl = 1;

    public static Action NextAction(List<int> pinFalls) {
        ActionMaster actionMaster = new ActionMaster();
        Action currentAction = new Action();

        foreach (int pinFall in pinFalls) {
            currentAction = actionMaster.Bowl(pinFall);
        }

        return currentAction;
    }

    public Action Bowl(int pins){//TODO Make private

        if(pins<0 || pins > 10){ throw new UnityException("Invalid pins.");}

        bowls[bowl - 1] = pins;

        if (bowl == 21){
            return Action.EndGame;
        }

        //Hanld last-frame special cases.
        if(bowl >= 19 && pins==10) {
            bowl++;
            return Action.Reset;
        }else if(bowl == 20 ){
            bowl++;
            if(bowls[19-1] == 10 && bowls [20-1] != 10){ return Action.Tidy;}
            if (((bowls[19 - 1] + bowls[20 - 1]) % 10) == 0){return Action.Reset;}
            return Bowl21Awarded() ? Action.Tidy : Action.EndGame;
        }
        else if (bowl % 2 != 0){ // Frist bowl of frame
            if(pins == 10) {
                bowl += 2;
                return Action.EndTurn;
            }
            bowl += 1;
            return Action.Tidy;
        }
        else if(bowl %2 == 0) { //Second bowl of frame
            bowl += 1;
            return Action.EndTurn;
        }

        throw new UnityException("Not sure what action to return! ");
    }

    private bool Bowl21Awarded(){
        return (bowls[19 - 1] + bowls[20 - 1] >= 10);
    }

}
