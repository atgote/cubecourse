// 2021.03.20 Tihonovschi Andrei
// Menu controls

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    // Method to be called on "PLAY" button press
    public void PlayPressed()
    {
        Game.Start();
    }

    // Method to be called on "EXIT" button press
    public void ExitPressed()
    {
        // For testing button action inside Unity
        Debug.Log("Exit Pressed!");
        Application.Quit();
    }
}
