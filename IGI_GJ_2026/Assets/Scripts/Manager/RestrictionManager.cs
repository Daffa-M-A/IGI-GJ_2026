using System.Collections.Generic;
using UnityEngine;

public class RestrictionManager : MonoBehaviour
{
    public static RestrictionManager Instance { get; private set; }

    private HashSet<string> recentlyInteractedItems = new HashSet<string>();

    private HashSet<string> bannedItems = new HashSet<string>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RecordInteraction(string itemName)
    {
        if (!string.IsNullOrEmpty(itemName) && !recentlyInteractedItems.Contains(itemName))
        {
            recentlyInteractedItems.Add(itemName);
            Debug.Log($"[RESTRICTION] Mencatat penggunaan item: {itemName}");
        }
    }

    public void ApplyRestrictions()
    {
        foreach (string item in recentlyInteractedItems)
        {
            bannedItems.Add(item);
        }
        recentlyInteractedItems.Clear();
        Debug.Log("[RESTRICTION] Player gagal. Item yang dipakai sebelumnya telah dihilangkan dari map.");
    }

    public void ClearRestrictions()
    {
        bannedItems.Clear();
        recentlyInteractedItems.Clear();
        Debug.Log("[RESTRICTION] Hukuman direset. Semua item kembali tersedia.");
    }

    public bool IsItemBanned(string itemName)
    {
        return bannedItems.Contains(itemName);
    }
}