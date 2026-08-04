using UnityEngine;

public class ToggleSwitch : MonoBehaviour, IInteractable
{
    public string switchName = "Valve";
    public bool isOn = false;
    public string turnOnFlag; 

    public string GetInteractText()
    {
        return isOn ? $"[Klik Kiri] Matikan {switchName}" : $"[Klik Kiri] Nyalakan {switchName}";
    }

    public void OnInteractE() { }

    public void OnInteractLMB()
    {
        isOn = !isOn;
        Debug.Log($"[TOGGLE] {switchName} sekarang {(isOn ? "ON" : "OFF")}.");

        if (isOn && SolutionTracker.Instance != null && !string.IsNullOrEmpty(turnOnFlag))
        {
            SolutionTracker.Instance.SetFlag(turnOnFlag);
        }
    }
}