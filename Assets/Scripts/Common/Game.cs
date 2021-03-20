using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game
{
    private static int level = 0;
    private static int score = 0;

    public static void Start()
    {
        Debug.Log("Starting new game.");
        level = 0;
        score = 0;
        NextLevel();
    }

    public static void NextLevel()
    {
        ++level;
        Debug.Log("Loading scene 'Level' with level = " + level);
        SceneManager.LoadScene("Level");
    }

    public static void Finish()
    {
        Debug.Log("Finishing game.");
        SceneManager.LoadScene("Menu");
    }

    public static void ScoreIncrease(int increment)
    {
        score += increment;
    }

    public static int GetScore()
    {
        return score;
    }

    public static int GetLevel()
    {
        return level;
    }
}