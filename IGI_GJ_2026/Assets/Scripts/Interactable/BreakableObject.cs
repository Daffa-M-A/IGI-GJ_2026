using UnityEngine;

public class BreakableObject : MonoBehaviour, IInteractable
{
    public string requiredTool = "Hammer"; 
    public string solutionFlag; 
    public GameObject brokenVisualPrefab; 

    public string GetInteractText()
    {
        string heldItem = InventoryManager.Instance.currentItem;
        if (heldItem == requiredTool) return $"[Klik Kiri] Hancurkan dengan {requiredTool}";
        return $"Butuh {requiredTool} untuk menghancurkan ini.";
    }

    public void OnInteractE() { }

    public void OnInteractLMB()
    {
        if (InventoryManager.Instance.currentItem == requiredTool)
        {
            Debug.Log($"[BREAKABLE] Berhasil dihancurkan dengan {requiredTool}!");

            if (SolutionTracker.Instance != null && !string.IsNullOrEmpty(solutionFlag))
                SolutionTracker.Instance.SetFlag(solutionFlag);

            if (brokenVisualPrefab != null)
                Instantiate(brokenVisualPrefab, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}