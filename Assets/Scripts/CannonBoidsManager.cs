using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonBoidsManager : MonoBehaviour
{
    private string scorePrefix = "SCORE: ";
    private string timeSuffix = "s LEFT";
    private int numSeconds = 30;
    private Text gameScore;
    private Text timer;
    private float time;

    public void updateScore()
    {
        char space = ' ';
        int score = int.Parse(gameScore.text.Split(space)[1]);
        score++;
        gameScore.text = "SCORE: " + score.ToString();
    }

    public void updateTimer()
    {
        time += Time.deltaTime;
        int secondsElapsed = Mathf.FloorToInt(time % 60f);
        int timeLeft = numSeconds - secondsElapsed;
        if(timeLeft > 0)
        {
            timer.text = timeLeft.ToString() + timeSuffix;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameScore = GameObject.Find("Text_Score").GetComponent<Text>();
        timer = GameObject.Find("Text_Timer").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        updateTimer();
    }
}
