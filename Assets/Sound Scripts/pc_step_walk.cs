using UnityEngine;
using FMODUnity;

public class Footsteps : MonoBehaviour
{
    [SerializeField] private EventReference footstepEvent;

    // Этот метод будет вызываться из Animation Event
    public void StepWalk()
    {
        RuntimeManager.PlayOneShot(footstepEvent, transform.position);
    }
}