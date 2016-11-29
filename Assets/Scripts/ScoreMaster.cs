﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreMaster {

    //Return a list of cumulative scores, like a normal score card.
    public static List<int> ScoreCumulative(List<int> rolls) {
        List<int> cumulativeScores = new List<int>();
        int runningTotal = 0;


        foreach (int scoreFrame in ScoreFrames(rolls)) {
            runningTotal += scoreFrame;
            cumulativeScores.Add(runningTotal);
        }


        return cumulativeScores;
    }

    //Return a list of individual frame scores, NOT cumulative.
    public static List<int> ScoreFrames(List<int> rolls) {
        List<int> frameList = new List<int>();


        return frameList;
    }

}
