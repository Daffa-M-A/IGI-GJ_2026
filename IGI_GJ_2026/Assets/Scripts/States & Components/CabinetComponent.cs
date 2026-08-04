using UnityEngine;

public class CabinetComponent : MonoBehaviour, IInteractable
{
    [Header("State Settings")]
    public CabinetState currentState = CabinetState.Standing;

    [Header("Visual/Physics References")]
    public Rigidbody rb;

    void OnEnable()
    {
        ChaosEventManager.OnChaosEvent += HandleChaos;
    }

    void OnDisable()
    {
        ChaosEventManager.OnChaosEvent -= HandleChaos;
    }

    public void ChangeState(CabinetState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
        ApplyStateBehaviour();
    }

    private void ApplyStateBehaviour()
    {
        switch (currentState)
        {
            case CabinetState.Standing:
                rb.isKinematic = true;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;

            case CabinetState.Fallen:
                rb.isKinematic = false;
                transform.rotation = Quaternion.Euler(90, 0, 0);
                Debug.Log("[STATE] Kabinet telah jatuh.");
                break;

            case CabinetState.Moved:
                rb.isKinematic = false;
                Debug.Log("[STATE] Kabinet berhasil digeser player.");
                break;
        }
    }

    private void HandleChaos(ChaosType chaosType)
    {
        if (chaosType == ChaosType.Earthquake && currentState == CabinetState.Standing)
        {
            ChangeState(CabinetState.Fallen);
        }
    }

    public string GetInteractText()
    {
        if (currentState == CabinetState.Fallen)
            return "[E] Geser Kabinet";

        return "Kabinet masih berdiri kokoh.";
    }

    public void OnInteractE()
    {
        if (currentState == CabinetState.Fallen)
        {
            ChangeState(CabinetState.Moved);

            PlayerInteract.Instance.GrabPhysicsObject(rb);
        }
    }

    public void OnInteractLMB() { }
}