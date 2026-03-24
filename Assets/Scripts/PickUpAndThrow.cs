
// This script allows the player to pick up, drop, and throw objects in the game world.
// It uses the StarterAssetsInputs for input handling and the InteractionSystem for detecting interactable objects.

using StarterAssets;
using UnityEngine;

public class PickUpAndThrow : MonoBehaviour
{
    public Transform currentObject;

    public Transform pickUpPoint;
    public float throwForce = 500f;

    private StarterAssetsInputs input;
    private InteractionSystem interactionSystem;

    private void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        interactionSystem = GetComponent<InteractionSystem>();
    }

    private void Update()
    {
        HandleInput();
    }

    void PickUpObject()
    {
        if (interactionSystem.currentInteractable != null)
        {
            // This is where Unity Editor's Error got started,
            // If i Press it was still behaving like holding the button.
            // So i got work around by setting it to false right after picking up the object,
            // which is not the best solution but it works for now
            // (Based on Unity's Forum This is issue related to Unity 6.4 Latest Version and not resolved yet).

            input.Interact = false;                         
            currentObject = interactionSystem.currentInteractable.transform;
            currentObject.transform.SetParent(pickUpPoint);
            interactionSystem.currentInteractable.rb.isKinematic = true;
            currentObject.transform.localPosition = Vector3.zero;
            currentObject.transform.localRotation = Quaternion.identity;
            interactionSystem.currentInteractable.canInteract = false;
        }
    }

    void DropObject()
    {
        if (currentObject != null)
        {
            input.Drop = false;
            currentObject.transform.SetParent(null);
            currentObject.GetComponent<Interactable>().rb.isKinematic = false;
            currentObject.GetComponent<Interactable>().canInteract = true;
            currentObject = null;
        }
    }

    void ThrowObject()
    {
        if (currentObject != null)
        {
            input.Throw = false;
            currentObject.transform.SetParent(null);
            Rigidbody rb = currentObject.GetComponent<Interactable>().rb;
            rb.isKinematic = false;
            rb.AddForce(Camera.main.transform.forward * throwForce / rb.mass, ForceMode.Impulse);   // Adjust throw force based on mass
            currentObject.GetComponent<Interactable>().canInteract = true;
            currentObject = null;
        }
    }

    void HandleInput()  // I could also use the StarterAssetsInputs events instead of checking the input every frame, but this is simpler for now
    {
        if (input.Interact)
        {
            PickUpObject();
        }
        else if (input.Drop)
        {
            DropObject();
        }
        else if (input.Throw && currentObject != null)
        {
            ThrowObject();
        }
    }
}
