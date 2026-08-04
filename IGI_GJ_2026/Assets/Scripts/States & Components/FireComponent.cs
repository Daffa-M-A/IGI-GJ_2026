using UnityEngine;

public class FireComponent : MonoBehaviour, IInteractable
{
    [Header("State Settings")]
    public FireState currentState = FireState.Small;

    [Header("Visual References")]
    public ParticleSystem smallFireVisual;
    public ParticleSystem largeFireVisual;
    public Collider fireCollider; 

    void Start()
    {
        ApplyStateBehaviour();
    }

    void OnEnable()
    {
        ChaosEventManager.OnChaosEvent += HandleChaos;
    }

    void OnDisable()
    {
        ChaosEventManager.OnChaosEvent -= HandleChaos;
    }

    public void ChangeState(FireState newState)
    {
        if (currentState == newState) return;
        currentState = newState;
        ApplyStateBehaviour();
    }

    private void ApplyStateBehaviour()
    {
        switch (currentState)
        {
            case FireState.Off:
                smallFireVisual.Stop();
                largeFireVisual.Stop();
                fireCollider.enabled = false;
                Debug.Log("[STATE] Api Padam.");
                break;

            case FireState.Small:
                smallFireVisual.Play();
                largeFireVisual.Stop();
                fireCollider.enabled = true;
                Debug.Log("[STATE] Api Kecil.");
                break;

            case FireState.Large:
                smallFireVisual.Play();
                largeFireVisual.Play(); 
                fireCollider.enabled = true;
                Debug.Log("[STATE] Api Membesar!");
                break;
        }
    }

    private void HandleChaos(ChaosType chaosType)
    {
        if (chaosType == ChaosType.FireSpread && currentState == FireState.Small)
        {
            ChangeState(FireState.Large);
        }
    }

    public string GetInteractText()
    {
        if (currentState == FireState.Off) return "";

        string heldItem = InventoryManager.Instance.currentItem;
        if (heldItem == "APAR")
            return "[Klik Kiri] Semprotkan APAR";

        return "Api menghalangi jalan. Cari pemadam!";
    }

    public void OnInteractE() { }

    public void OnInteractLMB()
    {
        if (InventoryManager.Instance.currentItem == "APAR" && currentState != FireState.Off)
        {
            ChangeState(FireState.Off);
        }
    }
}