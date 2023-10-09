using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GuardController guard = GetComponentInParent<GuardController>();
            guard.target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GuardController guard = GetComponentInParent<GuardController>();
            guard.target = null;
        }
    }
}
