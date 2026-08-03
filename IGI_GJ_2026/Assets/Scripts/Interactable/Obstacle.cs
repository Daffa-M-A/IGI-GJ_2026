using UnityEngine;

public class Obstacle : MonoBehaviour, IInteractable
{
    [Header("Obstacle Settings")]
    public string requiredSmartItem = "Linggis";
    public string requiredBruteItem = "Palu";
    public int bruteForceHitsRequired = 3;
    private int currentHits = 0;

    public string GetInteractText()
    {
        string heldItem = InventoryManager.Instance.currentItem;

        if (string.IsNullOrEmpty(heldItem))
            return "Rintangan menghalangi jalan. Cari alat!";
        else
            return $"[Klik Kiri] Gunakan {heldItem} pada rintangan";
    }

    public void OnInteractE()
    {
        Debug.Log("[SYSTEM] Objek ini terlalu besar untuk diambil.");
    }

    public void OnInteractLMB()
    {
        string heldItem = InventoryManager.Instance.currentItem;

        if (heldItem == requiredBruteItem)
        {
            currentHits++;
            Debug.Log($"[BRUTE FORCE] Memukul rintangan secara paksa... ({currentHits}/{bruteForceHitsRequired})");

            if (currentHits >= bruteForceHitsRequired)
            {
                ClearObstacle("BruteForce");
            }
        }
        else if (heldItem == requiredSmartItem)
        {
            Debug.Log("[SMART THINKING] Menggunakan celah pada rintangan. Terbuka instan!");
            ClearObstacle("SmartThinking");
        }
        else
        {
            Debug.Log($"[SYSTEM] {heldItem} tidak memiliki efek pada rintangan ini.");
        }
    }

    void ClearObstacle(string solutionType)
    {
        gameObject.SetActive(false);

        LevelManager.Instance.StopTimer();

        if (solutionType == "BruteForce")
        {
            InventoryManager.Instance.AddCoin(1);
        }
        else if (solutionType == "SmartThinking")
        {
            InventoryManager.Instance.AddCoin(5);
        }

        InventoryManager.Instance.RemoveItem();
    }
}