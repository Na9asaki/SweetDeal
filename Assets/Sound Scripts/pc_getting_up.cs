using UnityEngine;
using FMODUnity;

public class pc_get_up : MonoBehaviour
{
    [SerializeField] private EventReference getupEvent;

    // Этот метод будет вызываться из Animation Event
    public void pc_getting_up()
    {
        RuntimeManager.PlayOneShot(getupEvent, transform.position);
    }
}