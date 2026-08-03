using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TimerStartTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.ResetAndStartTimer();

            GetComponent<Collider>().enabled = false;
        }
    }
}