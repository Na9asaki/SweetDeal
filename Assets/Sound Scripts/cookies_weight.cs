using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using SweetDeal.Source.Bakery;

public class FootstepAudioController : MonoBehaviour
{
    [Header("FMOD Events")]
    [SerializeField] private EventReference walkStepEvent;
    [SerializeField] private EventReference runStepEvent;
    
    [Header("Cookie Parameter Settings")]
    [SerializeField] private string cookieParameterName = "CookieWeight";
    [SerializeField] private float minCookieValue = 0f;
    [SerializeField] private float maxCookieValue = 100f;
    [SerializeField] private float minParameterValue = 0f;
    [SerializeField] private float maxParameterValue = 1f;
    
    [Header("References")]
    [SerializeField] private GameObject cargoObject; // Ссылка на объект с cargo скриптом
    [SerializeField] private string cargoObjectName = ""; // Имя объекта с cargo скриптом для поиска
    [SerializeField] private string cargoScriptName = "cargo"; // Имя cargo скрипта
    [SerializeField] private PlayerContainer playerContainer; // Ссылка на PlayerContainer для проверки ботинок
    
    private System.Reflection.FieldInfo cookieCountField;
    private System.Reflection.PropertyInfo cookieCountProperty;
    private Component cargoComponent;
    
    void Start()
    {
        // Находим cargo скрипт
        FindCargoScript();
        
        // Находим PlayerContainer если не назначен
        if (playerContainer == null)
        {
            playerContainer = FindObjectOfType<PlayerContainer>();
            
        }
    }
    
    private void FindCargoScript()
    {
        // Если объект не назначен вручную, ищем по всей сцене
        if (cargoObject == null)
        {
            cargoObject = FindCargoObjectInScene();
        }
        
        if (cargoObject != null)
        {
            // Ищем компонент cargo на найденном объекте
            cargoComponent = FindCargoComponent(cargoObject);
            
            if (cargoComponent != null)
            {
                SetupCookieAccess();
            }
        }
    }
    
    private GameObject FindCargoObjectInScene()
    {
        // Сначала пытаемся найти по указанному имени
        if (!string.IsNullOrEmpty(cargoObjectName))
        {
            GameObject foundByName = GameObject.Find(cargoObjectName);
            if (foundByName != null)
            {
                return foundByName;
            }
        }
        
        // Ищем по всем объектам в сцене
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            Component cargoComp = FindCargoComponent(obj);
            if (cargoComp != null)
            {
                return obj;
            }
        }
        
        // Пытаемся найти по распространенным именам
        string[] commonNames = { "Player", "Character", "Hero", "Inventory", "InventoryManager" };
        foreach (string name in commonNames)
        {
            GameObject found = GameObject.Find(name);
            if (found != null)
            {
                Component cargoComp = FindCargoComponent(found);
                if (cargoComp != null)
                {
                    return found;
                }
            }
        }
        
        return null;
    }
    
    private Component FindCargoComponent(GameObject obj)
    {
        Component[] components = obj.GetComponents<Component>();
        foreach (Component comp in components)
        {
            if (comp == null) continue;
            
            string typeName = comp.GetType().Name.ToLower();
            string scriptName = cargoScriptName.ToLower();
            
            // Ищем по точному имени или содержащим ключевые слова
            if (typeName.Contains(scriptName) || 
                typeName.Contains("cargo") || 
                typeName.Contains("inventory") ||
                typeName.Contains("item"))
            {
                return comp;
            }
        }
        return null;
    }
    
    private void SetupCookieAccess()
    {
        System.Type cargoType = cargoComponent.GetType();
        
        // Список возможных имен полей/свойств для печенья (приоритет для Count)
        string[] cookieFieldNames = { "Count", "count", "cookieCount", "cookies", "cookieAmount", "cookie", "Cookie", "CookieCount" };
        
        // Пытаемся найти поле
        foreach (string fieldName in cookieFieldNames)
        {
            cookieCountField = cargoType.GetField(fieldName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (cookieCountField != null)
            {
                return;
            }
        }
        
        // Пытаемся найти свойство
        foreach (string propName in cookieFieldNames)
        {
            cookieCountProperty = cargoType.GetProperty(propName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (cookieCountProperty != null)
            {
                return;
            }
        }
    }
    
    private int GetCookieCount()
    {
        if (cargoComponent == null) return 0;
        
        try
        {
            if (cookieCountField != null)
            {
                return (int)cookieCountField.GetValue(cargoComponent);
            }
            else if (cookieCountProperty != null)
            {
                return (int)cookieCountProperty.GetValue(cargoComponent);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Ошибка получения количества печенья: {e.Message}");
        }
        
        return 0;
    }
    
    private float CalculateCookieParameter()
    {
        int cookieCount = GetCookieCount();
        
        
        // Проверяем валидность диапазона
        if (maxCookieValue <= minCookieValue)
        {
            return minParameterValue;
        }
        
        // Нормализуем значение в диапазон 0-1
        float normalizedValue = Mathf.InverseLerp(minCookieValue, maxCookieValue, cookieCount);
        
        // Преобразуем в диапазон параметра
        float parameterValue = Mathf.Lerp(minParameterValue, maxParameterValue, normalizedValue);
        
        // Ограничиваем значение в допустимых пределах
        parameterValue = Mathf.Clamp(parameterValue, minParameterValue, maxParameterValue);
        
        
        return parameterValue;
    }
    
    // Проверяем, куплены ли ботинки
    private bool AreBootsPurchased()
    {
        if (playerContainer == null) return false;
        return playerContainer.BootsBoost > 0f;
    }
    
    // Метод для вызова из анимации - шаг при ходьбе
    public void stepWalk()
    {
        
        // Не воспроизводим звук, если куплены ботинки
        if (AreBootsPurchased())
        {
            return;
        }
        
        if (walkStepEvent.IsNull)
        {
            return;
        }
        RuntimeManager.PlayOneShot(walkStepEvent);
        
        // Если нужны параметры, добавим их отдельно
        if (!string.IsNullOrEmpty(cookieParameterName))
        {
            try
            {
                float cookieParam = CalculateCookieParameter();
                
                EventInstance walkInstance = RuntimeManager.CreateInstance(walkStepEvent);
                walkInstance.setParameterByName(cookieParameterName, cookieParam);
                walkInstance.start();
                walkInstance.release();
                
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Ошибка с параметрами: {e.Message}");
            }
        }
    }
    
    // Метод для вызова из анимации - шаг при беге
    public void stepRun()
    {
        
        // Не воспроизводим звук, если куплены ботинки
        if (AreBootsPurchased())
        {
            return;
        }
        
        if (runStepEvent.IsNull)
        {
            return;
        }
        
        RuntimeManager.PlayOneShot(runStepEvent);
        
        // Если нужны параметры, добавим их отдельно
        if (!string.IsNullOrEmpty(cookieParameterName))
        {
            try
            {
                float cookieParam = CalculateCookieParameter();
                
                EventInstance runInstance = RuntimeManager.CreateInstance(runStepEvent);
                runInstance.setParameterByName(cookieParameterName, cookieParam);
                runInstance.start();
                runInstance.release();
                
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Ошибка с параметрами: {e.Message}");
            }
        }
    }
    
    // Альтернативный метод с указанием типа шага
    public void PlayFootstep(string stepType)
    {
        switch (stepType.ToLower())
        {
            case "walk":
                stepWalk();
                break;
            case "run":
                stepRun();
                break;
            default:
                break;
        }
    }
    
    // Устаревшие методы для обратной совместимости
    public void PlayWalkStep() => stepWalk();
    public void PlayRunStep() => stepRun();
    
    // Метод для настройки ссылки на cargo объект из кода
    public void SetCargoObject(GameObject cargo)
    {
        cargoObject = cargo;
        FindCargoScript();
    }
    
    // Метод для настройки диапазонов параметров
    public void SetParameterRanges(float minCookie, float maxCookie, float minParam, float maxParam)
    {
        minCookieValue = minCookie;
        maxCookieValue = maxCookie;
        minParameterValue = minParam;
        maxParameterValue = maxParam;
    }
}
