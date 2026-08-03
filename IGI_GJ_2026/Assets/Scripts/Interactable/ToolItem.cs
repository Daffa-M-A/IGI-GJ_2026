using UnityEngine;

public class ToolItem : MonoBehaviour, IInteractable
{
    [Header("Item Settings")]
    public string toolName;

    public string GetInteractText()
    {
        return $"[E] Ambil {toolName}";
    }

    public void OnInteractE()
    {
        InventoryManager.Instance.EquipItem(toolName, gameObject);
    }

    public void OnInteractLMB() { }
}