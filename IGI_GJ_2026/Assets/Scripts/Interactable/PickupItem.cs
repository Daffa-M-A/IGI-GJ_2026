using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    [Header("Coin Settings")]
    public int coinValue = 1;

    public string GetInteractText()
    {
        return $"[E] Ambil Koin";
    }

    public void OnInteractE()
    {
        InventoryManager.Instance.AddCoin(coinValue);
        Destroy(gameObject);
    }

    public void OnInteractLMB()
    {

    }
}