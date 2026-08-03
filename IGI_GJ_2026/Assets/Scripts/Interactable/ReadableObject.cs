using UnityEngine;

public class ReadableObject : MonoBehaviour, IInteractable
{
    [Header("Document Settings")]
    public string promptText = "Baca Poster";
    [TextArea(5, 10)]
    public string documentContent;

    public string GetInteractText()
    {
        return $"[E] {promptText}";
    }

    public void OnInteractE()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowDocument(documentContent);
            Debug.Log("[SYSTEM] Menampilkan dokumen ke layar.");
        }
    }

    public void OnInteractLMB() { }
}