// 2021.03.20 Tihonovschi Andrei
// Cubes...

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cubes can be interacted with (when stacking with player)
// as well as "destructible" - when touching "lava" or blocked by blocks
public class Cube : MonoBehaviour, IDestructible, IInteractable
{
    // cube durability value
    private float durability = 1;

    // When the Cube interacts (collide) with other object
    private void OnCollisionEnter(Collision c)
    {
        // try to "interact" with that object
        IInteractable interactable = c.gameObject.GetComponent<IInteractable>();

        // if is interactable
        if (interactable != null)
        {
            // get direction and interact in that direction
            Vector3 direction = c.GetContact(0).normal;
            interactable.Interact(gameObject, direction);
        }
    }

    // When the Cube is interacted by other object (Player or other cube), generic (withou direction)
    public void Interact(GameObject interactor)
    {
        // if the interactor is a Stacked object (Player or Player's "Cube tower")
        Stacked stacked = interactor.GetComponent<Stacked>();

        if (stacked != null)
        {
            GameObject top = stacked.GetFirst();

            if (top.GetComponent<Player>() != null) // if is interacted not by Player or Player's "Cube tower"
            {
                return;
            }

            // else - stack
            stacked.Stack(gameObject);
        }
    }

    // interection with known direction
    public void Interact(GameObject interactor, Vector3 direction)
    {
        // just use generic interaction
        Interact(interactor);
    }

    // receiving generic damage
    public void Damage(float amount)
    {
        durability -= amount;
    }

    // receiving damage of defined type
    public void Damage(float amount, DamageType type)
    {
        // if cube is blocked by a block - VIRTUAL damage received
        if (type == DamageType.VIRTUAL)
        {
            // unstack / slice
            GameObject upper = gameObject.GetComponent<Stacked>().GetUpper();
            if (upper != null)
            {
                Stacked sc = upper.GetComponent<Stacked>();
                if (sc != null)
                {
                    sc.Unstack(gameObject);
                }
            }
        }
        // decrement durability by calling generic damage receiver
        Damage(amount);
    }

    private void Update()
    {
        if (durability <= 0)    // if Cube was destroyed by lava
        {
            GameObject upper = gameObject.GetComponent<Stacked>().GetUpper();
            upper.GetComponent<Stacked>().Unstack(gameObject);
            gameObject.SetActive(false);
        }
    }
}
