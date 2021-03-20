using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public void PlayPressed()
    {
        Game.Start();
    }

    public void ExitPressed()
    {
        Debug.Log("Exit Pressed!");
        Application.Quit();
    }
}
