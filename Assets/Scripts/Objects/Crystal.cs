// 2021.03.20 Tihonovschi Andrei
// Collectible Crystals

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Implements ICollectible - to be collected
public class Crystal : MonoBehaviour, ICollectible
{
    // can be created crystals with different
    // collectible values
    private int amount = 1;
    
    // ICollectible
    public int Collect()
    {
        // the crystal should disappear when collected
        gameObject.SetActive(false);
        int a = amount;
        // this is to ensure the crystal is collected only once
        amount = 0;
        return a;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
    }
}
