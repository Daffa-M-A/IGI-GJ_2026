using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PushableObject : MonoBehaviour, IInteractable
{
    private Rigidbody rb;
    public string solutionFlag; 

    void Start() => rb = GetComponent<Rigidbody>();

    public string GetInteractText() => "[E] Tahan/Geser Objek";

    public void OnInteractE()
    {
        if (PlayerInteract.Instance != null)
        {
            PlayerInteract.Instance.GrabPhysicsObject(rb);

            if (SolutionTracker.Instance != null && !string.IsNullOrEmpty(solutionFlag))
            {
                SolutionTracker.Instance.SetFlag(solutionFlag);
                solutionFlag = ""; 
            }
        }
    }

    public void OnInteractLMB() { }
}