using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WinConditionTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.ExecuteWinCondition();
        }
    }
}