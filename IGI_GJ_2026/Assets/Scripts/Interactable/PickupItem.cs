using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    public string itemName;
    public string solutionFlag;

    public string GetInteractText() => $"[E] Ambil {itemName}";

    public void OnInteractE()
    {
        InventoryManager.Instance.EquipItem(itemName, gameObject);

        if (SolutionTracker.Instance != null && !string.IsNullOrEmpty(solutionFlag))
            SolutionTracker.Instance.SetFlag(solutionFlag);

        if (RestrictionManager.Instance != null)
            RestrictionManager.Instance.RecordInteraction(itemName);
    }

    public void OnInteractLMB() { }
}