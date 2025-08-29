using UnityEngine;
using FMODUnity;

public class pc_slip : MonoBehaviour
{
    [SerializeField] private EventReference slipEvent;

    // Этот метод будет вызываться из Animation Event
    public void SlipSound()
    {
        RuntimeManager.PlayOneShot(slipEvent, transform.position);
    }
}