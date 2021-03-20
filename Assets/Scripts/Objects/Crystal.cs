using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour, ICollectible
{
    private int amount = 1;
    
    // icollectible
    public int Collect()
    {
        gameObject.SetActive(false);
        int a = amount;
        amount = 0;
        return a;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
    }
}
