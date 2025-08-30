using UnityEngine;
using FMODUnity;

public class SimpleFootstepTest : MonoBehaviour
{
    [Header("FMOD Events")]
    [SerializeField] private EventReference walkStepEvent;
    [SerializeField] private EventReference runStepEvent;
    
    [Header("Test Controls")]
    [SerializeField] private KeyCode testWalkKey = KeyCode.Q;
    [SerializeField] private KeyCode testRunKey = KeyCode.E;
    
    void Update()
    {
        // Тестовые клавиши для проверки воспроизведения
        if (Input.GetKeyDown(testWalkKey))
        {
            TestWalkStep();
        }
        
        if (Input.GetKeyDown(testRunKey))
        {
            TestRunStep();
        }
    }
    
    public void TestWalkStep()
    {
        Debug.Log("TestWalkStep() вызван!");
        
        if (walkStepEvent.IsNull)
        {
            Debug.LogError("Walk Step Event не назначен!");
            return;
        }
        
        // Простейший способ воспроизведения
        RuntimeManager.PlayOneShot(walkStepEvent);
        Debug.Log("Walk PlayOneShot выполнен");
    }
    
    public void TestRunStep()
    {
        Debug.Log("TestRunStep() вызван!");
        
        if (runStepEvent.IsNull)
        {
            Debug.LogError("Run Step Event не назначен!");
            return;
        }
        
        // Простейший способ воспроизведения
        RuntimeManager.PlayOneShot(runStepEvent);
        Debug.Log("Run PlayOneShot выполнен");
    }
    
    // Методы для анимационных событий (упрощенные)
    public void stepWalk()
    {
        TestWalkStep();
    }
    
    public void stepRun()
    {
        TestRunStep();
    }
}
