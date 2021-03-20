// 2021.03.20 Tihonovschi Andrei
// Score display

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Text UI element
    [SerializeField] Text _text = null;

    // Start is called before the first frame update
    void Start()
    {
        if (_text)
        {
            _text.text = "Score: 0";
        }
    }

    // Receive score increase (from collector(s))
    public void Increase(int s)
    {
        Game.ScoreIncrease(s);
        _text.text = "Score: " + Game.GetScore();
    }
}
