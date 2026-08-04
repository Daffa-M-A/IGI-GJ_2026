using System.Collections.Generic;
using UnityEngine;

public class SolutionTracker : MonoBehaviour
{
    public static SolutionTracker Instance { get; private set; }

    private Dictionary<string, bool> levelFlags = new Dictionary<string, bool>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetFlag(string flagName)
    {
        if (string.IsNullOrEmpty(flagName)) return;

        if (!levelFlags.ContainsKey(flagName))
        {
            levelFlags.Add(flagName, true);
        }
        else
        {
            levelFlags[flagName] = true;
        }

        Debug.Log($"[SOLUTION TRACKER] Flag tercatat: {flagName}");
    }

    public bool HasFlag(string flagName)
    {
        return levelFlags.ContainsKey(flagName) && levelFlags[flagName];
    }

    public void ResetFlags()
    {
        levelFlags.Clear();
        Debug.Log("[SOLUTION TRACKER] Lembar catatan dibersihkan untuk lantai baru.");
    }

    public string EvaluateFloorSolution(int floorNumber)
    {
        Debug.Log($"[SOLUTION TRACKER] Mengevaluasi solusi untuk Lantai {floorNumber}...");

        if (floorNumber == 4)
        {
            if (HasFlag("HasRotatedValve") && HasFlag("HasMovedDivider") && HasFlag("HasClosedBathroomValve"))
            {
                Debug.Log("[RESULT] Solusi Smart A ditemukan: Hydraulic Thinker!");
                return "Smart_A";
            }
            if (HasFlag("HasClosedAllWater") && HasFlag("HasWaitedFireSpread"))
            {
                Debug.Log("[RESULT] Solusi Smart B ditemukan: Controlled Burn!");
                return "Smart_B";
            }
            if (HasFlag("HasUsedCrowbar") && HasFlag("HasUsedAPAR"))
            {
                Debug.Log("[RESULT] Solusi Brute Force ditemukan: Firefighter!");
                return "Brute_Force";
            }
        }

        return "Standard_Clear";
    }
}