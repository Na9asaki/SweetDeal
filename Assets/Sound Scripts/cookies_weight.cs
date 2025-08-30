using UnityEngine;
using FMODUnity;
using FMOD.Studio;

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
    
    private System.Reflection.FieldInfo cookieCountField;
    private System.Reflection.PropertyInfo cookieCountProperty;
    private Component cargoComponent;
    
    void Start()
    {
        // Находим cargo скрипт
        FindCargoScript();
    }
    
    void Update()
    {
        // Тестовые клавиши для проверки вызова методов
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Клавиша 1 нажата - тестируем stepWalk");
            stepWalk();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Клавиша 2 нажата - тестируем stepRun");
            stepRun();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Клавиша 3 нажата - проверяем Animation Events");
            Debug.Log("Убедитесь, что Animation Events настроены правильно!");
            Debug.Log("Function должна быть: stepWalk или stepRun");
            Debug.Log("Object должен указывать на этот GameObject: " + gameObject.name);
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
                Debug.Log($"Cargo компонент найден: {cargoComponent.GetType().Name} на объекте {cargoObject.name}");
            }
            else
            {
                Debug.LogWarning($"Cargo скрипт не найден на объекте {cargoObject.name}!");
            }
        }
        else
        {
            Debug.LogError("Объект с cargo скриптом не найден! Назначьте его вручную или укажите имя объекта.");
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
                Debug.Log($"Найден объект по имени: {cargoObjectName}");
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
                Debug.Log($"Найден cargo скрипт на объекте: {obj.name}");
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
                    Debug.Log($"Найден cargo скрипт на объекте с общим именем: {name}");
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
                Debug.Log($"Найдено поле для печенья: {fieldName}");
                return;
            }
        }
        
        // Пытаемся найти свойство
        foreach (string propName in cookieFieldNames)
        {
            cookieCountProperty = cargoType.GetProperty(propName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (cookieCountProperty != null)
            {
                Debug.Log($"Найдено свойство для печенья: {propName}");
                return;
            }
        }
        
        Debug.LogWarning("Поле/свойство для количества печенья не найдено!");
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
        
        // Детальное логирование для отладки
        Debug.Log($"Cookie Count: {cookieCount}, Min: {minCookieValue}, Max: {maxCookieValue}");
        
        // Проверяем валидность диапазона
        if (maxCookieValue <= minCookieValue)
        {
            Debug.LogWarning("Неверный диапазон печенья! Max должен быть больше Min.");
            return minParameterValue;
        }
        
        // Нормализуем значение в диапазон 0-1
        float normalizedValue = Mathf.InverseLerp(minCookieValue, maxCookieValue, cookieCount);
        
        // Преобразуем в диапазон параметра
        float parameterValue = Mathf.Lerp(minParameterValue, maxParameterValue, normalizedValue);
        
        // Ограничиваем значение в допустимых пределах
        parameterValue = Mathf.Clamp(parameterValue, minParameterValue, maxParameterValue);
        
        Debug.Log($"Normalized: {normalizedValue:F3}, Parameter: {parameterValue:F3}");
        
        return parameterValue;
    }
    
    // Метод для вызова из анимации - шаг при ходьбе
    public void stepWalk()
    {
        Debug.Log("stepWalk() вызван!");
        
        if (walkStepEvent.IsNull)
        {
            Debug.LogError("Walk Step Event не назначен в инспекторе!");
            return;
        }
        
        // ВРЕМЕННО: Используем простой способ как в тестовом скрипте
        Debug.Log("Пробуем простой PlayOneShot...");
        RuntimeManager.PlayOneShot(walkStepEvent);
        Debug.Log("PlayOneShot выполнен для walkStep");
        
        // Если нужны параметры, добавим их отдельно
        if (!string.IsNullOrEmpty(cookieParameterName))
        {
            Debug.Log("Пробуем с параметрами...");
            try
            {
                float cookieParam = CalculateCookieParameter();
                
                EventInstance walkInstance = RuntimeManager.CreateInstance(walkStepEvent);
                walkInstance.setParameterByName(cookieParameterName, cookieParam);
                walkInstance.start();
                walkInstance.release();
                
                Debug.Log($"Параметрическая версия: печенье={GetCookieCount()}, параметр={cookieParam:F3}");
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
        Debug.Log("stepRun() вызван!");
        
        if (runStepEvent.IsNull)
        {
            Debug.LogError("Run Step Event не назначен в инспекторе!");
            return;
        }
        
        // ВРЕМЕННО: Используем простой способ как в тестовом скрипте
        Debug.Log("Пробуем простой PlayOneShot...");
        RuntimeManager.PlayOneShot(runStepEvent);
        Debug.Log("PlayOneShot выполнен для runStep");
        
        // Если нужны параметры, добавим их отдельно
        if (!string.IsNullOrEmpty(cookieParameterName))
        {
            Debug.Log("Пробуем с параметрами...");
            try
            {
                float cookieParam = CalculateCookieParameter();
                
                EventInstance runInstance = RuntimeManager.CreateInstance(runStepEvent);
                runInstance.setParameterByName(cookieParameterName, cookieParam);
                runInstance.start();
                runInstance.release();
                
                Debug.Log($"Параметрическая версия: печенье={GetCookieCount()}, параметр={cookieParam:F3}");
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
                Debug.LogWarning($"Неизвестный тип шага: {stepType}");
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
