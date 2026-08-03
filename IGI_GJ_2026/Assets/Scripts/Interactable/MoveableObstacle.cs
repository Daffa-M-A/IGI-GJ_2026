using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveableObject : MonoBehaviour, IInteractable
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public string GetInteractText()
    {
        return "[E] Tahan untuk Menggeser Objek";
    }

    public void OnInteractE()
    {
        if (PlayerInteract.Instance != null)
        {
            PlayerInteract.Instance.GrabPhysicsObject(rb);
        }
    }

    public void OnInteractLMB() { }
}