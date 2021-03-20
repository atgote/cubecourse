using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour, IInteractable
{
    bool isBurning = true;

    // Start is called before the first frame update
    void Start()
    {
        isBurning = true;
    }

    // IInteractable implementation
    public void Interact(GameObject interactor)
    {
        if (!isBurning)
            return;

        IDestructible destructible = interactor.GetComponent<IDestructible>();

        if (destructible != null)
        {
            destructible.Damage(10, DamageType.FIRE);
            isBurning = false;
            // rise up... Just to show what happens
            gameObject.transform.position += new Vector3(0f, .25f, 0f);
        }
    }

    public void Interact(GameObject interactor, Vector3 direction)
    {
        Interact(interactor);
    }
}
