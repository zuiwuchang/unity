using UnityEditor;
using UnityEngine;
namespace com.king011
{
    /// <summary>
    /// 定義對帶有 `LabelAttribute` 特性字段的面板內容繪製
    /// </summary>
    [CustomPropertyDrawer(typeof(LabelAttribute))]
    public class LabelDrawer : PropertyDrawer
    {
        private GUIContent _label = null;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_label == null)
            {
                string name = (attribute as LabelAttribute).name;
                label.text = name;
                _label = new GUIContent(name);
            }
            EditorGUI.PropertyField(position, property, _label);
        }
    }
}