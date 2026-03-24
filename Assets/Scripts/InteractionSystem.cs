
// This Script is responsible for handling player interactions with objects in the game world.
// It uses raycasting to detect interactable objects and updates the UI to show interaction prompts.

using StarterAssets;
using TMPro;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{

    public Interactable currentInteractable;
    public TextMeshProUGUI interactionText;


    private void Update()
    {
        CheckForInteractable();
        HandleInteractionUI();
    }

    void CheckForInteractable()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit,10f))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null && interactable.canInteract)
            {
                currentInteractable = interactable;
            }
            else
            {
                currentInteractable = null;
            }
        }

    }

    void HandleInteractionUI()
    {
        if (currentInteractable == null)
        {
            interactionText.text = "";
        }
        else
        {
            interactionText.text = currentInteractable.interactText;
        }
    }
}
