using UnityEditor;
using UnityEngine;

namespace SweetDeal.Source.Stealth.Editor
{
    [CustomEditor(typeof(NoiseSignal))]
    public class NoiseSignalEditor : UnityEditor.Editor
    {
        private Vector3 _position = Vector3.zero;
        private float _force = 5f;

        public override void OnInspectorGUI()
        {
            // Рисуем дефолтный инспектор
            DrawDefaultInspector();

            // Дополнительные поля для вызова Emit
            _position = EditorGUILayout.Vector3Field("Emit Position", _position);
            _force = EditorGUILayout.FloatField("Force", _force);

            // Кнопка
            if (GUILayout.Button("Emit Noise"))
            {
                var noiseSignal = (NoiseSignal)target;
                noiseSignal.Emit(noiseSignal.transform.position, _force);
            }
        }
    }
}