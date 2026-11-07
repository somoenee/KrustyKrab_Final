
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionRange = 2f;
    public KeyCode interactionKey = KeyCode.E;

    void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(
            transform.position,
            interactionRange
        );

        foreach (Collider obj in objectsInRange)
        {
            if (obj.CompareTag("Interactable"))
            {
                InteractableObject interactable =
                    obj.GetComponentInParent<InteractableObject>();

                if (interactable != null)
                {
                    interactable.Interact();
                    return;
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
