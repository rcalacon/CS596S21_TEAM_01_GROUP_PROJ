using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonBoidsManager : MonoBehaviour
{
    private string scorePrefix = "SCORE: ";
    private string timeSuffix = "s LEFT";
    private static bool gameIsPaused = true;
    private static bool gameEnded = false;
    private int numSeconds = 3;
    private Text gameScore;
    private Text timer;
    private float time;
    private float startTime;
    public static int currentLevel;
    public static int lastLevel = 3;
    public GameObject boidsController;

    public void updateScore()
    {
        gameScore = GameObject.Find("Text_Score").GetComponent<Text>();
        char space = ' ';
        int score = int.Parse(gameScore.text.Split(space)[1]);
        score++;
        gameScore.text = "SCORE: " + score.ToString();
    }

    public void updateTimer()
    {
        if (!gameIsPaused)
        {
            time += Time.deltaTime;
            int secondsElapsed = Mathf.FloorToInt(time % 60f);
            int timeLeft = numSeconds - secondsElapsed;
            if (timeLeft >= 0)
            {
                timer.text = timeLeft.ToString() + timeSuffix;
            }
            // New Level Has Started
            else
            {
                StartNextLevel();
            }
        }
        else
        {
            startTime += Time.deltaTime;
            var startTimer = GameObject.Find("Image_Timer");
            int startSecondsElapsed = Mathf.FloorToInt(startTime % 60f);
            switch (startSecondsElapsed)
            {
                case 0:
                    GameObject.Find("Text_Three").GetComponent<Image>().enabled = true;
                    GameObject.Find("Text_Two").GetComponent<Image>().enabled = false;
                    GameObject.Find("Text_One").GetComponent<Image>().enabled = false;
                    break;
                case 1:
                    GameObject.Find("Text_Three").GetComponent<Image>().enabled = false;
                    GameObject.Find("Text_Two").GetComponent<Image>().enabled = true;
                    GameObject.Find("Text_One").GetComponent<Image>().enabled = false;
                    break;
                case 2:
                    GameObject.Find("Text_Three").GetComponent<Image>().enabled = false;
                    GameObject.Find("Text_Two").GetComponent<Image>().enabled = false;
                    GameObject.Find("Text_One").GetComponent<Image>().enabled = true;
                    break;
                default:
                    GameObject.Find("Text_Three").GetComponent<Image>().enabled = false;
                    GameObject.Find("Text_Two").GetComponent<Image>().enabled = false;
                    GameObject.Find("Text_One").GetComponent<Image>().enabled = false;
                    gameIsPaused = false;
                    break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("Text_Timer").GetComponent<Text>();
        currentLevel = 1;
        GameObject.Find("Text_Three").GetComponent<Image>().enabled = true;
        GameObject.Find("Text_Two").GetComponent<Image>().enabled = false;
        GameObject.Find("Text_One").GetComponent<Image>().enabled = false;
        ToggleGameOver(false);
    }

    void ToggleGameOver(bool show)
    {
        GameObject.Find("Text_Game").GetComponent<Image>().enabled = show;
        GameObject.Find("Text_Over").GetComponent<Image>().enabled = show;
        GameObject.Find("Button_Exit").GetComponent<Image>().enabled = show;
        GameObject.Find("Text_Exit").GetComponent<Text>().enabled = show;
    }

    public void GoToLobby()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    void StartNextLevel()
    {
        gameIsPaused = true;

        currentLevel++;
        if(currentLevel > lastLevel)
        {
            gameEnded = true;
            boidsController.GetComponent<BoidsController>().SetLevel(-1);
            ToggleGameOver(true);
        }
        else
        {
            boidsController.GetComponent<BoidsController>().ClearBoids();
            boidsController.GetComponent<BoidsController>().SetLevel(currentLevel);
            boidsController.GetComponent<BoidsController>().CreateBoids();
            startTime = 0;
            time = 0;
        }
    }

    public bool IsPaused()
    {
        return gameIsPaused;
    }

    public bool IsEnded()
    {
        return gameEnded;
    }

    // Update is called once per frame
    void Update()
    {
        updateTimer();
    }
}
