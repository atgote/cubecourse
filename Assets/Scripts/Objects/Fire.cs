// 2021.03.20 Tihonovschi Andrei
// Fire / lava

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// interactable fire / lava blocks
public class Fire : MonoBehaviour, IInteractable
{
    // if fire is burning
    bool isBurning = true;

    // Start is called before the first frame update
    void Start()
    {
        isBurning = true;
    }

    // IInteractable implementation
    public void Interact(GameObject interactor)
    {
        // only if fire is burning
        if (!isBurning)
            return;

        // if interactor is destructible
        IDestructible destructible = interactor.GetComponent<IDestructible>();
        if (destructible != null)
        {
            // do fire damage / destruct interacting object
            destructible.Damage(10, DamageType.FIRE);
            // only once
            isBurning = false;
            // rise up block... Just to show in scene what happens
            gameObject.transform.position += new Vector3(0f, .25f, 0f);
        }
    }

    // direction-aware interaction
    public void Interact(GameObject interactor, Vector3 direction)
    {
        // just interact...
        Interact(interactor);
    }
}
