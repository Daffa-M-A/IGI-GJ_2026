using UnityEngine;

public class InstallItemObstacle : MonoBehaviour, IInteractable
{
    [Header("Installation Settings")]
    public string requiredItemToInstall = "Sekering";
    public GameObject installedVisualPrefab; 

    private bool hasBeenInstalled = false;

    public string GetInteractText()
    {
        if (hasBeenInstalled) return "Telah terpasang.";

        string heldItem = InventoryManager.Instance.currentItem;

        if (heldItem == requiredItemToInstall)
            return $"[Klik Kiri] Pasang {requiredItemToInstall}";
        else
            return $"Butuh {requiredItemToInstall} untuk dipasang di sini.";
    }

    public void OnInteractE() { }

    public void OnInteractLMB()
    {
        if (hasBeenInstalled) return;

        string heldItem = InventoryManager.Instance.currentItem;

        if (heldItem == requiredItemToInstall)
        {
            Debug.Log($"[INSTALLATION] Memasang {requiredItemToInstall} ke dalam sistem...");

            if (installedVisualPrefab != null)
            {
                installedVisualPrefab.SetActive(true);
            }

            hasBeenInstalled = true;

            InventoryManager.Instance.RemoveItem();

            ClearObstacle();
        }
        else
        {
            Debug.Log($"[SYSTEM] Kamu tidak memegang barang yang tepat. Butuh: {requiredItemToInstall}");
        }
    }

    void ClearObstacle()
    {
        LevelManager.Instance.StopTimer();
        InventoryManager.Instance.AddCoin(5);
        Debug.Log("[SYSTEM] Item berhasil dipasang. Jalan atau sistem terbuka.");

        GetComponent<Collider>().enabled = false;
    }
}