// 2021.03.20 Tihonovschi Andrei
// Collectors collect collectibles

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Objects with Collector component will be able to collect collectibles
public class Collector : MonoBehaviour
{
    // when interacting with a trigger (collectibles)
    private void OnTriggerEnter(Collider c)
    {
        // Is collectible?
        ICollectible collectible = c.gameObject.GetComponent<ICollectible>();
        if (collectible != null)
        {
            // collect and increase score
            var score = (GameObject) GameObject.FindWithTag("Score");
            score.GetComponent<Score>().Increase(collectible.Collect());
        }
    }
}
