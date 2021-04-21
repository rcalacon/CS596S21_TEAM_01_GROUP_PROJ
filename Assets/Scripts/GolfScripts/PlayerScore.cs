using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int totalScore;

    private int strokes;

    public void IncrementStrokes()
    {
        strokes += 1;
    }

    public void HoleFinished()
    {
        totalScore += strokes;
        strokes = 0;
    }

    public int GetTotalScore()
    {
        return totalScore;
    }

    public int GetStrokes()
    {
        return strokes;
    }
}
