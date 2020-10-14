using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    public Text[] frameDisplay = new Text[10];
    public Text[] scoreDisplay = new Text[21];

    public void FillRollCard(List<int>rolls)
    {
        rolls[0] = 1;
    }
    public void FillFrames(List<int>frames)
    {

    }

    public static string FormatRolls(List<int> rolls)
    {
        return "blaj";
    }
}
