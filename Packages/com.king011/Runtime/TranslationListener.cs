using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace com.king011
{
    public class TranslationListener : MonoBehaviour
    {
        [System.Serializable]
        public class Node
        {
            public string key;
            public Text text;
        }
        [System.Serializable]
        public class Layer
        {
            public string name;
            public Node[] list;
        }
        [Label("翻譯資源")]
        public IntlTranslation intlTranslation;
        public Layer[] layer;

        void Start()
        {
            OnLanguageChange();
            intlTranslation.AddListener(OnLanguageChange);
        }

        void OnDestroy()
        {
            intlTranslation.RemoveListener(OnLanguageChange);
        }
        private void OnLanguageChange()
        {
            if (layer != null)
            {
                foreach (var list in layer)
                {
                    if (list != null && list.list != null)
                    {
                        foreach (var node in list.list)
                        {
                            if (node != null && node.key != null)
                            {
                                Translation(node);
                            }
                        }
                    }
                }
            }
        }
        private void Translation(Node node)
        {
            if (node.text != null)
            {
                node.text.text = intlTranslation.Get(node.key);
            }
        }

        public void DefaultLanguage()
        {
            intlTranslation.language = null;
        }
        public void LanguageAfrikaans()
        {
            intlTranslation.language = SystemLanguage.Afrikaans;
        }
        public void LanguageArabic()
        {
            intlTranslation.language = SystemLanguage.Arabic;
        }
        public void LanguageBasque()
        {
            intlTranslation.language = SystemLanguage.Basque;
        }
        public void LanguageBelarusian()
        {
            intlTranslation.language = SystemLanguage.Belarusian;
        }
        public void LanguageBulgarian()
        {
            intlTranslation.language = SystemLanguage.Bulgarian;
        }
        public void LanguageCatalan()
        {
            intlTranslation.language = SystemLanguage.Catalan;
        }
        public void LanguageChinese()
        {
            intlTranslation.language = SystemLanguage.Chinese;
        }
        public void LanguageCzech()
        {
            intlTranslation.language = SystemLanguage.Czech;
        }
        public void LanguageDanish()
        {
            intlTranslation.language = SystemLanguage.Danish;
        }
        public void LanguageDutch()
        {
            intlTranslation.language = SystemLanguage.Dutch;
        }
        public void LanguageEnglish()
        {
            intlTranslation.language = SystemLanguage.English;
        }
        public void LanguageEstonian()
        {
            intlTranslation.language = SystemLanguage.Estonian;
        }
        public void LanguageFaroese()
        {
            intlTranslation.language = SystemLanguage.Faroese;
        }
        public void LanguageFinnish()
        {
            intlTranslation.language = SystemLanguage.Finnish;
        }
        public void LanguageFrench()
        {
            intlTranslation.language = SystemLanguage.French;
        }
        public void LanguageGerman()
        {
            intlTranslation.language = SystemLanguage.German;
        }
        public void LanguageGreek()
        {
            intlTranslation.language = SystemLanguage.Greek;
        }
        public void LanguageHebrew()
        {
            intlTranslation.language = SystemLanguage.Hebrew;
        }
        public void LanguageHungarian()
        {
            intlTranslation.language = SystemLanguage.Hungarian;
        }
        public void LanguageIcelandic()
        {
            intlTranslation.language = SystemLanguage.Icelandic;
        }
        public void LanguageIndonesian()
        {
            intlTranslation.language = SystemLanguage.Indonesian;
        }
        public void LanguageItalian()
        {
            intlTranslation.language = SystemLanguage.Italian;
        }
        public void LanguageJapanese()
        {
            intlTranslation.language = SystemLanguage.Japanese;
        }
        public void LanguageKorean()
        {
            intlTranslation.language = SystemLanguage.Korean;
        }
        public void LanguageLatvian()
        {
            intlTranslation.language = SystemLanguage.Latvian;
        }
        public void LanguageLithuanian()
        {
            intlTranslation.language = SystemLanguage.Lithuanian;
        }
        public void LanguageNorwegian()
        {
            intlTranslation.language = SystemLanguage.Norwegian;
        }
        public void LanguagePolish()
        {
            intlTranslation.language = SystemLanguage.Polish;
        }
        public void LanguagePortuguese()
        {
            intlTranslation.language = SystemLanguage.Portuguese;
        }
        public void LanguageRomanian()
        {
            intlTranslation.language = SystemLanguage.Romanian;
        }
        public void LanguageRussian()
        {
            intlTranslation.language = SystemLanguage.Russian;
        }
        public void LanguageSerboCroatian()
        {
            intlTranslation.language = SystemLanguage.SerboCroatian;
        }
        public void LanguageSlovak()
        {
            intlTranslation.language = SystemLanguage.Slovak;
        }
        public void LanguageSlovenian()
        {
            intlTranslation.language = SystemLanguage.Slovenian;
        }
        public void LanguageSpanish()
        {
            intlTranslation.language = SystemLanguage.Spanish;
        }
        public void LanguageSwedish()
        {
            intlTranslation.language = SystemLanguage.Swedish;
        }
        public void LanguageThai()
        {
            intlTranslation.language = SystemLanguage.Thai;
        }
        public void LanguageTurkish()
        {
            intlTranslation.language = SystemLanguage.Turkish;
        }
        public void LanguageUkrainian()
        {
            intlTranslation.language = SystemLanguage.Ukrainian;
        }
        public void LanguageVietnamese()
        {
            intlTranslation.language = SystemLanguage.Vietnamese;
        }
        public void LanguageChineseSimplified()
        {
            intlTranslation.language = SystemLanguage.ChineseSimplified;
        }
        public void LanguageChineseTraditional()
        {
            intlTranslation.language = SystemLanguage.ChineseTraditional;
        }
    }
}