// 2021.03.20 Tihonovschi Andrei
// Blocking cubes

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IInteractable
{
    // Implement IInteractable
    // directed interaction
    public void Interact(GameObject interactor, Vector3 direction)
    {
        // Blocking moving objects are implemented through
        // doing some ZERO VIRTUAL DAMAGE to interactors.
        // but only the interaction occur with front of blocking
        // cube - i.e. object normal direction at collision
        // point should be (-1, 0, 0)

        // Get IDestructible interface of interactor
        IDestructible destructible = interactor.GetComponent<IDestructible>();

        // if is destructible, and direction is corresponding
        if (destructible != null && direction.x == -1)
        {
            // Inflict ZERO VIRTUAL DAMAGE to the interactor
            destructible.Damage(0, DamageType.VIRTUAL);
        }
    }

    // generic interaction (directionless)
    public void Interact(GameObject interactor)
    {
        // just "interact" with ZERO direction
        Interact(interactor, new Vector3(0, 0, 0));
    }
}
