// 2021.03.20 Tihonovschi Andrei
// Interface for interactable objects

using UnityEngine;

public interface IInteractable
{
    // Interact with object. The caller transmits itself (or something else) as interactor.
    // The interactable object may use the interactor reference for 
    // callbacks - such as damaging interactor or doing any other things.

    // generic interaction receiver
    public void Interact(GameObject interactor);

    // specific interaction receiver (with direction vector)
    public void Interact(GameObject interactor, Vector3 direction);
}