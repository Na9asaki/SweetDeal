using UnityEngine;

namespace SweetDeal.Source.GameplaySystems
{
    public static class DataKeeper
    {
        public static void Save<T>(T data, string key)
        {
            var jsonData = JsonUtility.ToJson(data);
            Debug.Log(jsonData);
            PlayerPrefs.SetString(key, jsonData);
            PlayerPrefs.Save();
        }

        public static T Load<T>(string key, bool canClear = false) where T : class
        {
            if (!PlayerPrefs.HasKey(key)) return null;
            var jsonData = PlayerPrefs.GetString(key);

            if (canClear)
            {
                DeleteKey(key);
            }
            
            return JsonUtility.FromJson<T>(jsonData);
        }

        public static void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.Save();
        }
    }
}