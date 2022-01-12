using UnityEngine;
using UnityEngine.UI;
namespace com.king011
{
    public class TranslationText : MonoBehaviour
    {
        [Label("翻譯資源")]
        public IntlTranslation intlTranslation;
        public string key;
        private Text text;
        private void Start()
        {
            text = GetComponent<Text>();
            OnLanguageChange();
            intlTranslation.AddListener(OnLanguageChange);
        }

        private void OnDestroy()
        {
            intlTranslation.RemoveListener(OnLanguageChange);
        }
        private void OnLanguageChange()
        {
            text.text = intlTranslation.Get(key);
        }
    }
}