// 2021.03.20 Tihonovschi Andrei
// Finish blocks

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interactable finish blocks
public class Finish : MonoBehaviour, IInteractable
{
    // IInteractable implementation / interaction along a direction
    public void Interact(GameObject interactor, Vector3 direction)
    {
        // just interact without direction
        Interact(interactor);
    }

    // generic interaction
    public void Interact(GameObject interactor)
    {
        // if we have player or player tower (i.e. some stacked Cubes)
        Stacked sc = interactor.GetComponent<Stacked>();
        if (sc != null)
        {
            GameObject top = sc.GetFirst();
            Player player = top.GetComponent<Player>();

            if (player != null)
            {
                // promote to next level
                player.Promote();
            }
        }
    }
}
