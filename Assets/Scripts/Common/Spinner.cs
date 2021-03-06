// 2021.03.20 Tihonovschi Andrei
// Spinner component
// Makes the object in scene spinning

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    private float spinSpeed = 90.0f;

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime * spinSpeed;

        gameObject.transform.Rotate(0, delta, 0);
    }
}
