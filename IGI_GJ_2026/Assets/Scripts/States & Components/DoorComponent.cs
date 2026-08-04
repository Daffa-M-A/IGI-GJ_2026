using UnityEngine;

public enum DoorState { Closed, Open, HalfOpen, Locked }

public class DoorComponent : MonoBehaviour, IInteractable
{
    public DoorState currentState = DoorState.Closed;
    public string keyToolName = "Crowbar"; 
    public string unlockFlag; 

    public string GetInteractText()
    {
        switch (currentState)
        {
            case DoorState.Closed: return "[Klik Kiri] Buka Pintu";
            case DoorState.Open: return "[Klik Kiri] Tutup Pintu";
            case DoorState.HalfOpen: return "[Klik Kiri] Buka Pintu Lebar";
            case DoorState.Locked:
                string heldItem = InventoryManager.Instance.currentItem;
                if (heldItem == keyToolName) return $"[Klik Kiri] Congkel dengan {keyToolName}";
                return "Pintu Terkunci.";
            default: return "";
        }
    }

    public void OnInteractE() { }

    public void OnInteractLMB()
    {
        if (currentState == DoorState.Locked)
        {
            if (InventoryManager.Instance.currentItem == keyToolName)
            {
                ChangeState(DoorState.Open);
                if (SolutionTracker.Instance != null && !string.IsNullOrEmpty(unlockFlag))
                    SolutionTracker.Instance.SetFlag(unlockFlag);
            }
        }
        else if (currentState == DoorState.Closed || currentState == DoorState.HalfOpen)
        {
            ChangeState(DoorState.Open);
        }
        else if (currentState == DoorState.Open)
        {
            ChangeState(DoorState.Closed);
        }
    }

    public void ChangeState(DoorState newState)
    {
        currentState = newState;
        Debug.Log($"[DOOR] State berubah menjadi: {currentState}");
    }
}