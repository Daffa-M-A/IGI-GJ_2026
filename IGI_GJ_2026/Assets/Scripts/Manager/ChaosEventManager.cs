using UnityEngine;
using System;

public enum ChaosType
{
    Earthquake,
    FireSpread,
    ElectricSurge
}

public class ChaosEventManager : MonoBehaviour
{
    public static ChaosEventManager Instance { get; private set; }

    public static event Action<ChaosType> OnChaosEvent;

    [Header("Chaos Intervals")]
    public float earthquakeInterval = 20f;
    public float fireInterval = 5f;
    public float electricInterval = 1f;

    private float eqTimer, fireTimer, elecTimer;
    private bool isChaosActive = true; 

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (!isChaosActive) return;

        eqTimer += Time.deltaTime;
        if (eqTimer >= earthquakeInterval)
        {
            eqTimer = 0f;
            TriggerChaos(ChaosType.Earthquake);
        }

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            fireTimer = 0f;
            TriggerChaos(ChaosType.FireSpread);
        }

        elecTimer += Time.deltaTime;
        if (elecTimer >= electricInterval)
        {
            elecTimer = 0f;
            TriggerChaos(ChaosType.ElectricSurge);
        }
    }

    private void TriggerChaos(ChaosType type)
    {
        Debug.Log($"[CHAOS BROADCAST] {type} terjadi!");

        OnChaosEvent?.Invoke(type);
    }

    public void StopAllChaos()
    {
        isChaosActive = false;
    }
}