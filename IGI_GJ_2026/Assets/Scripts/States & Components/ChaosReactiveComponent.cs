using UnityEngine;
using UnityEngine.Events;

public class ChaosReactiveComponent : MonoBehaviour
{
    public ChaosType targetChaosToReact; 

    [Tooltip("Apa yang terjadi saat event ini memanggil objek ini? (Contoh: Fall, Grow, Toggle)")]
    public UnityEvent OnReactToAction;

    void OnEnable()
    {
        ChaosEventManager.OnChaosEvent += HandleChaos;
    }

    void OnDisable()
    {
        ChaosEventManager.OnChaosEvent -= HandleChaos;
    }

    private void HandleChaos(ChaosType incomingChaos)
    {
        if (incomingChaos == targetChaosToReact)
        {
            Debug.Log($"[CHAOS REACTION] {gameObject.name} bereaksi terhadap {incomingChaos}!");
            OnReactToAction?.Invoke();
        }
    }

    public void Fall()
    {
        if (TryGetComponent<Rigidbody>(out Rigidbody rb)) rb.isKinematic = false;
        transform.Rotate(90, 0, 0);
    }

    public void Inflate()
    {
        transform.localScale = new Vector3(2, 2, 2);
    }
}