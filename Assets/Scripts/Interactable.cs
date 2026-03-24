
// Used Template Method for Handling Interactions with Objects as It's More Scalable and Flexible for Future Expansion.
// This Script Defines the Interactable Class, Which Can Be Derived for multiple different objects to Make It Interactable in the Game World.
// It Contains Properties for Interaction Text, Rigidbody Reference, and a Flag to Determine if the Object Can Be Interacted With.

using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string interactText;
    public Rigidbody rb;
    public bool canInteract = true;

    // Can Further Introduce or Expand Methods for Specific Interactions, Such as OnInteract,OnInspect etc.,
    // Which Can Be Overridden in Derived Classes for Custom Behavior.
}
