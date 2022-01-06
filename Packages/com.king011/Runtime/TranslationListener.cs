using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace com.king011
{
    public class TranslationListener : MonoBehaviour
    {
        [System.Serializable]
        public class Node<T>
        {
            public string key;
            public T node;
        }
        [Label("翻譯資源")]
        public IntlTranslation intlTranslation;
        public Node<Text>[] text;

        [Label("用於開發時重載配置")]
        public bool reset = false;
        void Start()
        {
            intlTranslation.AddListener(OnLanguageChange);
            OnLanguageChange();
        }

        void OnDestroy()
        {
            intlTranslation.RemoveListener(OnLanguageChange);
        }
        private void OnLanguageChange()
        {
            if (text != null)
            {
                foreach (var item in text)
                {
                    if (item.node == null)
                    {
                        continue;
                    }
                    item.node.text = intlTranslation.Get(item.key);
                }
            }
        }
    }
}