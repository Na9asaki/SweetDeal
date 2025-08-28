using UnityEngine;
using FMODUnity;

public class FootstepsRun : MonoBehaviour
{
    [SerializeField] private EventReference footstepEvent;

    // Этот метод будет вызываться из Animation Event
    public void StepRun()
    {
        RuntimeManager.PlayOneShot(footstepEvent, transform.position);
    }
}