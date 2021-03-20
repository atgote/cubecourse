using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter(Collider c)
    {
        ICollectible collectible = c.gameObject.GetComponent<ICollectible>();
        if (collectible != null)
        {
            var score = (GameObject) GameObject.FindWithTag("Score");
            score.GetComponent<Score>().Increase(collectible.Collect());
        }
    }
}
