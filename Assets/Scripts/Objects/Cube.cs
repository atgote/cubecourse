using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, IDestructible, IInteractable
{
    private float durability = 1;

    private void OnCollisionEnter(Collision c)
    {
        IInteractable interactable = c.gameObject.GetComponent<IInteractable>();

        if (interactable != null)
        {
            // get direction
            Vector3 direction = c.GetContact(0).normal;
            interactable.Interact(gameObject, direction);
        }
    }

    public void Interact(GameObject interactor)
    {
        Stacked stacked = interactor.GetComponent<Stacked>();

        if (stacked != null)
        {
            GameObject top = stacked.GetFirst();

            if (top.tag != "Player")
            {
                return;
            }

            stacked.Stack(gameObject);
        }
    }

    public void Interact(GameObject interactor, Vector3 direction)
    {
        Interact(interactor);
    }

    public void Damage(float amount)
    {
        durability -= amount;
    }

    public void Damage(float amount, DamageType type)
    {
        if (type == DamageType.VIRTUAL)
        {
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
        Damage(amount);
    }

    private void Update()
    {
        if (durability <= 0)
        {
            GameObject upper = gameObject.GetComponent<Stacked>().GetUpper();
            upper.GetComponent<Stacked>().Unstack(gameObject);
            gameObject.SetActive(false);
        }
    }
}
