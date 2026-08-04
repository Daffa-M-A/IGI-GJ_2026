using UnityEngine;
using TMPro; 

public class DiaryUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject diaryPanel;
    public TextMeshProUGUI pageContentText;
    public TextMeshProUGUI titleText;

    public void OpenDiary(int floorNumber)
    {
        diaryPanel.SetActive(true);
        titleText.text = $"CABANG SOLUSI - LANTAI {floorNumber}";

        string content = "";

        if (floorNumber == 4)
        {
            content += FormatBranchText("Floor4_Brute_Force", "Firefighter (Brute Force)");
            content += FormatBranchText("Floor4_Smart_A", "Hydraulic Thinker (Smart A)");
            content += FormatBranchText("Floor4_Smart_B", "Controlled Burn (Smart B)");
        }
        else if (floorNumber == 3)
        {
            content += FormatBranchText("Floor3_Brute_Force", "Electric Shock (Brute Force)");
            content += FormatBranchText("Floor3_Smart_A", "Wire Master (Smart A)");
        }
        else
        {
            content += "Data lantai ini belum tersedia.\n";
        }

        pageContentText.text = content;
    }

    public void CloseDiary()
    {
        diaryPanel.SetActive(false);
    }

    private string FormatBranchText(string branchID, string branchTrueName)
    {
        if (DiaryDataManager.Instance != null && DiaryDataManager.Instance.IsBranchUnlocked(branchID))
        {
            return $"[ \u2713 ] {branchTrueName}\n\n";
        }
        else
        {
            return $"[ ? ] ???????????????\n\n"; 
        }
    }
}