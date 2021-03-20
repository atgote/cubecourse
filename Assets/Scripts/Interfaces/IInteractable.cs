using UnityEngine;

public interface IInteractable
{
    // Interact with object. The caller transmits itself (or something else) as interactor.
    public void Interact(GameObject interactor);
    public void Interact(GameObject interactor, Vector3 direction);
}