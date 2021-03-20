// 2021.03.20 Tihonovschi Andrei
// Game global state and methods

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game
{
    // Current score and level
    private static int level = 0;
    private static int score = 0;

    // Start new game
    public static void Start()
    {
        level = 0;
        score = 0;
        // the level will be set to 1 on NextLevel() call
        NextLevel();
    }

    // Promote to next level
    public static void NextLevel()
    {
        ++level;
        // the level will be loaded and generated on Level scene initialization
        SceneManager.LoadScene("Level");
    }

    // Finish current game
    public static void Finish()
    {
        SceneManager.LoadScene("Menu");
    }

    // Increase the score
    public static void ScoreIncrease(int increment)
    {
        score += increment;
    }

    // Retrieve current score
    public static int GetScore()
    {
        return score;
    }

    // Retrieve current level
    public static int GetLevel()
    {
        return level;
    }
}