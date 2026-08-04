using System.Collections.Generic;
using UnityEngine;

public class DiaryDataManager : MonoBehaviour
{
    public static DiaryDataManager Instance { get; private set; }

    [Header("Unlocked Solutions Data")]
    private HashSet<string> unlockedSolutions = new HashSet<string>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UnlockSolutionBranch(string branchID)
    {
        if (!unlockedSolutions.Contains(branchID))
        {
            unlockedSolutions.Add(branchID);
            Debug.Log($"[DIARY] Cabang baru terbuka: {branchID}");
        }
    }

    public bool IsBranchUnlocked(string branchID)
    {
        return unlockedSolutions.Contains(branchID);
    }
}