using UnityEngine;
using FMODUnity;

public class snd_enemy_walk : MonoBehaviour
{
    [SerializeField] private EventReference enemyWalkEvent;

    // Этот метод будет вызываться из Animation Event
    public void snd_monster_walk()
    {
        RuntimeManager.PlayOneShot(enemyWalkEvent, transform.position);
    }
}