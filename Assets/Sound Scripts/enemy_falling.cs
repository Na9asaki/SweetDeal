using UnityEngine;
using FMODUnity;

public class enemy_falling : MonoBehaviour
{
    [SerializeField] private EventReference enemyFallingkEvent;

    // Этот метод будет вызываться из Animation Event
    public void enemyFalling()
    {
        RuntimeManager.PlayOneShot(enemyFallingkEvent, transform.position);
    }
}