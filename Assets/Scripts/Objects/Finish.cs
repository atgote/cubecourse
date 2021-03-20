using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour, IInteractable
{
    // IInteractable implementation
    public void Interact(GameObject interactor, Vector3 direction)
    {
        Interact(interactor);
    }

    public void Interact(GameObject interactor)
    {
        Stacked sc = interactor.GetComponent<Stacked>();

        if (sc != null)
        {
            GameObject top = sc.GetFirst();
            Player player = top.GetComponent<Player>();

            if (player != null)
            {
                player.Promote();
            }
        }
    }
}
