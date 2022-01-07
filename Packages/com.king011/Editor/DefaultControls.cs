using UnityEngine;
using UnityEngine.UI;

namespace com.king011
{
    public static class DefaultControls
    {
        /// <summary>
        /// Object used to pass resources to use for the default controls.
        /// </summary>
        public struct Resources
        {
            /// <summary>
            /// The primary sprite to be used for graphical UI elements, used by the button, toggle, and dropdown controls, among others.
            /// </summary>
            public Sprite standard;

            /// <summary>
            /// Sprite used for background elements.
            /// </summary>
            public Sprite background;

            /// <summary>
            /// Sprite used as background for input fields.
            /// </summary>
            public Sprite inputField;

            /// <summary>
            /// Sprite used for knobs that can be dragged, such as on a slider.
            /// </summary>
            public Sprite knob;

            /// <summary>
            /// Sprite used for representation of an "on" state when present, such as a checkmark.
            /// </summary>
            public Sprite checkmark;

            /// <summary>
            /// Sprite used to indicate that a button will open a dropdown when clicked.
            /// </summary>
            public Sprite dropdown;

            /// <summary>
            /// Sprite used for masking purposes, for example to be used for the viewport of a scroll view.
            /// </summary>
            public Sprite mask;
        }
        public static GameObject CreateSwitch(Resources resources)
        {
            // 創建節點
            var size = new Vector2(200, 80);
            GameObject switchRoot = CreateUIElementRoot("Switch", size);

            // 添加一個 圖像作爲背景
            Image image = switchRoot.AddComponent<Image>();
            image.sprite = resources.standard;
            image.type = Image.Type.Sliced;
            image.color = new Color(1f, 1f, 1f, 1f);

            // 添加 knob
            GameObject knob = CreateUIObject("Knob", switchRoot);
            knob.transform.localPosition = new Vector3(-size.x / 2, 0, 0);
            Image knobImage = knob.AddComponent<Image>();
            knobImage.sprite = resources.knob;
            knobImage.color = new Color(1f, 1f, 1f, 1f);
            Selectable knobSelectable = knob.AddComponent<Selectable>();
            knobSelectable.transition = Selectable.Transition.ColorTint;

            // 添加組件腳本
            Switch s = switchRoot.AddComponent<Switch>();
            // 爲組件腳本設置初始化值
            s.transition = Selectable.Transition.None;

            return switchRoot;
        }

        private static GameObject CreateUIElementRoot(string name, Vector2 size)
        {
            GameObject child = new GameObject(name);
            RectTransform rectTransform = child.AddComponent<RectTransform>();
            rectTransform.sizeDelta = size;
            return child;
        }

        static GameObject CreateUIObject(string name, GameObject parent)
        {
            GameObject go = new GameObject(name);
            go.AddComponent<RectTransform>();
            SetParentAndAlign(go, parent);
            return go;
        }
        private static void SetParentAndAlign(GameObject child, GameObject parent)
        {
            if (parent == null)
                return;

            child.transform.SetParent(parent.transform, false);
            SetLayerRecursively(child, parent.layer);
        }

        private static void SetLayerRecursively(GameObject go, int layer)
        {
            go.layer = layer;
            Transform t = go.transform;
            for (int i = 0; i < t.childCount; i++)
                SetLayerRecursively(t.GetChild(i).gameObject, layer);
        }
    }
}