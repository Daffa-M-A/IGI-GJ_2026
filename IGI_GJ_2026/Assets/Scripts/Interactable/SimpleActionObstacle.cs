using UnityEngine;

public class SimpleActionObstacle : MonoBehaviour, IInteractable
{
    [Header("Action Settings")]
    public string actionName = "Putar Katup";
    public bool isSolved = false;

    public string GetInteractText()
    {
        if (isSolved) return "Sudah diaktifkan.";
        return $"[Klik Kiri] {actionName}";
    }

    public void OnInteractE() { }

    public void OnInteractLMB()
    {
        if (isSolved) return;

        Debug.Log($"[ACTION] Melakukan aksi: {actionName}");
        isSolved = true;

        // Panggil animasi putar katup atau turunkan tuas di sini
        // GetComponent<Animator>().SetTrigger("Activate");

        ClearObstacle();
    }

    void ClearObstacle()
    {
        LevelManager.Instance.StopTimer();
        InventoryManager.Instance.AddCoin(2);
        Debug.Log($"[SYSTEM] {actionName} berhasil dilakukan. Masalah selesai.");
    }
}