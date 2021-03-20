using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IInteractable
{
    // Implement IInteractable
    public void Interact(GameObject interactor, Vector3 direction)
    {
        IDestructible destructible = interactor.GetComponent<IDestructible>();

        if (destructible != null && direction.x == -1)
        {
            destructible.Damage(0, DamageType.VIRTUAL);
        }
    }

    public void Interact(GameObject interactor)
    {
        Interact(interactor, new Vector3(0, 0, 0));
    }
}
