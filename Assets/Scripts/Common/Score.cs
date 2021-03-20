using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Text _text = null;

    // Start is called before the first frame update
    void Start()
    {
        if (_text)
        {
            _text.text = "Score: 0";
        }
    }

    // Update is called once per frame
    public void Increase(int s)
    {
        Game.ScoreIncrease(s);
        _text.text = "Score: " + Game.GetScore();
    }
}
