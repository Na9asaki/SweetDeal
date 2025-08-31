using TMPro;
using UnityEngine;

namespace SweetDeal.Source.UI
{
    public class Message : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private RectTransform rectTransform;

        public void Place(RectTransform parent)
        {
            rectTransform.SetParent(parent, false); // false = сброс локальных координат/масштаба
    
            // Обнуляем якоря (растягивание по всей области)
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero; // left / bottom
            rectTransform.offsetMax = Vector2.zero;
        }
        
        public void Write(string msg)
        {
            text.text = msg;
        }
    }
}