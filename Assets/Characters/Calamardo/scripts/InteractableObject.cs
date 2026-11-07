
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [Header("Interaction")]
    public UnityEvent onInteract;

    public void Interact()
    {
        Debug.Log("Interacting with: " + gameObject.name);
        onInteract?.Invoke();
    }
}
